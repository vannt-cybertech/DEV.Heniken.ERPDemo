using System;
using System.Collections.Generic;

namespace HenikenERP.Core.DTOs
{
    /// <summary>
    /// Data transfer object for creating orders
    /// </summary>
    public class OrderCreateDTO
    {
        public string Customer_ID { get; set; }
        public string Warehouse_ID { get; set; }
        public DateTime Order_Date { get; set; }
        public List<OrderDetailCreateDTO> OrderDetails { get; set; }
        
        public OrderCreateDTO()
        {
            OrderDetails = new List<OrderDetailCreateDTO>();
        }
    }
    
    /// <summary>
    /// Data transfer object for order detail creation
    /// </summary>
    public class OrderDetailCreateDTO
    {
        public string Product_ID { get; set; }
        public int Quantity { get; set; }
    }
}

