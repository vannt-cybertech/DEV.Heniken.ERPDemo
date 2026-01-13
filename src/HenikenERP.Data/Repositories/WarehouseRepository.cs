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
    public class WarehouseRepository : Repository<Warehouse>
    {
        public WarehouseRepository(DatabaseContext context) : base(context, "WAREHOUSES") { }
        
        public override Warehouse GetById(string id)
        {
            var dataTable = ExecuteQuery("SELECT * FROM WAREHOUSES WHERE Warehouse_ID = @Warehouse_ID",
                new MySqlParameter[] { new MySqlParameter("@Warehouse_ID", id) });
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public override IEnumerable<Warehouse> GetAll()
        {
            var dataTable = ExecuteQuery("SELECT * FROM WAREHOUSES ORDER BY Location");
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<Warehouse> Find(Expression<Func<Warehouse, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }
        
        public override void Add(Warehouse entity)
        {
            ExecuteNonQuery(@"INSERT INTO WAREHOUSES (Warehouse_ID, Location, Capacity, Created_Date, Modified_Date)
                            VALUES (@Warehouse_ID, @Location, @Capacity, @Created_Date, @Modified_Date)",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Warehouse_ID", entity.Warehouse_ID),
                    new MySqlParameter("@Location", entity.Location),
                    new MySqlParameter("@Capacity", entity.Capacity),
                    new MySqlParameter("@Created_Date", DateTime.Now),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Update(Warehouse entity)
        {
            ExecuteNonQuery(@"UPDATE WAREHOUSES SET Location = @Location, Capacity = @Capacity, Modified_Date = @Modified_Date
                            WHERE Warehouse_ID = @Warehouse_ID",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Warehouse_ID", entity.Warehouse_ID),
                    new MySqlParameter("@Location", entity.Location),
                    new MySqlParameter("@Capacity", entity.Capacity),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Delete(Warehouse entity) => DeleteById(entity.Warehouse_ID);
        public override void DeleteById(string id) => ExecuteNonQuery("DELETE FROM WAREHOUSES WHERE Warehouse_ID = @Warehouse_ID",
            new MySqlParameter[] { new MySqlParameter("@Warehouse_ID", id) });
        public override bool Exists(string id) => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM WAREHOUSES WHERE Warehouse_ID = @Warehouse_ID",
            new MySqlParameter[] { new MySqlParameter("@Warehouse_ID", id) }).Rows[0][0]) > 0;
        public override int Count() => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM WAREHOUSES").Rows[0][0]);
        
        private Warehouse MapToEntity(DataRow row) => new Warehouse
        {
            Warehouse_ID = row["Warehouse_ID"].ToString(),
            Location = row["Location"].ToString(),
            Capacity = Convert.ToInt32(row["Capacity"]),
            Created_Date = Convert.ToDateTime(row["Created_Date"]),
            Modified_Date = Convert.ToDateTime(row["Modified_Date"])
        };
    }
}

