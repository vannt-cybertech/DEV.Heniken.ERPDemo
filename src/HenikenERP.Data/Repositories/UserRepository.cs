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
    /// User repository implementation
    /// </summary>
    public class UserRepository : Repository<User>
    {
        public UserRepository(DatabaseContext context) : base(context, "USERS")
        {
        }
        
        public override User GetById(string id)
        {
            string query = "SELECT * FROM USERS WHERE User_ID = @User_ID";
            var dataTable = ExecuteQuery(query, new MySqlParameter[] { new MySqlParameter("@User_ID", id) });
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public User GetByUsername(string username)
        {
            string query = "SELECT * FROM USERS WHERE Username = @Username";
            var dataTable = ExecuteQuery(query, new MySqlParameter[] { new MySqlParameter("@Username", username) });
            return dataTable.Rows.Count > 0 ? MapToEntity(dataTable.Rows[0]) : null;
        }
        
        public override IEnumerable<User> GetAll()
        {
            string query = "SELECT * FROM USERS ORDER BY Username";
            var dataTable = ExecuteQuery(query);
            return dataTable.Rows.Cast<DataRow>().Select(MapToEntity);
        }
        
        public override IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
        {
            return GetAll().Where(predicate.Compile());
        }
        
        public override void Add(User entity)
        {
            string query = @"INSERT INTO USERS (User_ID, Username, Password_Hash, Role, Is_Active, Created_Date, Modified_Date)
                           VALUES (@User_ID, @Username, @Password_Hash, @Role, @Is_Active, @Created_Date, @Modified_Date)";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@User_ID", entity.User_ID),
                new MySqlParameter("@Username", entity.Username),
                new MySqlParameter("@Password_Hash", entity.Password_Hash),
                new MySqlParameter("@Role", entity.Role),
                new MySqlParameter("@Is_Active", entity.Is_Active),
                new MySqlParameter("@Created_Date", DateTime.Now),
                new MySqlParameter("@Modified_Date", DateTime.Now)
            };
            ExecuteNonQuery(query, parameters);
        }
        
        public override void Update(User entity)
        {
            string query = @"UPDATE USERS SET Username = @Username, Password_Hash = @Password_Hash, 
                           Role = @Role, Is_Active = @Is_Active, Modified_Date = @Modified_Date,
                           Last_Login = @Last_Login WHERE User_ID = @User_ID";
            var parameters = new MySqlParameter[]
            {
                new MySqlParameter("@User_ID", entity.User_ID),
                new MySqlParameter("@Username", entity.Username),
                new MySqlParameter("@Password_Hash", entity.Password_Hash),
                new MySqlParameter("@Role", entity.Role),
                new MySqlParameter("@Is_Active", entity.Is_Active),
                new MySqlParameter("@Last_Login", (object)entity.Last_Login ?? DBNull.Value),
                new MySqlParameter("@Modified_Date", DateTime.Now)
            };
            ExecuteNonQuery(query, parameters);
        }
        
        public override void Delete(User entity) => DeleteById(entity.User_ID);
        
        public override void DeleteById(string id)
        {
            ExecuteNonQuery("DELETE FROM USERS WHERE User_ID = @User_ID",
                new MySqlParameter[] { new MySqlParameter("@User_ID", id) });
        }
        
        public override bool Exists(string id)
        {
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM USERS WHERE User_ID = @User_ID", _context.Connection))
            {
                cmd.Parameters.AddWithValue("@User_ID", id);
                return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
            }
        }
        
        public override int Count()
        {
            using (var cmd = new MySqlCommand("SELECT COUNT(*) FROM USERS", _context.Connection))
                return Convert.ToInt32(cmd.ExecuteScalar());
        }
        
        private User MapToEntity(DataRow row)
        {
            return new User
            {
                User_ID = row["User_ID"].ToString(),
                Username = row["Username"].ToString(),
                Password_Hash = row["Password_Hash"].ToString(),
                Role = row["Role"].ToString(),
                Is_Active = Convert.ToBoolean(row["Is_Active"]),
                Created_Date = Convert.ToDateTime(row["Created_Date"]),
                Modified_Date = Convert.ToDateTime(row["Modified_Date"]),
                Last_Login = row["Last_Login"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["Last_Login"]) : null
            };
        }
    }
}

