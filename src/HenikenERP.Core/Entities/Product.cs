using System;

namespace HenikenERP.Core.Entities
{
    /// <summary>
    /// Product entity
    /// </summary>
    public class Product
    {
        public string Product_ID { get; set; }
        public string Product_Name { get; set; }
        public string Unit { get; set; }
        public string Category_ID { get; set; }
        public DateTime Created_Date { get; set; }
        public DateTime Modified_Date { get; set; }
    }
}

