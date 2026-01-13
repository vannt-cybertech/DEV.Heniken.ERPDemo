using System;

namespace HenikenERP.Core.Entities
{
    /// <summary>
    /// Customer tier entity
    /// </summary>
    public class CustomerTier
    {
        public string Tier_ID { get; set; }
        public string Tier_Name { get; set; }
        public decimal Discount_Rate { get; set; }
        public string Description { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
    }
}

