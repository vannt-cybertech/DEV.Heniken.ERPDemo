using System;
using System.Collections.Generic;
using System.Linq;
using HenikenERP.Core.Entities;
using HenikenERP.Core.DTOs;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Common.Helpers;
using HenikenERP.Common.Constants;

namespace HenikenERP.Business.Services
{
    /// <summary>
    /// Order service for order management
    /// </summary>
    public class OrderService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public OrderService()
        {
            var context = new DatabaseContext();
            _unitOfWork = new UnitOfWork(context);
        }
        
        public OrderService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// Create new order with automatic pricing and inventory reservation
        /// </summary>
        public (bool Success, string Message, string OrderId) CreateOrder(OrderCreateDTO orderDTO, string createdBy)
        {
            try
            {
                // Validation
                if (orderDTO == null || orderDTO.OrderDetails == null || !orderDTO.OrderDetails.Any())
                {
                    return (false, "Đơn hàng phải có ít nhất một sản phẩm", null);
                }

                _unitOfWork.BeginTransaction();
                
                // Get customer to determine tier
                var customer = _unitOfWork.Customers.GetById(orderDTO.Customer_ID);
                if (customer == null)
                {
                    _unitOfWork.RollbackTransaction();
                    return (false, "Không tìm thấy khách hàng", null);
                }
                
                // Verify warehouse exists
                var warehouse = _unitOfWork.Warehouses.GetById(orderDTO.Warehouse_ID);
                if (warehouse == null)
                {
                    _unitOfWork.RollbackTransaction();
                    return (false, "Không tìm thấy kho hàng", null);
                }
                
                // Create order
                var order = new Order
                {
                    Order_ID = IDGenerator.GenerateOrderID(),
                    Customer_ID = orderDTO.Customer_ID,
                    Warehouse_ID = orderDTO.Warehouse_ID,
                    Order_Date = orderDTO.Order_Date,
                    Status = AppConstants.ORDER_STATUS_CREATED,
                    Created_By = createdBy,
                    Total_Amount = 0
                };
                
                // Add order first (before details) to satisfy foreign key constraint
                _unitOfWork.Orders.Add(order);
                
                var priceService = new PriceListService(_unitOfWork);
                var inventoryRepository = _unitOfWork.Inventories as InventoryRepository;
                decimal totalAmount = 0;
                
                // Process order details
                foreach (var detailDTO in orderDTO.OrderDetails)
                {
                    // Get product
                    var product = _unitOfWork.Products.GetById(detailDTO.Product_ID);
                    if (product == null)
                    {
                        _unitOfWork.RollbackTransaction();
                        return (false, $"Không tìm thấy sản phẩm: {detailDTO.Product_ID}", null);
                    }
                    
                    // Get active price for product and customer tier
                    decimal? unitPrice = priceService.GetActivePrice(detailDTO.Product_ID, customer.Tier_ID, orderDTO.Order_Date);
                    if (!unitPrice.HasValue)
                    {
                        _unitOfWork.RollbackTransaction();
                        return (false, $"Không tìm thấy giá cho sản phẩm '{product.Product_Name}' và hạng khách hàng '{customer.Tier?.Tier_Name ?? "N/A"}'", null);
                    }
                    
                    // Check inventory availability
                    var inventory = inventoryRepository?.GetByWarehouseAndProduct(orderDTO.Warehouse_ID, detailDTO.Product_ID);
                    if (inventory == null)
                    {
                        _unitOfWork.RollbackTransaction();
                        return (false, $"Sản phẩm '{product.Product_Name}' không có trong kho '{warehouse.Location}'", null);
                    }
                    
                    if (inventory.Available_Quantity < detailDTO.Quantity)
                    {
                        _unitOfWork.RollbackTransaction();
                        return (false, $"Sản phẩm '{product.Product_Name}' không đủ số lượng. Khả dụng: {inventory.Available_Quantity}, Yêu cầu: {detailDTO.Quantity}", null);
                    }
                    
                    // Create order detail
                    var orderDetail = new OrderDetail
                    {
                        OrderDetail_ID = IDGenerator.GenerateOrderDetailID(order.Order_ID, orderDTO.OrderDetails.IndexOf(detailDTO) + 1),
                        Order_ID = order.Order_ID,
                        Product_ID = detailDTO.Product_ID,
                        Quantity = detailDTO.Quantity,
                        Applied_Price = unitPrice.Value
                    };
                    
                    _unitOfWork.OrderDetails.Add(orderDetail);
                    totalAmount += orderDetail.Quantity * orderDetail.Applied_Price;
                    
                    // Reserve inventory
                    inventory.Quantity_Reserved += detailDTO.Quantity;
                    _unitOfWork.Inventories.Update(inventory);
                }
                
                // Update order total amount
                order.Total_Amount = totalAmount;
                _unitOfWork.Orders.Update(order);
                
                _unitOfWork.CommitTransaction();
                
                Logger.LogInfo($"Order created successfully: {order.Order_ID}");
                return (true, "Tạo đơn hàng thành công", order.Order_ID);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                Logger.LogError("Error creating order", ex);
                return (false, $"Lỗi khi tạo đơn hàng: {ex.Message}", null);
            }
        }
        
