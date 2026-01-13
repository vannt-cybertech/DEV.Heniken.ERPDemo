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
    public class OrderDetailRepository : Repository<OrderDetail>
    {
        public OrderDetailRepository(DatabaseContext context) : base(context, "ORDER_DETAILS") { }
        
        public override OrderDetail GetById(string id)
        {
            var dataTable = ExecuteQuery("SELECT * FROM ORDER_DETAILS WHERE OrderDetail_ID = @OrderDetail_ID",
                new MySqlParameter[] { new MySqlParameter("@OrderDetail_ID", id) });
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public IEnumerable<OrderDetail> GetByOrderId(string orderId)
        {
            var query = @"SELECT od.*, p.Product_Name 
                         FROM ORDER_DETAILS od 
                         LEFT JOIN PRODUCTS p ON od.Product_ID = p.Product_ID
                         WHERE od.Order_ID = @Order_ID";
            var dataTable = ExecuteQuery(query, new MySqlParameter[] { new MySqlParameter("@Order_ID", orderId) });
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<OrderDetail> GetAll()
        {
            var dataTable = ExecuteQuery("SELECT * FROM ORDER_DETAILS ORDER BY Order_ID, OrderDetail_ID");
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<OrderDetail> Find(Expression<Func<OrderDetail, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }
        
        public override void Add(OrderDetail entity)
        {
            ExecuteNonQuery(@"INSERT INTO ORDER_DETAILS (OrderDetail_ID, Order_ID, Product_ID, Quantity, Applied_Price, Created_Date, Modified_Date)
                            VALUES (@OrderDetail_ID, @Order_ID, @Product_ID, @Quantity, @Applied_Price, @Created_Date, @Modified_Date)",
                new MySqlParameter[]
                {
                    new MySqlParameter("@OrderDetail_ID", entity.OrderDetail_ID),
                    new MySqlParameter("@Order_ID", entity.Order_ID),
                    new MySqlParameter("@Product_ID", entity.Product_ID),
                    new MySqlParameter("@Quantity", entity.Quantity),
                    new MySqlParameter("@Applied_Price", entity.Applied_Price),
                    new MySqlParameter("@Created_Date", DateTime.Now),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Update(OrderDetail entity)
        {
            ExecuteNonQuery(@"UPDATE ORDER_DETAILS SET Product_ID = @Product_ID, Quantity = @Quantity, 
                            Applied_Price = @Applied_Price, Modified_Date = @Modified_Date
                            WHERE OrderDetail_ID = @OrderDetail_ID",
                new MySqlParameter[]
                {
                    new MySqlParameter("@OrderDetail_ID", entity.OrderDetail_ID),
                    new MySqlParameter("@Product_ID", entity.Product_ID),
                    new MySqlParameter("@Quantity", entity.Quantity),
                    new MySqlParameter("@Applied_Price", entity.Applied_Price),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Delete(OrderDetail entity) => DeleteById(entity.OrderDetail_ID);
        public override void DeleteById(string id) => ExecuteNonQuery("DELETE FROM ORDER_DETAILS WHERE OrderDetail_ID = @OrderDetail_ID",
            new MySqlParameter[] { new MySqlParameter("@OrderDetail_ID", id) });
        public override bool Exists(string id) => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM ORDER_DETAILS WHERE OrderDetail_ID = @OrderDetail_ID",
            new MySqlParameter[] { new MySqlParameter("@OrderDetail_ID", id) }).Rows[0][0]) > 0;
        public override int Count() => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM ORDER_DETAILS").Rows[0][0]);
        
        private OrderDetail MapToEntity(DataRow row) => new OrderDetail
        {
            OrderDetail_ID = row["OrderDetail_ID"].ToString(),
            Order_ID = row["Order_ID"].ToString(),
            Product_ID = row["Product_ID"].ToString(),
            Quantity = Convert.ToInt32(row["Quantity"]),
            Applied_Price = Convert.ToDecimal(row["Applied_Price"]),
            Created_Date = Convert.ToDateTime(row["Created_Date"]),
            Modified_Date = Convert.ToDateTime(row["Modified_Date"]),
            Product = row["Product_Name"] != DBNull.Value ? new Product { Product_Name = row["Product_Name"].ToString() } : null
        };
    }
}

