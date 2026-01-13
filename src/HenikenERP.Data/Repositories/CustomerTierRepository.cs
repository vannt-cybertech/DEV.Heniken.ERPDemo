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
    public class CustomerTierRepository : Repository<CustomerTier>
    {
        public CustomerTierRepository(DatabaseContext context) : base(context, "CUSTOMER_TIERS") { }
        
        public override CustomerTier GetById(string id)
        {
            var dataTable = ExecuteQuery("SELECT * FROM CUSTOMER_TIERS WHERE Tier_ID = @Tier_ID",
                new MySqlParameter[] { new MySqlParameter("@Tier_ID", id) });
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public override IEnumerable<CustomerTier> GetAll()
        {
            var dataTable = ExecuteQuery("SELECT * FROM CUSTOMER_TIERS ORDER BY Tier_Name");
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<CustomerTier> Find(Expression<Func<CustomerTier, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }
        
        public override void Add(CustomerTier entity)
        {
            ExecuteNonQuery(@"INSERT INTO CUSTOMER_TIERS (Tier_ID, Tier_Name, Discount_Rate, Description, Created_Date, Modified_Date)
                            VALUES (@Tier_ID, @Tier_Name, @Discount_Rate, @Description, @Created_Date, @Modified_Date)",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Tier_ID", entity.Tier_ID),
                    new MySqlParameter("@Tier_Name", entity.Tier_Name),
                    new MySqlParameter("@Discount_Rate", entity.Discount_Rate),
                    new MySqlParameter("@Description", (object)entity.Description ?? DBNull.Value),
                    new MySqlParameter("@Created_Date", DateTime.Now),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Update(CustomerTier entity)
        {
            ExecuteNonQuery(@"UPDATE CUSTOMER_TIERS SET Tier_Name = @Tier_Name, Discount_Rate = @Discount_Rate, 
                            Description = @Description, Modified_Date = @Modified_Date WHERE Tier_ID = @Tier_ID",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Tier_ID", entity.Tier_ID),
                    new MySqlParameter("@Tier_Name", entity.Tier_Name),
                    new MySqlParameter("@Discount_Rate", entity.Discount_Rate),
                    new MySqlParameter("@Description", (object)entity.Description ?? DBNull.Value),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Delete(CustomerTier entity) => DeleteById(entity.Tier_ID);
        public override void DeleteById(string id) => ExecuteNonQuery("DELETE FROM CUSTOMER_TIERS WHERE Tier_ID = @Tier_ID",
            new MySqlParameter[] { new MySqlParameter("@Tier_ID", id) });
        public override bool Exists(string id) => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM CUSTOMER_TIERS WHERE Tier_ID = @Tier_ID",
            new MySqlParameter[] { new MySqlParameter("@Tier_ID", id) }).Rows[0][0]) > 0;
        public override int Count() => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM CUSTOMER_TIERS").Rows[0][0]);
        
        private CustomerTier MapToEntity(DataRow row) => new CustomerTier
        {
            Tier_ID = row["Tier_ID"].ToString(),
            Tier_Name = row["Tier_Name"].ToString(),
            Discount_Rate = Convert.ToDecimal(row["Discount_Rate"]),
            Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
            Created_Date = Convert.ToDateTime(row["Created_Date"]),
            Modified_Date = Convert.ToDateTime(row["Modified_Date"])
        };
    }
}

