using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using MySql.Data.MySqlClient;
using HenikenERP.Core.Entities;
using HenikenERP.Core.Interfaces;
using HenikenERP.Data.Context;

namespace HenikenERP.Data.Repositories
{
    /// <summary>
    /// Product repository implementation
    /// </summary>
    public class ProductRepository : Repository<Product>, IRepository<Product>
    {
        public ProductRepository(DatabaseContext context) : base(context, "PRODUCTS")
        {
        }
        
        public override Product GetById(string id)
        {
            string query = "SELECT * FROM PRODUCTS WHERE Product_ID = @Product_ID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Product_ID", id)
            };
            
            var dataTable = ExecuteQuery(query, parameters);
            if (dataTable.Rows.Count > 0)
            {
                return MapToEntity(dataTable.Rows[0]);
            }
            return null;
        }
        
        public override IEnumerable<Product> GetAll()
        {
            string query = "SELECT * FROM PRODUCTS ORDER BY Product_Name";
            var dataTable = ExecuteQuery(query);
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<Product> Find(Expression<Func<Product, bool>> predicate)
        {
            // Simple implementation - in production, use proper query translation
            return GetAll().Where(predicate.Compile());
        }
        
        public override void Add(Product entity)
        {
            string query = @"INSERT INTO PRODUCTS (Product_ID, Product_Name, Unit, Category_ID, Created_Date, Modified_Date)
                           VALUES (@Product_ID, @Product_Name, @Unit, @Category_ID, @Created_Date, @Modified_Date)";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Product_ID", entity.Product_ID),
                new MySqlParameter("@Product_Name", entity.Product_Name),
                new MySqlParameter("@Unit", (object)entity.Unit ?? DBNull.Value),
                new MySqlParameter("@Category_ID", (object)entity.Category_ID ?? DBNull.Value),
                new MySqlParameter("@Created_Date", DateTime.Now),
                new MySqlParameter("@Modified_Date", DateTime.Now)
            };
            
            ExecuteNonQuery(query, parameters);
        }
        
        public override void Update(Product entity)
        {
            string query = @"UPDATE PRODUCTS SET Product_Name = @Product_Name, Unit = @Unit, 
                           Category_ID = @Category_ID, Modified_Date = @Modified_Date
                           WHERE Product_ID = @Product_ID";
            
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Product_ID", entity.Product_ID),
                new MySqlParameter("@Product_Name", entity.Product_Name),
                new MySqlParameter("@Unit", (object)entity.Unit ?? DBNull.Value),
                new MySqlParameter("@Category_ID", (object)entity.Category_ID ?? DBNull.Value),
                new MySqlParameter("@Modified_Date", DateTime.Now)
            };
            
            ExecuteNonQuery(query, parameters);
        }
        
        public override void Delete(Product entity)
        {
            DeleteById(entity.Product_ID);
        }
        
        public override void DeleteById(string id)
        {
            string query = "DELETE FROM PRODUCTS WHERE Product_ID = @Product_ID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Product_ID", id)
            };
            
            ExecuteNonQuery(query, parameters);
        }
        
        public override bool Exists(string id)
        {
            string query = "SELECT COUNT(*) FROM PRODUCTS WHERE Product_ID = @Product_ID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Product_ID", id)
            };
            
            using (var command = new MySqlCommand(query, _context.Connection))
            {
                command.Parameters.AddRange(parameters);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
        
        public override int Count()
        {
            string query = "SELECT COUNT(*) FROM PRODUCTS";
            using (var command = new MySqlCommand(query, _context.Connection))
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }
        
        private Product MapToEntity(DataRow row)
        {
            return new Product
            {
                Product_ID = row["Product_ID"].ToString(),
                Product_Name = row["Product_Name"].ToString(),
                Unit = row["Unit"] != DBNull.Value ? row["Unit"].ToString() : null,
                Category_ID = row["Category_ID"] != DBNull.Value ? row["Category_ID"].ToString() : null,
                Created_Date = Convert.ToDateTime(row["Created_Date"]),
                Modified_Date = Convert.ToDateTime(row["Modified_Date"])
            };
        }
    }
}

