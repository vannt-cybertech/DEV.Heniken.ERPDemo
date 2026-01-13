# Database Scripts

## Scripts Overview

### 1. init-db.sql
Creates the database schema with all tables, indexes, and constraints.
**Run this first** to set up the database structure.

### 2. sample-data.sql
Populates the database with sample data for testing and demonstration.
**Run this after** init-db.sql to add sample data.

## How to Use

### Option 1: Using Docker (Automatic)
The `init-db.sql` script runs automatically when the MySQL container starts for the first time.

To add sample data:
```bash
docker exec -i heniken_erp_mysql mysql -uheniken_user -pheniken_password heniken_erp < scripts/sample-data.sql
```

### Option 2: Using MySQL Command Line
```bash
# Connect to MySQL
mysql -u heniken_user -p heniken_erp

# Or if using root
mysql -u root -p heniken_erp

# Then run:
source scripts/init-db.sql;
source scripts/sample-data.sql;
```

### Option 3: Using MySQL Workbench or phpMyAdmin
1. Open MySQL Workbench or phpMyAdmin
2. Connect to your MySQL server
3. Select the `heniken_erp` database
4. Run the SQL scripts in order:
   - First: `init-db.sql`
   - Then: `sample-data.sql`

## Sample Data Includes

- **10 Products**: Various beer products (Heineken, Tiger, local beers)
- **10 Customers**: Different customer types (dealers, restaurants, supermarkets)
- **30 Price List Entries**: Prices for all products across all tiers (Gold, Silver, Bronze)
- **30 Inventory Records**: Stock levels across 3 warehouses
- **5 Sample Orders**: Orders in different statuses with order details

## Default Login Credentials

- **Username**: `admin`
- **Password**: `admin123`

⚠️ **Important**: Change the default password in production!

## Fix Password Hash (If Login Fails)

If you're unable to login with the default credentials, the password hash in the database might be incorrect. Run this script to fix it:

```bash
# Using Docker
docker exec -i heniken_erp_mysql mysql -uheniken_user -pheniken_password heniken_erp < scripts/fix-password-hash.sql

# Or using MySQL command line
mysql -u heniken_user -pheniken_password heniken_erp < scripts/fix-password-hash.sql
```

This will update the password hash to the correct SHA256 + Base64 hash for password "admin123".

## Verifying Data

After running the scripts, you can verify the data:

```sql
SELECT COUNT(*) AS Products FROM PRODUCTS;
SELECT COUNT(*) AS Customers FROM CUSTOMERS;
SELECT COUNT(*) AS PriceEntries FROM PRICE_LIST;
SELECT COUNT(*) AS InventoryRecords FROM INVENTORY;
SELECT COUNT(*) AS Orders FROM ORDERS;
```

