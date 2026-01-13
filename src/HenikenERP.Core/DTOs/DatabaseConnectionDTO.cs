namespace HenikenERP.Core.DTOs
{
    /// <summary>
    /// Data transfer object for database connection settings
    /// </summary>
    public class DatabaseConnectionDTO
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public string GetConnectionString()
        {
            return $"Server={Server};Port={Port};Database={Database};Uid={Username};Pwd={Password};CharSet=utf8mb4;";
        }
    }
}

