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
    public class PriceListRepository : Repository<PriceList>
    {
        public PriceListRepository(DatabaseContext context) : base(context, "PRICE_LIST") { }
        
        public override PriceList GetById(string id)
        {
            var dataTable = ExecuteQuery("SELECT * FROM PRICE_LIST WHERE Price_ID = @Price_ID",
                new MySqlParameter[] { new MySqlParameter("@Price_ID", id) });
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public override IEnumerable<PriceList> GetAll()
        {
            var dataTable = ExecuteQuery("SELECT * FROM PRICE_LIST ORDER BY Effective_Date DESC");
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<PriceList> Find(Expression<Func<PriceList, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }
        
        public PriceList GetActivePrice(string productId, string tierId, DateTime date)
        {
            var query = @"SELECT * FROM PRICE_LIST 
                         WHERE Product_ID = @Product_ID AND Tier_ID = @Tier_ID 
                         AND Effective_Date <= @Date 
                         AND (Expiry_Date IS NULL OR Expiry_Date >= @Date)
                         ORDER BY Effective_Date DESC LIMIT 1";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@Product_ID", productId),
                new MySqlParameter("@Tier_ID", tierId),
                new MySqlParameter("@Date", date)
            };
            var dataTable = ExecuteQuery(query, parameters);
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public override void Add(PriceList entity)
        {
            ExecuteNonQuery(@"INSERT INTO PRICE_LIST (Price_ID, Product_ID, Tier_ID, Unit_Price, Effective_Date, Expiry_Date, Created_Date, Modified_Date)
                            VALUES (@Price_ID, @Product_ID, @Tier_ID, @Unit_Price, @Effective_Date, @Expiry_Date, @Created_Date, @Modified_Date)",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Price_ID", entity.Price_ID),
                    new MySqlParameter("@Product_ID", entity.Product_ID),
                    new MySqlParameter("@Tier_ID", entity.Tier_ID),
                    new MySqlParameter("@Unit_Price", entity.Unit_Price),
                    new MySqlParameter("@Effective_Date", entity.Effective_Date),
                    new MySqlParameter("@Expiry_Date", (object)entity.Expiry_Date ?? DBNull.Value),
                    new MySqlParameter("@Created_Date", DateTime.Now),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Update(PriceList entity)
        {
            ExecuteNonQuery(@"UPDATE PRICE_LIST SET Product_ID = @Product_ID, Tier_ID = @Tier_ID, Unit_Price = @Unit_Price,
                            Effective_Date = @Effective_Date, Expiry_Date = @Expiry_Date, Modified_Date = @Modified_Date
                            WHERE Price_ID = @Price_ID",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Price_ID", entity.Price_ID),
                    new MySqlParameter("@Product_ID", entity.Product_ID),
                    new MySqlParameter("@Tier_ID", entity.Tier_ID),
                    new MySqlParameter("@Unit_Price", entity.Unit_Price),
                    new MySqlParameter("@Effective_Date", entity.Effective_Date),
                    new MySqlParameter("@Expiry_Date", (object)entity.Expiry_Date ?? DBNull.Value),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Delete(PriceList entity) => DeleteById(entity.Price_ID);
        public override void DeleteById(string id) => ExecuteNonQuery("DELETE FROM PRICE_LIST WHERE Price_ID = @Price_ID",
            new MySqlParameter[] { new MySqlParameter("@Price_ID", id) });
        public override bool Exists(string id) => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM PRICE_LIST WHERE Price_ID = @Price_ID",
            new MySqlParameter[] { new MySqlParameter("@Price_ID", id) }).Rows[0][0]) > 0;
        public override int Count() => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM PRICE_LIST").Rows[0][0]);
        
        private PriceList MapToEntity(DataRow row) => new PriceList
        {
            Price_ID = row["Price_ID"].ToString(),
            Product_ID = row["Product_ID"].ToString(),
            Tier_ID = row["Tier_ID"].ToString(),
            Unit_Price = Convert.ToDecimal(row["Unit_Price"]),
            Effective_Date = Convert.ToDateTime(row["Effective_Date"]),
            Expiry_Date = row["Expiry_Date"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["Expiry_Date"]) : null,
            Created_Date = Convert.ToDateTime(row["Created_Date"]),
            Modified_Date = Convert.ToDateTime(row["Modified_Date"])
        };
    }
}

