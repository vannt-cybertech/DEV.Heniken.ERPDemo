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
    /// <summary>
    /// Customer repository implementation
    /// </summary>
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(DatabaseContext context) : base(context, "CUSTOMERS")
        {
        }
        
        public override Customer GetById(string id)
        {
            string query = @"SELECT c.*, ct.Tier_Name, ct.Discount_Rate 
                           FROM CUSTOMERS c 
                           LEFT JOIN CUSTOMER_TIERS ct ON c.Tier_ID = ct.Tier_ID 
                           WHERE c.Customer_ID = @Customer_ID";
            var parameters = new MySqlParameter[] { new MySqlParameter("@Customer_ID", id) };
            var dataTable = ExecuteQuery(query, parameters);
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public override IEnumerable<Customer> GetAll()
        {
            string query = @"SELECT c.*, ct.Tier_Name, ct.Discount_Rate 
                           FROM CUSTOMERS c 
                           LEFT JOIN CUSTOMER_TIERS ct ON c.Tier_ID = ct.Tier_ID 
                           ORDER BY c.Customer_Name";
            var dataTable = ExecuteQuery(query);
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }
        
        public override void Add(Customer entity)
        {
            string query = @"INSERT INTO CUSTOMERS (Customer_ID, Customer_Name, Tier_ID, Phone_Number, Is_Active, Created_Date, Modified_Date)
                           VALUES (@Customer_ID, @Customer_Name, @Tier_ID, @Phone_Number, @Is_Active, @Created_Date, @Modified_Date)";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Customer_ID", entity.Customer_ID),
                new MySqlParameter("@Customer_Name", entity.Customer_Name),
                new MySqlParameter("@Tier_ID", (object)entity.Tier_ID ?? DBNull.Value),
                new MySqlParameter("@Phone_Number", (object)entity.Phone_Number ?? DBNull.Value),
                new MySqlParameter("@Is_Active", entity.Is_Active),
                new MySqlParameter("@Created_Date", DateTime.Now),
                new MySqlParameter("@Modified_Date", DateTime.Now)
            };
            ExecuteNonQuery(query, parameters);
        }
        
        public override void Update(Customer entity)
        {
            string query = @"UPDATE CUSTOMERS SET Customer_Name = @Customer_Name, Tier_ID = @Tier_ID, 
                           Phone_Number = @Phone_Number, Is_Active = @Is_Active, Modified_Date = @Modified_Date
                           WHERE Customer_ID = @Customer_ID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Customer_ID", entity.Customer_ID),
                new MySqlParameter("@Customer_Name", entity.Customer_Name),
                new MySqlParameter("@Tier_ID", (object)entity.Tier_ID ?? DBNull.Value),
                new MySqlParameter("@Phone_Number", (object)entity.Phone_Number ?? DBNull.Value),
                new MySqlParameter("@Is_Active", entity.Is_Active),
                new MySqlParameter("@Modified_Date", DateTime.Now)
            };
            ExecuteNonQuery(query, parameters);
        }
        
        public override void Delete(Customer entity) => DeleteById(entity.Customer_ID);
        
        public override void DeleteById(string id)
        {
            ExecuteNonQuery("DELETE FROM CUSTOMERS WHERE Customer_ID = @Customer_ID",
                new MySqlParameter[] { new MySqlParameter("@Customer_ID", id) });
        }
        
        public override bool Exists(string id)
        {
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM CUSTOMERS WHERE Customer_ID = @Customer_ID", _context.Connection))
            {
                cmd.Parameters.AddWithValue("@Customer_ID", id);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        
        public override int Count()
        {
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM CUSTOMERS", _context.Connection))
                return Convert.ToInt32(cmd.ExecuteScalar());
        }
        
        private Customer MapToEntity(DataRow row)
        {
            return new Customer
            {
                Customer_ID = row["Customer_ID"].ToString(),
                Customer_Name = row["Customer_Name"].ToString(),
                Tier_ID = row["Tier_ID"] != DBNull.Value ? row["Tier_ID"].ToString() : null,
                Phone_Number = row["Phone_Number"] != DBNull.Value ? row["Phone_Number"].ToString() : null,
                Is_Active = row["Is_Active"] != DBNull.Value ? Convert.ToBoolean(row["Is_Active"]) : true,
                Created_Date = Convert.ToDateTime(row["Created_Date"]),
                Modified_Date = Convert.ToDateTime(row["Modified_Date"]),
                Tier = row["Tier_Name"] != DBNull.Value ? new CustomerTier
                {
                    Tier_ID = row["Tier_ID"] != DBNull.Value ? row["Tier_ID"].ToString() : null,
                    Tier_Name = row["Tier_Name"].ToString(),
                    Discount_Rate = row["Discount_Rate"] != DBNull.Value ? Convert.ToDecimal(row["Discount_Rate"]) : 0
                } : null
            };
        }
    }
}

