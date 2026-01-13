using System;
using HenikenERP.Core.DTOs;
using HenikenERP.Core.Entities;
using HenikenERP.Core.Interfaces;
using HenikenERP.Common.Helpers;
using HenikenERP.Data.Context;
using HenikenERP.Data.Repositories;
using HenikenERP.Data.UnitOfWork;

namespace HenikenERP.Business.Services
{
    /// <summary>
    /// Authentication service for user login
    /// </summary>
    public class AuthenticationService
    {
        private DatabaseContext _context;
        private UnitOfWork _unitOfWork;

        /// <summary>
        /// Last error message for UI display (when Authenticate returns null).
        /// </summary>
        public string LastErrorMessage { get; private set; }
        
        public AuthenticationService()
        {
            // Don't initialize context here - defer until actual use
        }
        
        /// <summary>
        /// Get or create database context (lazy initialization)
        /// </summary>
        private DatabaseContext GetContext()
        {
            if (_context == null)
            {
                _context = new DatabaseContext();
            }
            return _context;
        }
        
        /// <summary>
        /// Get or create unit of work (lazy initialization)
        /// </summary>
        private UnitOfWork GetUnitOfWork()
        {
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork(GetContext());
            }
            return _unitOfWork;
        }
        
        /// <summary>
        /// Authenticate user login
        /// </summary>
        public User Authenticate(LoginDTO loginDTO)
        {
            LastErrorMessage = null;

            if (loginDTO == null || string.IsNullOrWhiteSpace(loginDTO.Username) || string.IsNullOrWhiteSpace(loginDTO.Password))
            {
                LastErrorMessage = "Vui lòng nhập tên đăng nhập và mật khẩu.";
                return null;
            }
            
            try
            {
                // Initialize context and unit of work only when needed
                var unitOfWork = GetUnitOfWork();
                var userRepository = unitOfWork.Users as UserRepository;
                var user = userRepository?.GetByUsername(loginDTO.Username);
                
                if (user == null || !user.Is_Active)
                {
                    if (user == null)
                    {
                        try
                        {
                            var dbName = _context.TryGetDatabaseName();
                            Logger.LogWarning($"Login failed: user '{loginDTO.Username}' not found. Connected DB='{dbName}'.");
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError("Login diagnostic query failed.", ex);
                        }

                        LastErrorMessage = "Không tìm thấy người dùng. Vui lòng kiểm tra lại tên đăng nhập.";
                    }
                    else
                    {
                        Logger.LogWarning($"Login failed: user '{loginDTO.Username}' is inactive.");
                        LastErrorMessage = "Tài khoản đã bị khóa (Is_Active = false).";
                    }
                    return null;
                }
                
                // Verify password
                var passwordForHash = loginDTO.Password ?? string.Empty;
                var trimmedPasswordForHash = passwordForHash.Trim();
                var computedHash = EncryptionHelper.HashPassword(trimmedPasswordForHash);
                if (computedHash == user.Password_Hash)
                {
                    // Update last login
                    user.Last_Login = DateTime.Now;
                    unitOfWork.Users.Update(user);
                    unitOfWork.SaveChanges();
                    
                    return user;
                }

                Logger.LogWarning(
                    $"Login failed: password mismatch for user '{loginDTO.Username}'. " +
                    $"ComputedHash='{computedHash}', StoredHash='{user.Password_Hash}', " +
                    $"PasswordLen={passwordForHash.Length}, TrimmedLen={trimmedPasswordForHash.Length}.");
                LastErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError("Authentication error", ex);
                // Don't just log - rethrow with meaningful message
                throw new Exception("Lỗi kết nối database. Vui lòng kiểm tra cấu hình Database trong Settings.", ex);
            }
        }
        
        /// <summary>
        /// Dispose resources
        /// </summary>
        public void Dispose()
        {
            _unitOfWork?.Dispose();
            _context?.Dispose();
        }
    }
}

