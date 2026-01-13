using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace HenikenERP.Common.Helpers
{
    /// <summary>
    /// Helper class for encryption and hashing operations
    /// </summary>
    public static class EncryptionHelper
    {
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("HenikenERP2026Key!1234567890123456"); // 32 bytes for AES-256
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("HenikenERP2026IV!"); // 16 bytes for AES
        
        /// <summary>
        /// Hash password using SHA256 (for demo - use BCrypt in production)
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;
                
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
        
        /// <summary>
        /// Verify password against hash
        /// </summary>
        public static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hash))
                return false;
                
            string hashedPassword = HashPassword(password);
            return hashedPassword == hash;
        }
        
        /// <summary>
        /// Encrypt connection string
        /// </summary>
        public static string EncryptConnectionString(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;
                
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;
                    
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                            {
                                swEncrypt.Write(plainText);
                            }
                            return Convert.ToBase64String(msEncrypt.ToArray());
                        }
                    }
                }
            }
            catch
            {
                return plainText; // Return plain text if encryption fails
            }
        }
        
        /// <summary>
        /// Decrypt connection string
        /// </summary>
        public static string DecryptConnectionString(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;
                
            try
            {
                byte[] buffer = Convert.FromBase64String(cipherText);
                
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Key;
                    aes.IV = IV;
                    
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    
                    using (MemoryStream msDecrypt = new MemoryStream(buffer))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch
            {
                return cipherText; // Return as-is if decryption fails
            }
        }
    }
}

