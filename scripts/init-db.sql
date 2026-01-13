-- Heniken ERP Database Schema
-- MySQL 8.0

-- Create database if not exists
CREATE DATABASE IF NOT EXISTS heniken_erp CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE heniken_erp;

-- Table: CUSTOMER_TIERS
CREATE TABLE IF NOT EXISTS CUSTOMER_TIERS (
    Tier_ID VARCHAR(50) PRIMARY KEY,
    Tier_Name VARCHAR(100) NOT NULL,
    Discount_Rate DECIMAL(5,2) DEFAULT 0.00,
    Description TEXT,
    Created_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
    Modified_Date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table: PRODUCTS
CREATE TABLE IF NOT EXISTS PRODUCTS (
    Product_ID VARCHAR(50) PRIMARY KEY,
    Product_Name VARCHAR(200) NOT NULL,
    Unit VARCHAR(50),
    Category_ID VARCHAR(50),
    Created_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
    Modified_Date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table: WAREHOUSES
CREATE TABLE IF NOT EXISTS WAREHOUSES (
    Warehouse_ID VARCHAR(50) PRIMARY KEY,
    Location VARCHAR(200) NOT NULL,
    Capacity INT DEFAULT 0,
    Created_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
    Modified_Date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table: CUSTOMERS
CREATE TABLE IF NOT EXISTS CUSTOMERS (
    Customer_ID VARCHAR(50) PRIMARY KEY,
    Customer_Name VARCHAR(200) NOT NULL,
    Tier_ID VARCHAR(50),
    Phone_Number VARCHAR(20),
    Is_Active BOOLEAN DEFAULT TRUE,
    Created_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
    Modified_Date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (Tier_ID) REFERENCES CUSTOMER_TIERS(Tier_ID) ON DELETE SET NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table: PRICE_LIST
CREATE TABLE IF NOT EXISTS PRICE_LIST (
    Price_ID VARCHAR(50) PRIMARY KEY,
    Product_ID VARCHAR(50) NOT NULL,
    Tier_ID VARCHAR(50) NOT NULL,
    Unit_Price DECIMAL(18,2) NOT NULL,
    Effective_Date DATE NOT NULL,
    Expiry_Date DATE,
    Created_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
    Modified_Date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (Product_ID) REFERENCES PRODUCTS(Product_ID) ON DELETE CASCADE,
    FOREIGN KEY (Tier_ID) REFERENCES CUSTOMER_TIERS(Tier_ID) ON DELETE CASCADE,
    INDEX idx_product_tier (Product_ID, Tier_ID),
    INDEX idx_dates (Effective_Date, Expiry_Date)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table: INVENTORY
CREATE TABLE IF NOT EXISTS INVENTORY (
    Inventory_ID VARCHAR(50) PRIMARY KEY,
    Warehouse_ID VARCHAR(50) NOT NULL,
    Product_ID VARCHAR(50) NOT NULL,
    Quantity_On_Hand INT DEFAULT 0,
    Quantity_Reserved INT DEFAULT 0,
    Created_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
    Modified_Date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (Warehouse_ID) REFERENCES WAREHOUSES(Warehouse_ID) ON DELETE CASCADE,
    FOREIGN KEY (Product_ID) REFERENCES PRODUCTS(Product_ID) ON DELETE CASCADE,
    UNIQUE KEY uk_warehouse_product (Warehouse_ID, Product_ID),
    INDEX idx_warehouse (Warehouse_ID),
    INDEX idx_product (Product_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table: ORDERS
CREATE TABLE IF NOT EXISTS ORDERS (
    Order_ID VARCHAR(50) PRIMARY KEY,
    Customer_ID VARCHAR(50) NOT NULL,
    Warehouse_ID VARCHAR(50) NOT NULL,
    Order_Date DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    Total_Amount DECIMAL(18,2) DEFAULT 0.00,
    Status VARCHAR(50) DEFAULT 'Created',
    Created_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
    Modified_Date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    Created_By VARCHAR(50),
    FOREIGN KEY (Customer_ID) REFERENCES CUSTOMERS(Customer_ID) ON DELETE RESTRICT,
    FOREIGN KEY (Warehouse_ID) REFERENCES WAREHOUSES(Warehouse_ID) ON DELETE RESTRICT,
    INDEX idx_customer (Customer_ID),
    INDEX idx_warehouse (Warehouse_ID),
    INDEX idx_status (Status),
    INDEX idx_order_date (Order_Date)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table: ORDER_DETAILS
CREATE TABLE IF NOT EXISTS ORDER_DETAILS (
    OrderDetail_ID VARCHAR(50) PRIMARY KEY,
    Order_ID VARCHAR(50) NOT NULL,
    Product_ID VARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    Applied_Price DECIMAL(18,2) NOT NULL,
    Created_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
    Modified_Date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (Order_ID) REFERENCES ORDERS(Order_ID) ON DELETE CASCADE,
    FOREIGN KEY (Product_ID) REFERENCES PRODUCTS(Product_ID) ON DELETE RESTRICT,
    INDEX idx_order (Order_ID),
    INDEX idx_product (Product_ID)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table: USERS (for authentication)
CREATE TABLE IF NOT EXISTS USERS (
    User_ID VARCHAR(50) PRIMARY KEY,
    Username VARCHAR(100) NOT NULL UNIQUE,
    Password_Hash VARCHAR(255) NOT NULL,
    Role VARCHAR(50) DEFAULT 'User',
    Is_Active BOOLEAN DEFAULT TRUE,
    Created_Date DATETIME DEFAULT CURRENT_TIMESTAMP,
    Modified_Date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    Last_Login DATETIME,
    INDEX idx_username (Username)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Table: APP_SETTINGS (for application configuration)
CREATE TABLE IF NOT EXISTS APP_SETTINGS (
    Setting_Key VARCHAR(100) PRIMARY KEY,
    Setting_Value TEXT,
    Description VARCHAR(255),
    Modified_Date DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Insert default admin user (password: admin123)
-- Password hash: SHA256 + Base64 encoding (calculated using EncryptionHelper.HashPassword)
-- Hash for 'admin123': JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=
INSERT INTO USERS (User_ID, Username, Password_Hash, Role, Is_Active) 
VALUES ('USR001', 'admin', 'JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=', 'Administrator', TRUE)
ON DUPLICATE KEY UPDATE Username=Username;

-- Insert sample customer tiers
INSERT INTO CUSTOMER_TIERS (Tier_ID, Tier_Name, Discount_Rate, Description) VALUES
('TIER001', 'Gold', 15.00, 'Khách hàng VIP với mức chiết khấu cao nhất'),
('TIER002', 'Silver', 10.00, 'Khách hàng thân thiết với mức chiết khấu trung bình'),
('TIER003', 'Bronze', 5.00, 'Khách hàng thường với mức chiết khấu cơ bản')
ON DUPLICATE KEY UPDATE Tier_Name=VALUES(Tier_Name);

-- Insert sample warehouses
INSERT INTO WAREHOUSES (Warehouse_ID, Location, Capacity) VALUES
('WH001', 'Hoc Mon, Ho Chi Minh City', 10000),
('WH002', 'Da Nang', 8000),
('WH003', 'Hanoi', 12000)
ON DUPLICATE KEY UPDATE Location=VALUES(Location);

-- Insert default database connection setting (will be updated by application)
INSERT INTO APP_SETTINGS (Setting_Key, Setting_Value, Description) VALUES
('DatabaseConnectionString', '', 'Encrypted database connection string')
ON DUPLICATE KEY UPDATE Setting_Value=VALUES(Setting_Value);

