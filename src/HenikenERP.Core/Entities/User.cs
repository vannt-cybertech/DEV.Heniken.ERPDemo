using System;
using HenikenERP.Core.Enums;

namespace HenikenERP.Core.Entities
{
    /// <summary>
    /// User entity for authentication
    /// </summary>
    public class User
    {
        public string User_ID { get; set; }
        public string Username { get; set; }
        public string Password_Hash { get; set; }
        public string Role { get; set; }
        public bool Is_Active { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public DateTime? Last_Login { get; set; }
    }
}

