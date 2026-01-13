using System;

namespace HenikenERP.Core.Entities
{
    /// <summary>
    /// Inventory entity
    /// </summary>
    public class Inventory
    {
        public string Inventory_ID { get; set; }
        public string Warehouse_ID { get; set; }
        public string Product_ID { get; set; }
        public int Quantity_On_Hand { get; set; }
        public int Quantity_Reserved { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        
        // Computed property
        public int Available_Quantity => Quantity_On_Hand - Quantity_Reserved;
        
        // Navigation properties
        public Warehouse Warehouse { get; set; }
        public Product Product { get; set; }
    }
}

