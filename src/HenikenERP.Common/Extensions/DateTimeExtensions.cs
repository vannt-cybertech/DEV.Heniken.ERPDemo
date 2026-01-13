using System;

namespace HenikenERP.Common.Extensions
{
    /// <summary>
    /// DateTime extension methods
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Format date for display
        /// </summary>
        public static string ToDisplayString(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy HH:mm:ss");
        }
        
        /// <summary>
        /// Format date only for display
        /// </summary>
        public static string ToDateDisplayString(this DateTime dateTime)
        {
            return dateTime.ToString("dd/MM/yyyy");
        }
        
        /// <summary>
        /// Check if date is today
        /// </summary>
        public static bool IsToday(this DateTime dateTime)
        {
            return dateTime.Date == DateTime.Today;
        }
        
        /// <summary>
        /// Check if date is in current month
        /// </summary>
        public static bool IsCurrentMonth(this DateTime dateTime)
        {
            return dateTime.Year == DateTime.Now.Year && dateTime.Month == DateTime.Now.Month;
        }
    }
}

