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
    public class AppSettingRepository : Repository<AppSetting>
    {
        public AppSettingRepository(DatabaseContext context) : base(context, "APP_SETTINGS") { }
        
        public override AppSetting GetById(string id)
        {
            var dataTable = ExecuteQuery("SELECT * FROM APP_SETTINGS WHERE Setting_Key = @Setting_Key",
                new MySqlParameter[] { new MySqlParameter("@Setting_Key", id) });
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public override IEnumerable<AppSetting> GetAll()
        {
            var dataTable = ExecuteQuery("SELECT * FROM APP_SETTINGS ORDER BY Setting_Key");
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<AppSetting> Find(Expression<Func<AppSetting, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }
        
        public override void Add(AppSetting entity)
        {
            ExecuteNonQuery(@"INSERT INTO APP_SETTINGS (Setting_Key, Setting_Value, Description, Modified_Date)
                            VALUES (@Setting_Key, @Setting_Value, @Description, @Modified_Date)",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Setting_Key", entity.Setting_Key),
                    new MySqlParameter("@Setting_Value", (object)entity.Setting_Value ?? DBNull.Value),
                    new MySqlParameter("@Description", (object)entity.Description ?? DBNull.Value),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Update(AppSetting entity)
        {
            ExecuteNonQuery(@"UPDATE APP_SETTINGS SET Setting_Value = @Setting_Value, Description = @Description, 
                            Modified_Date = @Modified_Date WHERE Setting_Key = @Setting_Key",
                new MySqlParameter[]
                {
                    new MySqlParameter("@Setting_Key", entity.Setting_Key),
                    new MySqlParameter("@Setting_Value", (object)entity.Setting_Value ?? DBNull.Value),
                    new MySqlParameter("@Description", (object)entity.Description ?? DBNull.Value),
                    new MySqlParameter("@Modified_Date", DateTime.Now)
                });
        }
        
        public override void Delete(AppSetting entity) => DeleteById(entity.Setting_Key);
        public override void DeleteById(string id) => ExecuteNonQuery("DELETE FROM APP_SETTINGS WHERE Setting_Key = @Setting_Key",
            new MySqlParameter[] { new MySqlParameter("@Setting_Key", id) });
        public override bool Exists(string id) => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM APP_SETTINGS WHERE Setting_Key = @Setting_Key",
            new MySqlParameter[] { new MySqlParameter("@Setting_Key", id) }).Rows[0][0]) > 0;
        public override int Count() => Convert.ToInt32(ExecuteQuery("SELECT COUNT(*) FROM APP_SETTINGS").Rows[0][0]);
        
        private AppSetting MapToEntity(DataRow row) => new AppSetting
        {
            Setting_Key = row["Setting_Key"].ToString(),
            Setting_Value = row["Setting_Value"] != DBNull.Value ? row["Setting_Value"].ToString() : null,
            Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
            Modified_Date = Convert.ToDateTime(row["Modified_Date"])
        };
    }
}

