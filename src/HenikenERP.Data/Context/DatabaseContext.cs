using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using HenikenERP.Common.Helpers;

namespace HenikenERP.Data.Context
{
    /// <summary>
    /// Database context for managing MySQL connections
    /// </summary>
    public class DatabaseContext : IDisposable
    {
        private MySqlConnection _connection;
        private bool _disposed = false;

        /// <summary>
        /// Get current database name for diagnostics.
        /// </summary>
        public string TryGetDatabaseName()
        {
            try
            {
                using (var cmd = new MySqlCommand("SELECT DATABASE()", Connection))
                {
                    return cmd.ExecuteScalar()?.ToString() ?? string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to get current database name.", ex);
                return string.Empty;
            }
        }
        
        /// <summary>
        /// Get database connection
        /// </summary>
        public MySqlConnection Connection
        {
            get
            {
                if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
                {
                    _connection = CreateConnection();
                }
                return _connection;
            }
        }
        
        /// <summary>
        /// Create new database connection
        /// </summary>
        private MySqlConnection CreateConnection()
        {
            string connectionString = GetConnectionString();
            
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Database connection string is not configured. Please configure it in Settings.");
            }
            
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
        
        /// <summary>
        /// Get connection string from configuration
        /// </summary>
        private string GetConnectionString()
        {
            try
            {
                // Try to get from App.config
                string encryptedConnectionString = ConfigurationManager.AppSettings["DatabaseConnectionString"];
                
                if (string.IsNullOrEmpty(encryptedConnectionString))
                {
                    return string.Empty;
                }
                
                // Decrypt connection string
                return EncryptionHelper.DecryptConnectionString(encryptedConnectionString);
            }
            catch
            {
                return string.Empty;
            }
        }
        
        /// <summary>
        /// Test database connection
        /// </summary>
        public bool TestConnection(string connectionString = null)
        {
            try
            {
                string connString = connectionString ?? GetConnectionString();
                
                if (string.IsNullOrEmpty(connString))
                {
                    return false;
                }
                
                using (var testConnection = new MySqlConnection(connString))
                {
                    testConnection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Dispose resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_connection != null)
                    {
                        if (_connection.State == System.Data.ConnectionState.Open)
                        {
                            _connection.Close();
                        }
                        _connection.Dispose();
                    }
                }
                _disposed = true;
            }
        }
    }
}

