using HenikenERP.Core.Enums;

namespace HenikenERP.Core.DTOs
{
    /// <summary>
    /// Data transfer object for stock transactions
    /// </summary>
    public class StockTransactionDTO
    {
        public StockTransactionType TransactionType { get; set; }
        public string Warehouse_ID { get; set; }
        public string Product_ID { get; set; }
        public int Quantity { get; set; }
        public string To_Warehouse_ID { get; set; } // For transfer transactions
        public string Notes { get; set; }
    }
}

