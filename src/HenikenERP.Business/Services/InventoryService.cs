using System;
using System.Collections.Generic;
using HenikenERP.Core.DTOs;
using HenikenERP.Core.Entities;
using HenikenERP.Core.Enums;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;
using HenikenERP.Data.UnitOfWork;
using HenikenERP.Common.Helpers;
using HenikenERP.Common.Constants;

namespace HenikenERP.Business.Services
{
    /// <summary>
    /// Inventory service for stock management
    /// </summary>
    public class InventoryService
    {
        private readonly UnitOfWork _unitOfWork;
        
        public InventoryService()
        {
            var context = new DatabaseContext();
            _unitOfWork = new UnitOfWork(context);
        }
        
        public IEnumerable<Inventory> GetAllInventory()
        {
            return _unitOfWork.Inventories.GetAll();
        }
        
        public Inventory GetInventoryByWarehouseAndProduct(string warehouseId, string productId)
        {
            var repository = _unitOfWork.Inventories as InventoryRepository;
            return repository?.GetByWarehouseAndProduct(warehouseId, productId);
        }
        
        /// <summary>
        /// Process stock transaction (in/out/adjust/transfer)
        /// </summary>
        public bool ProcessStockTransaction(StockTransactionDTO transaction)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                var inventoryRepository = _unitOfWork.Inventories as InventoryRepository;
                
                var inventory = inventoryRepository?.GetByWarehouseAndProduct(transaction.Warehouse_ID, transaction.Product_ID);
                bool isNewInventory = (inventory == null);
                
                if (inventory == null)
                {
                    // Create new inventory record
                    inventory = new Inventory
                    {
                        Inventory_ID = IDGenerator.GenerateIDWithTimestamp(AppConstants.PREFIX_INVENTORY),
                        Warehouse_ID = transaction.Warehouse_ID,
                        Product_ID = transaction.Product_ID,
                        Quantity_On_Hand = 0,
                        Quantity_Reserved = 0
                    };
                }
                
                switch (transaction.TransactionType)
                {
                    case StockTransactionType.StockIn:
                        inventory.Quantity_On_Hand += transaction.Quantity;
                        break;
                    case StockTransactionType.StockOut:
                        if (inventory.Available_Quantity < transaction.Quantity)
                        {
                            _unitOfWork.RollbackTransaction();
                            return false;
                        }
                        inventory.Quantity_On_Hand -= transaction.Quantity;
                        break;
                    case StockTransactionType.Adjustment:
                        inventory.Quantity_On_Hand = transaction.Quantity;
                        break;
                    case StockTransactionType.Transfer:
                        if (inventory.Available_Quantity < transaction.Quantity)
                        {
                            _unitOfWork.RollbackTransaction();
                            return false;
                        }
                        inventory.Quantity_On_Hand -= transaction.Quantity;
                        
                        // Add to destination warehouse
                        var destInventory = inventoryRepository?.GetByWarehouseAndProduct(transaction.To_Warehouse_ID, transaction.Product_ID);
                        if (destInventory == null)
                        {
                            destInventory = new Inventory
                            {
                                Inventory_ID = IDGenerator.GenerateIDWithTimestamp(AppConstants.PREFIX_INVENTORY),
                                Warehouse_ID = transaction.To_Warehouse_ID,
                                Product_ID = transaction.Product_ID,
                                Quantity_On_Hand = transaction.Quantity,
                                Quantity_Reserved = 0
                            };
                            _unitOfWork.Inventories.Add(destInventory);
                        }
                        else
                        {
                            destInventory.Quantity_On_Hand += transaction.Quantity;
                            _unitOfWork.Inventories.Update(destInventory);
                        }
                        break;
                }
                
                // Fix: Use isNewInventory flag instead of checking Inventory_ID prefix
                if (isNewInventory)
                {
                    _unitOfWork.Inventories.Add(inventory);
                }
                else
                {
                    _unitOfWork.Inventories.Update(inventory);
                }
                
                _unitOfWork.CommitTransaction();
                return true;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                Logger.LogError("Error processing stock transaction", ex);
                return false;
            }
        }
    }
}

