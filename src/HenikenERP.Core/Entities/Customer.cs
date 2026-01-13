using System;

namespace HenikenERP.Core.Entities
{
    /// <summary>
    /// Customer entity
    /// </summary>
    public class Customer
    {
        public string Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public string Tier_ID { get; set; }
        public string Phone_Number { get; set; }
        public bool Is_Active { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        
        // Navigation property
        public CustomerTier Tier { get; set; }
    }
}

