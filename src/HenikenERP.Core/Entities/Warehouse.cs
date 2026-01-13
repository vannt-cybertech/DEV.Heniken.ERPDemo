using System;

namespace HenikenERP.Core.Entities
{
    /// <summary>
    /// Warehouse entity
    /// </summary>
    public class Warehouse
    {
        public string Warehouse_ID { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
    }
}

