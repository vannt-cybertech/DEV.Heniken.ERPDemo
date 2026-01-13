using System;
using HenikenERP.Core.Interfaces;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;

namespace HenikenERP.Data.UnitOfWork
{
    /// <summary>
    /// Unit of Work implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DatabaseContext _context;
        private bool _disposed = false;
        private MySql.Data.MySqlClient.MySqlTransaction _transaction;
        
        private IRepository<Core.Entities.Product> _products;
        private IRepository<Core.Entities.Customer> _customers;
        private IRepository<Core.Entities.CustomerTier> _customerTiers;
        private IRepository<Core.Entities.PriceList> _priceLists;
        private IRepository<Core.Entities.Warehouse> _warehouses;
        private IRepository<Core.Entities.Inventory> _inventories;
        private IRepository<Core.Entities.Order> _orders;
        private IRepository<Core.Entities.OrderDetail> _orderDetails;
        private IRepository<Core.Entities.User> _users;
        private IRepository<Core.Entities.AppSetting> _appSettings;
        
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }
        
        public IRepository<Core.Entities.Product> Products
        {
            get { return _products ?? (_products = new ProductRepository(_context)); }
        }
        
        public IRepository<Core.Entities.Customer> Customers
        {
            get { return _customers ?? (_customers = new CustomerRepository(_context)); }
        }
        
        public IRepository<Core.Entities.CustomerTier> CustomerTiers
        {
            get { return _customerTiers ?? (_customerTiers = new CustomerTierRepository(_context)); }
        }
        
        public IRepository<Core.Entities.PriceList> PriceLists
        {
            get { return _priceLists ?? (_priceLists = new PriceListRepository(_context)); }
        }
        
        public IRepository<Core.Entities.Warehouse> Warehouses
        {
            get { return _warehouses ?? (_warehouses = new WarehouseRepository(_context)); }
        }
        
        public IRepository<Core.Entities.Inventory> Inventories
        {
            get { return _inventories ?? (_inventories = new InventoryRepository(_context)); }
        }
        
        public IRepository<Core.Entities.Order> Orders
        {
            get { return _orders ?? (_orders = new OrderRepository(_context)); }
        }
        
        public IRepository<Core.Entities.OrderDetail> OrderDetails
        {
            get { return _orderDetails ?? (_orderDetails = new OrderDetailRepository(_context)); }
        }
        
        public IRepository<Core.Entities.User> Users
        {
            get { return _users ?? (_users = new UserRepository(_context)); }
        }
        
        public IRepository<Core.Entities.AppSetting> AppSettings
        {
            get { return _appSettings ?? (_appSettings = new AppSettingRepository(_context)); }
        }
        
        public void SaveChanges()
        {
            // For ADO.NET, changes are committed immediately unless in transaction
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }
        
        public void BeginTransaction()
        {
            if (_transaction == null)
            {
                _transaction = _context.Connection.BeginTransaction();
            }
        }
        
        public void CommitTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Commit();
                _transaction = null;
            }
        }
        
        public void RollbackTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                }
                _disposed = true;
            }
        }
    }
}

