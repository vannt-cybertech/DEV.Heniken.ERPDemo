using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using MySql.Data.MySqlClient;
using HenikenERP.Core.Entities;
using HenikenERP.Data.Context;

namespace HenikenERP.Data.Repositories
{
    public class InventoryRepository : Repository<Inventory>
    {
        public InventoryRepository(DatabaseContext context) : base(context, "INVENTORY") { }
        
        public override Inventory GetById(string id)
        {
            var dataTable = ExecuteQuery("SELECT * FROM INVENTORY WHERE Inventory_ID = @Inventory_ID",
                new MySqlParameter[] { new MySqlParameter("@Inventory_ID", id) });
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public Inventory GetByWarehouseAndProduct(string warehouseId, string productId)
        {
            var query = "SELECT * FROM INVENTORY WHERE Warehouse_ID = @Warehouse_ID AND Product_ID = @Product_ID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Warehouse_ID", warehouseId),
                new MySqlParameter("@Product_ID", productId)
            };
            var dataTable = ExecuteQuery(query, parameters);
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public override IEnumerable<Inventory> GetAll()
        {
            var dataTable = ExecuteQuery("SELECT * FROM INVENTORY ORDER BY Warehouse_ID, Product_ID");
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<Inventory> Find(Expression<Func<Inventory, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }
        
        public override void Add(Inventory entity)
        {
            ExecuteNonQuery(@"INSERT INTO INVENTORY (Inventory_ID, Warehouse_ID, Product_ID, Quantity_On_Hand, Quantity_Reserved, Created_Date, Modified_Date)
                            VALUES (@Inventory_ID, @Warehouse_ID, @Product_ID, @Quantity_On_Hand, @Quantity_Reserved, @Created_Date, @Modified_Date)",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Inventory_ID", entity.Inventory_ID),
                    new MySqlParameter("@Warehouse_ID", entity.Warehouse_ID),
                    new MySqlParameter("@Product_ID", entity.Product_ID),
                    new MySqlParameter("@Quantity_On_Hand", entity.Quantity_On_Hand),
                    new MySqlParameter("@Quantity_Reserved", entity.Quantity_Reserved),
                    new MySqlParameter("@Created_Date", DateTime.Now),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Update(Inventory entity)
        {
            ExecuteNonQuery(@"UPDATE INVENTORY SET Quantity_On_Hand = @Quantity_On_Hand, Quantity_Reserved = @Quantity_Reserved, 
                            Modified_Date = @Modified_Date WHERE Inventory_ID = @Inventory_ID",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Inventory_ID", entity.Inventory_ID),
                    new MySqlParameter("@Quantity_On_Hand", entity.Quantity_On_Hand),
                    new MySqlParameter("@Quantity_Reserved", entity.Quantity_Reserved),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Delete(Inventory entity) => DeleteById(entity.Inventory_ID);
        public override void DeleteById(string id) => ExecuteNonQuery("DELETE FROM INVENTORY WHERE Inventory_ID = @Inventory_ID",
            new MySqlParameter[] { new MySqlParameter("@Inventory_ID", id) });
        public override bool Exists(string id) => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM INVENTORY WHERE Inventory_ID = @Inventory_ID",
            new MySqlParameter[] { new MySqlParameter("@Inventory_ID", id) }).Rows[0][0]) > 0;
        public override int Count() => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM INVENTORY").Rows[0][0]);
        
        /// <summary>
        /// Get inventory totals for dashboard
        /// </summary>
        public Dictionary<string, int> GetInventoryTotals()
        {
            var query = @"SELECT 
                         SUM(Quantity_On_Hand) as TotalOnHand,
                         SUM(Quantity_Reserved) as TotalReserved,
                         SUM(Quantity_On_Hand - Quantity_Reserved) as TotalAvailable
                         FROM INVENTORY";
            var dataTable = ExecuteQuery(query);
            var result = new Dictionary<string, int>();
            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                result["TotalOnHand"] = row["TotalOnHand"] != DBNull.Value ? Convert.ToInt32(row["TotalOnHand"]) : 0;
                result["TotalReserved"] = row["TotalReserved"] != DBNull.Value ? Convert.ToInt32(row["TotalReserved"]) : 0;
                result["TotalAvailable"] = row["TotalAvailable"] != DBNull.Value ? Convert.ToInt32(row["TotalAvailable"]) : 0;
            }
            return result;
        }
        
        /// <summary>
        /// Get inventory items with product details
        /// </summary>
        public IEnumerable<Inventory> GetInventoryWithProducts()
        {
            var query = @"SELECT i.*, p.Product_Name, w.Location 
                         FROM INVENTORY i 
                         LEFT JOIN PRODUCTS p ON i.Product_ID = p.Product_ID
                         LEFT JOIN WAREHOUSES w ON i.Warehouse_ID = w.Warehouse_ID
                         ORDER BY w.Location, p.Product_Name";
            var dataTable = ExecuteQuery(query);
            return dataTable.Rows.Cast<DataRow>().Select(row => 
            {
                var inventory = MapToEntity(row);
                if (row["Product_Name"] != DBNull.Value)
                {
                    inventory.Product = new Product { Product_Name = row["Product_Name"].ToString() };
                }
                if (row["Location"] != DBNull.Value)
                {
                    inventory.Warehouse = new Warehouse { Location = row["Location"].ToString() };
                }
                return inventory;
            });
        }
        
        private Inventory MapToEntity(DataRow row) => new Inventory
        {
            Inventory_ID = row["Inventory_ID"].ToString(),
            Warehouse_ID = row["Warehouse_ID"].ToString(),
            Product_ID = row["Product_ID"].ToString(),
            Quantity_On_Hand = Convert.ToInt32(row["Quantity_On_Hand"]),
            Quantity_Reserved = Convert.ToInt32(row["Quantity_Reserved"]),
            Created_Date = Convert.ToDateTime(row["Created_Date"]),
            Modified_Date = Convert.ToDateTime(row["Modified_Date"])
        };
    }
}

