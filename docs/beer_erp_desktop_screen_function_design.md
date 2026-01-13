# Beer Distribution ERP Desktop App

## 1. Overview
A desktop ERP application designed for beer distribution businesses, supporting product management, warehouses, customers (dealers), tier-based pricing, and order processing.

**Target Users**
- System Administrator
- Sales Staff
- Warehouse Staff
- Managers / Accounting

---

## 2. Main Menu Structure

- Dashboard
- Products
- Pricing & Customer Tiers
- Warehouses & Inventory
- Customers / Dealers
- Orders
- Reports
- System Settings

---

## 3. Screen & Function Design

### 3.1 Dashboard

**Purpose**
Provide a real-time overview of business and warehouse operations.

**Key Functions**
- Total orders today
- Revenue (daily / monthly)
- Total inventory on hand
- Pending approval orders

**UI Components**
- KPI cards
- Revenue charts
- Recent orders table
- Low-stock alerts

---

### 3.2 Products (PRODUCTS)

**Product List**
- Product SKU
- Product name
- Unit of measure
- Product category
- Total stock quantity

**Functions**
- Create / edit / delete products
- Assign product categories
- View stock by warehouse
- Import / export product data

---

### 3.3 Pricing & Customer Tiers

#### Customer Tiers (CUSTOMER_TIERS)

**Functions**
- Define customer tiers (Gold, Silver, Bronze, VIP, etc.)
- Configure default discount rates
- Define tier qualification conditions

---

#### Price List (PRICE_LIST)

**Functions**
- Manage prices by product and customer tier
- Set effective and expiry dates
- Maintain price history
- Automatically apply correct price when creating orders

---

### 3.4 Warehouses & Inventory (WAREHOUSES & INVENTORY)

**Warehouses**
- Warehouse ID
- Location
- Capacity

**Inventory Information**
- Product
- Warehouse
- Quantity on hand
- Reserved quantity
- Available quantity

**Functions**
- Stock in
- Stock out
- Inventory adjustment
- Warehouse transfers
- Inventory movement history

---

### 3.5 Customers / Dealers (CUSTOMERS)

**Customer Information**
- Customer ID
- Dealer name
- Assigned customer tier
- Phone number

**Functions**
- Assign or change customer tier
- View order history
- Revenue summary per customer
- Activate / deactivate customers

---

### 3.6 Orders (ORDERS & ORDER_DETAILS)

**Order Status Workflow**
- Created
- Pending approval
- Approved
- Delivering
- Completed / Cancelled

**Functions**
- Create sales orders
- Automatically apply tier-based pricing
- Reserve inventory upon order creation
- Approve or cancel orders
- Print invoices and delivery notes

---

### 3.7 Reports

**Available Reports**
- Revenue by customer
- Revenue by product
- Revenue by customer tier
- Inventory reports
- Order status reports

**Export Options**
- PDF
- Excel

---

### 3.8 System Settings

**Functions**
- User and role management
- Permission configuration
- Order approval workflow settings
- General system configurations

---

## 4. ERD to Module Mapping

| Database Table | ERP Module |
|---------------|------------|
| PRODUCTS | Product Management |
| CUSTOMER_TIERS | Customer Tier Management |
| PRICE_LIST | Pricing Management |
| INVENTORY | Inventory Management |
| WAREHOUSES | Warehouse Management |
| CUSTOMERS | Customer Management |
| ORDERS | Order Management |
| ORDER_DETAILS | Order Detail Management |

---

*This document serves as a functional and screen-level design reference for a Beer Distribution ERP Desktop Application.*