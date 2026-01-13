using System;
using System.Text.RegularExpressions;

namespace HenikenERP.Common.Helpers
{
    /// <summary>
    /// Helper class for input validation
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Validate email format
        /// </summary>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;
                
            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }
        
        /// <summary>
        /// Validate phone number format
        /// </summary>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return false;
                
            var regex = new Regex(@"^[\d\s\-\+\(\)]+$");
            return regex.IsMatch(phoneNumber);
        }
        
        /// <summary>
        /// Validate required field
        /// </summary>
        public static bool IsRequired(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        
        /// <summary>
        /// Validate numeric value
        /// </summary>
        public static bool IsNumeric(string value)
        {
            return decimal.TryParse(value, out _);
        }
        
        /// <summary>
        /// Validate positive number
        /// </summary>
        public static bool IsPositiveNumber(decimal value)
        {
            return value > 0;
        }
        
        /// <summary>
        /// Validate non-negative number
        /// </summary>
        public static bool IsNonNegativeNumber(decimal value)
        {
            return value >= 0;
        }
        
        /// <summary>
        /// Validate ID format (alphanumeric with optional prefix)
        /// </summary>
        public static bool IsValidID(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;
                
            var regex = new Regex(@"^[A-Z0-9]+$");
            return regex.IsMatch(id);
        }
    }
}

