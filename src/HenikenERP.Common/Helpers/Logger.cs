using System;
using System.IO;

namespace HenikenERP.Common.Helpers
{
    /// <summary>
    /// Simple logging utility
    /// </summary>
    public static class Logger
    {
        private static readonly string LogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
        private static readonly object LockObject = new object();
        
        static Logger()
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }
        }
        
        /// <summary>
        /// Log information message
        /// </summary>
        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }
        
        /// <summary>
        /// Log warning message
        /// </summary>
        public static void LogWarning(string message)
        {
            Log("WARNING", message);
        }
        
        /// <summary>
        /// Log error message
        /// </summary>
        public static void LogError(string message, Exception ex = null)
        {
            string errorMessage = message;
            if (ex != null)
            {
                errorMessage += $"\nException: {ex.Message}\nStack Trace: {ex.StackTrace}";
            }
            Log("ERROR", errorMessage);
        }
        
        /// <summary>
        /// Write log entry to file
        /// </summary>
        private static void Log(string level, string message)
        {
            lock (LockObject)
            {
                try
                {
                    string logFile = Path.Combine(LogDirectory, $"HenikenERP_{DateTime.Now:yyyyMMdd}.log");
                    string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}\n";
                    
                    File.AppendAllText(logFile, logEntry);
                }
                catch
                {
                    // Silently fail if logging fails
                }
            }
        }
    }
}

