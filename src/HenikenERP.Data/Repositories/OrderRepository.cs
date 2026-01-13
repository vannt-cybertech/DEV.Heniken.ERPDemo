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
    public class OrderRepository : Repository<Order>
    {
        public OrderRepository(DatabaseContext context) : base(context, "ORDERS") { }
        
        public override Order GetById(string id)
        {
            var query = @"SELECT o.*, c.Customer_Name, c.Tier_ID, w.Location 
                         FROM ORDERS o 
                         LEFT JOIN CUSTOMERS c ON o.Customer_ID = c.Customer_ID
                         LEFT JOIN WAREHOUSES w ON o.Warehouse_ID = w.Warehouse_ID
                         WHERE o.Order_ID = @Order_ID";
            var dataTable = ExecuteQuery(query, new MySqlParameter[] { new MySqlParameter("@Order_ID", id) });
            if (dataTable.Rows.Count == 0) return null;
            
            var order = MapToEntity(dataTable.Rows[0]);
            // Load order details
            order.OrderDetails = new OrderDetailRepository(_context).GetByOrderId(id).ToList();
            return order;
        }
        
        public override IEnumerable<Order> GetAll()
        {
            var query = @"SELECT o.*, c.Customer_Name, c.Tier_ID, w.Location 
                         FROM ORDERS o 
                         LEFT JOIN CUSTOMERS c ON o.Customer_ID = c.Customer_ID
                         LEFT JOIN WAREHOUSES w ON o.Warehouse_ID = w.Warehouse_ID
                         ORDER BY o.Order_Date DESC";
            var dataTable = ExecuteQuery(query);
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<Order> Find(Expression<Func<Order, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }
        
        public override void Add(Order entity)
        {
            ExecuteNonQuery(@"INSERT INTO ORDERS (Order_ID, Customer_ID, Warehouse_ID, Order_Date, Total_Amount, Status, Created_Date, Modified_Date, Created_By)
                            VALUES (@Order_ID, @Customer_ID, @Warehouse_ID, @Order_Date, @Total_Amount, @Status, @Created_Date, @Modified_Date, @Created_By)",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Order_ID", entity.Order_ID),
                    new MySqlParameter("@Customer_ID", entity.Customer_ID),
                    new MySqlParameter("@Warehouse_ID", entity.Warehouse_ID),
                    new MySqlParameter("@Order_Date", entity.Order_Date),
                    new MySqlParameter("@Total_Amount", entity.Total_Amount),
                    new MySqlParameter("@Status", entity.Status),
                    new MySqlParameter("@Created_Date", DateTime.Now),
                    new MySqlParameter("@Modified_Date", DateTime.Now),
                    new MySqlParameter("@Created_By", (object)entity.Created_By ?? DBNull.Value)
                });
        }
        
        public override void Update(Order entity)
        {
            ExecuteNonQuery(@"UPDATE ORDERS SET Customer_ID = @Customer_ID, Warehouse_ID = @Warehouse_ID, 
                            Order_Date = @Order_Date, Total_Amount = @Total_Amount, Status = @Status, 
                            Modified_Date = @Modified_Date WHERE Order_ID = @Order_ID",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Order_ID", entity.Order_ID),
                    new MySqlParameter("@Customer_ID", entity.Customer_ID),
                    new MySqlParameter("@Warehouse_ID", entity.Warehouse_ID),
                    new MySqlParameter("@Order_Date", entity.Order_Date),
                    new MySqlParameter("@Total_Amount", entity.Total_Amount),
                    new MySqlParameter("@Status", entity.Status),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Delete(Order entity) => DeleteById(entity.Order_ID);
        public override void DeleteById(string id) => ExecuteNonQuery("DELETE FROM ORDERS WHERE Order_ID = @Order_ID",
            new MySqlParameter[] { new MySqlParameter("@Order_ID", id) });
        public override bool Exists(string id) => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM ORDERS WHERE Order_ID = @Order_ID",
            new MySqlParameter[] { new MySqlParameter("@Order_ID", id) }).Rows[0][0]) > 0;
        public override int Count() => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM ORDERS").Rows[0][0]);
        
        /// <summary>
        /// Get count of orders by status
        /// </summary>
        public Dictionary<string, int> GetOrderCountsByStatus()
        {
            var query = "SELECT Status, COUNT(*) as Count FROM ORDERS GROUP BY Status";
            var dataTable = ExecuteQuery(query);
            var result = new Dictionary<string, int>();
            foreach (DataRow row in dataTable.Rows)
            {
                result[row["Status"].ToString()] = Convert.ToInt32(row["Count"]);
            }
            return result;
        }
        
        /// <summary>
        /// Get recent orders (last N orders)
        /// </summary>
        public IEnumerable<Order> GetRecentOrders(int count = 10)
        {
            var query = @"SELECT o.*, c.Customer_Name, c.Tier_ID, w.Location 
                         FROM ORDERS o 
                         LEFT JOIN CUSTOMERS c ON o.Customer_ID = c.Customer_ID
                         LEFT JOIN WAREHOUSES w ON o.Warehouse_ID = w.Warehouse_ID
                         ORDER BY o.Order_Date DESC LIMIT @Count";
            var dataTable = ExecuteQuery(query, new MySqlParameter[] { new MySqlParameter("@Count", count) });
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        /// <summary>
        /// Get orders with filters
        /// </summary>
        public IEnumerable<Order> GetOrdersWithFilters(DateTime? startDate = null, DateTime? endDate = null, string status = null, string customerId = null)
        {
            var query = @"SELECT o.*, c.Customer_Name, c.Tier_ID, w.Location 
                         FROM ORDERS o 
                         LEFT JOIN CUSTOMERS c ON o.Customer_ID = c.Customer_ID
                         LEFT JOIN WAREHOUSES w ON o.Warehouse_ID = w.Warehouse_ID
                         WHERE 1=1";
            var parameters = new List<MySqlParameter>();
            
            if (startDate.HasValue)
            {
                query += " AND o.Order_Date >= @StartDate";
                parameters.Add(new MySqlParameter("@StartDate", startDate.Value));
            }
            
            if (endDate.HasValue)
            {
                query += " AND o.Order_Date <= @EndDate";
                parameters.Add(new MySqlParameter("@EndDate", endDate.Value));
            }
            
            if (!string.IsNullOrEmpty(status))
            {
                query += " AND o.Status = @Status";
                parameters.Add(new MySqlParameter("@Status", status));
            }
            
            if (!string.IsNullOrEmpty(customerId))
            {
                query += " AND o.Customer_ID = @Customer_ID";
                parameters.Add(new MySqlParameter("@Customer_ID", customerId));
            }
            
            query += " ORDER BY o.Order_Date DESC";
            
            var dataTable = ExecuteQuery(query, parameters.ToArray());
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        private Order MapToEntity(DataRow row) => new Order
        {
            Order_ID = row["Order_ID"].ToString(),
            Customer_ID = row["Customer_ID"].ToString(),
            Warehouse_ID = row["Warehouse_ID"].ToString(),
            Order_Date = Convert.ToDateTime(row["Order_Date"]),
            Total_Amount = Convert.ToDecimal(row["Total_Amount"]),
            Status = row["Status"].ToString(),
            Created_Date = Convert.ToDateTime(row["Created_Date"]),
            Modified_Date = Convert.ToDateTime(row["Modified_Date"]),
            Created_By = row["Created_By"] != DBNull.Value ? row["Created_By"].ToString() : null,
            Customer = row["Customer_Name"] != DBNull.Value ? new Customer { Customer_Name = row["Customer_Name"].ToString() } : null,
            Warehouse = row["Location"] != DBNull.Value ? new Warehouse { Location = row["Location"].ToString() } : null
        };
    }
}

