namespace HenikenERP.Core.Interfaces
{
    /// <summary>
    /// Unit of Work pattern interface
    /// </summary>
    public interface IUnitOfWork
    {
        IRepository<Entities.Product> Products { get; }
        IRepository<Entities.Customer> Customers { get; }
        IRepository<Entities.CustomerTier> CustomerTiers { get; }
        IRepository<Entities.PriceList> PriceLists { get; }
        IRepository<Entities.Warehouse> Warehouses { get; }
        IRepository<Entities.Inventory> Inventories { get; }
        IRepository<Entities.Order> Orders { get; }
        IRepository<Entities.OrderDetail> OrderDetails { get; }
        IRepository<Entities.User> Users { get; }
        IRepository<Entities.AppSetting> AppSettings { get; }
        
        void SaveChanges();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}

