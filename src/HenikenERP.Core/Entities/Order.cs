using System;
using System.Collections.Generic;
using HenikenERP.Core.Enums;

namespace HenikenERP.Core.Entities
{
    /// <summary>
    /// Order entity
    /// </summary>
    public class Order
    {
        public string Order_ID { get; set; }
        public string Customer_ID { get; set; }
        public string Warehouse_ID { get; set; }
        public DateTime Order_Date { get; set; }
        public decimal Total_Amount { get; set; }
        public string Status { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public string Created_By { get; set; }
        
        // Navigation properties
        public Customer Customer { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}

