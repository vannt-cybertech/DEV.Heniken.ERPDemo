-- Fix password hash for admin user
-- Password: admin123
-- Hash: SHA256 + Base64 encoding
-- Correct hash calculated using: EncryptionHelper.HashPassword("admin123")

USE heniken_erp;

-- Update admin user password hash
-- This is the correct SHA256 + Base64 hash for password "admin123"
UPDATE USERS 
SET Password_Hash = 'JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=' 
WHERE Username = 'admin';

-- Verify the update
SELECT Username, Password_Hash, Role, Is_Active 
FROM USERS 
WHERE Username = 'admin';

