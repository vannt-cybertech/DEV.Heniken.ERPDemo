using System;

namespace HenikenERP.Core.Entities
{
    /// <summary>
    /// Price list entity
    /// </summary>
    public class PriceList
    {
        public string Price_ID { get; set; }
        public string Product_ID { get; set; }
        public string Tier_ID { get; set; }
        public decimal Unit_Price { get; set; }
        public DateTime Effective_Date { get; set; }
        public DateTime? Expiry_Date { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        
        // Navigation properties
        public Product Product { get; set; }
        public CustomerTier Tier { get; set; }
    }
}