        public IEnumerable<Order> GetAllOrders()
        {
            return _unitOfWork.Orders.GetAll();
        }
        
        public Order GetOrderById(string id)
        {
            return _unitOfWork.Orders.GetById(id);
        }
        
        /// <summary>
        /// Update order status with proper state transitions
        /// </summary>
        public (bool Success, string Message) UpdateOrderStatus(string orderId, string newStatus)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                
                var order = _unitOfWork.Orders.GetById(orderId);
                if (order == null)
                {
                    _unitOfWork.RollbackTransaction();
                    return (false, "Không tìm thấy đơn hàng");
                }
                
                // Validate status transition
                var validTransition = ValidateStatusTransition(order.Status, newStatus);
                if (!validTransition.IsValid)
                {
                    _unitOfWork.RollbackTransaction();
                    return (false, validTransition.Message);
                }
                
                var inventoryRepository = _unitOfWork.Inventories as InventoryRepository;
                
                // Handle status-specific logic
                if (newStatus == AppConstants.ORDER_STATUS_CANCELLED && order.Status != AppConstants.ORDER_STATUS_CANCELLED)
                {
                    // Release reserved inventory (do NOT return to stock - reservation is just a hold)
                    foreach (var detail in order.OrderDetails)
                    {
                        var inventory = inventoryRepository?.GetByWarehouseAndProduct(order.Warehouse_ID, detail.Product_ID);
                        if (inventory != null)
                        {
                            inventory.Quantity_Reserved -= detail.Quantity;
                            _unitOfWork.Inventories.Update(inventory);
                        }
                    }
                    Logger.LogInfo($"Order {orderId} cancelled, inventory reservation released");
                }
                else if (newStatus == AppConstants.ORDER_STATUS_COMPLETED && order.Status != AppConstants.ORDER_STATUS_COMPLETED)
                {
                    // Deduct from on-hand and release reservation
                    foreach (var detail in order.OrderDetails)
                    {
                        var inventory = inventoryRepository?.GetByWarehouseAndProduct(order.Warehouse_ID, detail.Product_ID);
                        if (inventory != null)
                        {
                            inventory.Quantity_On_Hand -= detail.Quantity;
                            inventory.Quantity_Reserved -= detail.Quantity;
                            _unitOfWork.Inventories.Update(inventory);
                        }
                    }
                    Logger.LogInfo($"Order {orderId} completed, inventory deducted");
                }
                
                order.Status = newStatus;
                _unitOfWork.Orders.Update(order);
                _unitOfWork.CommitTransaction();
                
                return (true, "Cập nhật trạng thái thành công");
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                Logger.LogError("Error updating order status", ex);
                return (false, $"Lỗi khi cập nhật trạng thái: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Validate status transition rules
        /// </summary>
        private (bool IsValid, string Message) ValidateStatusTransition(string currentStatus, string newStatus)
        {
            // Created -> Pending_Approval, Cancelled
            if (currentStatus == AppConstants.ORDER_STATUS_CREATED)
            {
                if (newStatus == AppConstants.ORDER_STATUS_PENDING_APPROVAL || newStatus == AppConstants.ORDER_STATUS_CANCELLED)
                    return (true, "");
                return (false, "Đơn hàng ở trạng thái 'Đã tạo' chỉ có thể chuyển sang 'Chờ duyệt' hoặc 'Đã hủy'");
            }
            
            // Pending_Approval -> Approved, Cancelled
            if (currentStatus == AppConstants.ORDER_STATUS_PENDING_APPROVAL)
            {
                if (newStatus == AppConstants.ORDER_STATUS_APPROVED || newStatus == AppConstants.ORDER_STATUS_CANCELLED)
                    return (true, "");
                return (false, "Đơn hàng ở trạng thái 'Chờ duyệt' chỉ có thể chuyển sang 'Đã duyệt' hoặc 'Đã hủy'");
            }
            
            // Approved -> Delivering, Cancelled
            if (currentStatus == AppConstants.ORDER_STATUS_APPROVED)
            {
                if (newStatus == AppConstants.ORDER_STATUS_DELIVERING || newStatus == AppConstants.ORDER_STATUS_CANCELLED)
                    return (true, "");
                return (false, "Đơn hàng ở trạng thái 'Đã duyệt' chỉ có thể chuyển sang 'Đang giao' hoặc 'Đã hủy'");
            }
            
            // Delivering -> Completed, Cancelled
            if (currentStatus == AppConstants.ORDER_STATUS_DELIVERING)
            {
                if (newStatus == AppConstants.ORDER_STATUS_COMPLETED || newStatus == AppConstants.ORDER_STATUS_CANCELLED)
                    return (true, "");
                return (false, "Đơn hàng ở trạng thái 'Đang giao' chỉ có thể chuyển sang 'Hoàn thành' hoặc 'Đã hủy'");
            }
            
            // Completed and Cancelled are final states
            if (currentStatus == AppConstants.ORDER_STATUS_COMPLETED || currentStatus == AppConstants.ORDER_STATUS_CANCELLED)
            {
                return (false, "Không thể thay đổi trạng thái của đơn hàng đã hoàn thành hoặc đã hủy");
            }
            
            return (true, "");
        }
    }
}

