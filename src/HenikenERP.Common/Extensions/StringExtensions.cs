using System;

namespace HenikenERP.Common.Extensions
{
    /// <summary>
    /// String extension methods
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Check if string is null or empty
        /// </summary>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        
        /// <summary>
        /// Check if string is null, empty, or whitespace
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        
        /// <summary>
        /// Convert string to decimal safely
        /// </summary>
        public static decimal ToDecimal(this string value, decimal defaultValue = 0)
        {
            if (decimal.TryParse(value, out decimal result))
                return result;
            return defaultValue;
        }
        
        /// <summary>
        /// Convert string to int safely
        /// </summary>
        public static int ToInt(this string value, int defaultValue = 0)
        {
            if (int.TryParse(value, out int result))
                return result;
            return defaultValue;
        }
        
        /// <summary>
        /// Truncate string to specified length
        /// </summary>
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;
                
            if (value.Length <= maxLength)
                return value;
                
            return value.Substring(0, maxLength) + "...";
        }
    }
}

