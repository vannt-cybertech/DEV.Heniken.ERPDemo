using System;
using System.Linq;
using HenikenERP.Common.Constants;

namespace HenikenERP.Common.Helpers
{
    /// <summary>
    /// Helper class for generating unique IDs
    /// </summary>
    public static class IDGenerator
    {
        private static readonly Random Random = new Random();
        
        /// <summary>
        /// Generate unique ID with prefix
        /// </summary>
        public static string GenerateID(string prefix, int existingCount = 0)
        {
            int sequence = existingCount + 1;
            return $"{prefix}{sequence:D6}";
        }
        
        /// <summary>
        /// Generate unique ID with timestamp
        /// </summary>
        public static string GenerateIDWithTimestamp(string prefix)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            int random = Random.Next(1000, 9999);
            return $"{prefix}{timestamp}{random}";
        }
        
        /// <summary>
        /// Generate product ID
        /// </summary>
        public static string GenerateProductID(int existingCount = 0)
        {
            return GenerateID(AppConstants.PREFIX_PRODUCT, existingCount);
        }
        
        /// <summary>
        /// Generate customer ID
        /// </summary>
        public static string GenerateCustomerID(int existingCount = 0)
        {
            return GenerateID(AppConstants.PREFIX_CUSTOMER, existingCount);
        }
        
        /// <summary>
        /// Generate order ID
        /// </summary>
        public static string GenerateOrderID()
        {
            return GenerateIDWithTimestamp(AppConstants.PREFIX_ORDER);
        }
        
        /// <summary>
        /// Generate order detail ID
        /// </summary>
        public static string GenerateOrderDetailID(string orderID, int sequence)
        {
            return $"{orderID}-{AppConstants.PREFIX_ORDER_DETAIL}{sequence:D3}";
        }
    }
}

