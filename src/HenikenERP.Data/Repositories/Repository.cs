using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using MySql.Data.MySqlClient;
using HenikenERP.Core.Interfaces;
using HenikenERP.Data.Context;

namespace HenikenERP.Data.Repositories
{
    /// <summary>
    /// Generic repository implementation
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DatabaseContext _context;
        protected string _tableName;
        
        public Repository(DatabaseContext context, string tableName)
        {
            _context = context;
            _tableName = tableName;
        }
        
        public abstract T GetById(string id);
        public abstract IEnumerable<T> GetAll();
        public abstract IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        public abstract void Add(T entity);
        public abstract void Update(T entity);
        public abstract void Delete(T entity);
        public abstract void DeleteById(string id);
        public abstract bool Exists(string id);
        public abstract int Count();
        
        /// <summary>
        /// Execute SQL query and return DataTable
        /// </summary>
        protected DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            using (var command = new MySqlCommand(query, _context.Connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                
                using (var adapter = new MySqlDataAdapter(command))
                {
                    var dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }
        
        /// <summary>
        /// Execute non-query SQL command
        /// </summary>
        protected int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            using (var command = new MySqlCommand(query, _context.Connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteNonQuery();
            }
        }
    }
}

