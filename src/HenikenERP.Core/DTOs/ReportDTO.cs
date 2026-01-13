using System;

namespace HenikenERP.Core.DTOs
{
    /// <summary>
    /// Data transfer object for revenue reports
    /// </summary>
    public class RevenueReportDTO
    {
        public string Customer_ID { get; set; }
        public string Customer_Name { get; set; }
        public string Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Tier_ID { get; set; }
        public string Tier_Name { get; set; }
        public decimal Total_Amount { get; set; }
        public int Total_Quantity { get; set; }
        public DateTime Report_Date { get; set; }
    }
    
    /// <summary>
    /// Data transfer object for inventory reports
    /// </summary>
    public class InventoryReportDTO
    {
        public string Warehouse_ID { get; set; }
        public string Warehouse_Location { get; set; }
        public string Product_ID { get; set; }
        public string Product_Name { get; set; }
        public int Quantity_On_Hand { get; set; }
        public int Quantity_Reserved { get; set; }
        public int Available_Quantity { get; set; }
    }
}

