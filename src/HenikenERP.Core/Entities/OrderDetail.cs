using System;

namespace HenikenERP.Core.Entities
{
    /// <summary>
    /// Order detail entity
    /// </summary>
    public class OrderDetail
    {
        public string OrderDetail_ID { get; set; }
        public string Order_ID { get; set; }
        public string Product_ID { get; set; }
        public int Quantity { get; set; }
        public decimal Applied_Price { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        
        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}

