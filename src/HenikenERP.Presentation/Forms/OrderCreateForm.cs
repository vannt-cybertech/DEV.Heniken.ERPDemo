using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using HenikenERP.Core.Entities;
using HenikenERP.Core.DTOs;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Business.Services;
using HenikenERP.Common.Helpers;
using HenikenERP.Presentation.UI.Theme;

namespace HenikenERP.Presentation.Forms
{
    public partial class OrderCreateForm : Form
    {
        private UnitOfWork _unitOfWork;
        private OrderService _orderService;
        private PriceListService _priceService;
        private User _currentUser;
        private BindingList<OrderDetailItem> _orderDetails;

        public OrderCreateForm(User currentUser = null)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _orderDetails = new BindingList<OrderDetailItem>();
            
            this.Load += OrderCreateForm_Load;
            btnAddProduct.Click += BtnAddProduct_Click;
            btnRemoveProduct.Click += BtnRemoveProduct_Click;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;
            cboCustomer.SelectedIndexChanged += CboCustomer_SelectedIndexChanged;
            cboWarehouse.SelectedIndexChanged += CboWarehouse_SelectedIndexChanged;
        }

        private void OrderCreateForm_Load(object sender, EventArgs e)
        {
            // Apply theme
            ThemeHelper.ApplyTheme(this);

            try
            {
                var context = new DatabaseContext();
                _unitOfWork = new UnitOfWork(context);
                _orderService = new OrderService(_unitOfWork);
                _priceService = new PriceListService(_unitOfWork);
                
                LoadCustomers();
                LoadWarehouses();
                InitializeOrderDetailsGrid();
                
                // Load products for the initially selected warehouse
                if (cboWarehouse.SelectedValue != null)
                {
                    UpdateProductListForWarehouse();
                }
                
                dtpOrderDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                Logger.LogError("Error loading order create form", ex);
                MessageBox.Show($"Lỗi khi tải form: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomers()
        {
            var customers = _unitOfWork.Customers.GetAll()
                .Where(c => c.Is_Active)
                .OrderBy(c => c.Customer_Name)
                .ToList();
            
            cboCustomer.DisplayMember = "Customer_Name";
            cboCustomer.ValueMember = "Customer_ID";
            cboCustomer.DataSource = customers;
        }

        private void LoadWarehouses()
        {
            var warehouses = _unitOfWork.Warehouses.GetAll()
                .OrderBy(w => w.Location)
                .ToList();
            
            cboWarehouse.DisplayMember = "Location";
            cboWarehouse.ValueMember = "Warehouse_ID";
            cboWarehouse.DataSource = warehouses;
        }

        private void InitializeOrderDetailsGrid()
        {
            dgvOrderDetails.AutoGenerateColumns = false;
            dgvOrderDetails.Columns.Clear();
            
            var colProduct = new DataGridViewComboBoxColumn
            {
                DataPropertyName = "Product_ID",
                HeaderText = "Sản phẩm",
                Name = "Product_ID",
                Width = 250,
                DisplayMember = "Product_Name",
                ValueMember = "Product_ID"
            };
            
            dgvOrderDetails.Columns.Add(colProduct);
            
            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Số lượng",
                Name = "Quantity",
                Width = 100
            });
            
            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Unit_Price",
                HeaderText = "Đơn giá",
                Name = "Unit_Price",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight },
                ReadOnly = true
            });
            
            dgvOrderDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Line_Total",
                HeaderText = "Thành tiền",
                Name = "Line_Total",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight },
                ReadOnly = true
            });
            
            dgvOrderDetails.DataSource = _orderDetails;
            dgvOrderDetails.CellValueChanged += DgvOrderDetails_CellValueChanged;
        }
        
        private void CboWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update product list based on selected warehouse
            UpdateProductListForWarehouse();
            
            // Clear existing order details when warehouse changes
            if (_orderDetails.Count > 0)
            {
                var result = MessageBox.Show(
                    "Thay đổi kho sẽ xóa các sản phẩm đã chọn. Bạn có muốn tiếp tục?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    _orderDetails.Clear();
                    CalculateTotal();
                }
                else
                {
                    // Revert warehouse selection
                    cboWarehouse.SelectedIndexChanged -= CboWarehouse_SelectedIndexChanged;
                    // Keep current selection
                    cboWarehouse.SelectedIndexChanged += CboWarehouse_SelectedIndexChanged;
                }
            }
        }
        
        private void UpdateProductListForWarehouse()
        {
            if (cboWarehouse.SelectedValue == null) return;
            
            string warehouseId = cboWarehouse.SelectedValue.ToString();
            
            // Get products that have inventory in the selected warehouse
            var inventoryInWarehouse = _unitOfWork.Inventories.GetAll()
                .Where(inv => inv.Warehouse_ID == warehouseId && inv.Available_Quantity > 0)
                .Select(inv => inv.Product_ID)
                .Distinct()
                .ToList();
            
            // Get product details for those products
            var availableProducts = _unitOfWork.Products.GetAll()
                .Where(p => inventoryInWarehouse.Contains(p.Product_ID))
                .OrderBy(p => p.Product_Name)
                .ToList();
            
            // Update the product column datasource
            var colProduct = dgvOrderDetails.Columns["Product_ID"] as DataGridViewComboBoxColumn;
            if (colProduct != null)
            {
                colProduct.DataSource = availableProducts;
            }
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            _orderDetails.Add(new OrderDetailItem());
        }

        private void BtnRemoveProduct_Click(object sender, EventArgs e)
        {
            if (dgvOrderDetails.SelectedRows.Count > 0)
            {
                var index = dgvOrderDetails.SelectedRows[0].Index;
                _orderDetails.RemoveAt(index);
                CalculateTotal();
            }
        }

        private void CboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Recalculate prices when customer changes (tier affects pricing)
            UpdateAllPrices();
        }

        private void DgvOrderDetails_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            
            var row = dgvOrderDetails.Rows[e.RowIndex];
            var detail = row.DataBoundItem as OrderDetailItem;
            if (detail == null) return;
            
            // When product or quantity changes, update price
            if (e.ColumnIndex == dgvOrderDetails.Columns["Product_ID"].Index || 
                e.ColumnIndex == dgvOrderDetails.Columns["Quantity"].Index)
            {
                UpdateLinePrice(detail);
                dgvOrderDetails.Refresh();
                CalculateTotal();
            }
        }

        private void UpdateAllPrices()
        {
            foreach (var detail in _orderDetails)
            {
                UpdateLinePrice(detail);
            }
            dgvOrderDetails.Refresh();
            CalculateTotal();
        }

        private void UpdateLinePrice(OrderDetailItem detail)
        {
            if (cboCustomer.SelectedValue == null || string.IsNullOrEmpty(detail.Product_ID))
                return;
            
            var customer = cboCustomer.SelectedItem as Customer;
            if (customer == null) return;
            
            var price = _priceService.GetActivePrice(detail.Product_ID, customer.Tier_ID, dtpOrderDate.Value);
            detail.Unit_Price = price ?? 0;
            detail.Line_Total = detail.Quantity * detail.Unit_Price;
        }

        private void CalculateTotal()
        {
            decimal total = _orderDetails.Sum(d => d.Line_Total);
            txtTotal.Text = total.ToString("N0");
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (cboCustomer.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn khách hàng", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (cboWarehouse.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn kho", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (_orderDetails.Count == 0)
                {
                    MessageBox.Show("Vui lòng thêm ít nhất một sản phẩm", "Thông báo", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // Check for invalid rows
                foreach (var detail in _orderDetails)
                {
                    if (string.IsNullOrEmpty(detail.Product_ID))
                    {
                        MessageBox.Show("Vui lòng chọn sản phẩm cho tất cả các dòng", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    if (detail.Quantity <= 0)
                    {
                        MessageBox.Show("Số lượng phải lớn hơn 0", "Thông báo", 
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                
                // Create order DTO
                var orderDTO = new OrderCreateDTO
                {
                    Customer_ID = cboCustomer.SelectedValue.ToString(),
                    Warehouse_ID = cboWarehouse.SelectedValue.ToString(),
                    Order_Date = dtpOrderDate.Value,
                    OrderDetails = _orderDetails.Select(d => new OrderDetailCreateDTO
                    {
                        Product_ID = d.Product_ID,
                        Quantity = d.Quantity
                    }).ToList()
                };
                
                var result = _orderService.CreateOrder(orderDTO, _currentUser?.Username ?? "admin");
                
                if (result.Success)
                {
                    MessageBox.Show($"{result.Message}\nMã đơn hàng: {result.OrderId}", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show(result.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Error creating order", ex);
                MessageBox.Show($"Lỗi khi tạo đơn hàng: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        // Helper class for order detail binding
        private class OrderDetailItem
        {
            public string Product_ID { get; set; }
            public int Quantity { get; set; } = 1;
            public decimal Unit_Price { get; set; }
            public decimal Line_Total { get; set; }
        }
    }
}

