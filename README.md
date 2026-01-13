# Heniken ERP Desktop Application

A comprehensive desktop ERP application designed for beer distribution businesses, supporting product management, warehouses, customers (dealers), tier-based pricing, and order processing.

## Overview

This application is built using C# WinForms (.NET Framework 4.8) and MySQL database. It provides a complete solution for managing:

- Product catalog and inventory
- Warehouse management
- Customer/dealer management with tier-based pricing
- Order processing with approval workflow
- Inventory reservation and tracking
- Comprehensive reporting

## Architecture

The application follows a layered architecture:

- **HenikenERP.Core**: Core entities, interfaces, enums, and DTOs
- **HenikenERP.Data**: Data access layer with repository pattern
- **HenikenERP.Business**: Business logic services
- **HenikenERP.Presentation**: WinForms UI layer
- **HenikenERP.Common**: Shared utilities and helpers

## Prerequisites

- .NET Framework 4.8
- Visual Studio 2019 or later
- Docker Desktop (for MySQL database)
- MySQL 8.0 (via Docker)

## Database Setup

### Using Docker

1. Navigate to the `docker` directory
2. Run `docker-compose up -d` to start MySQL container
3. The database will be initialized automatically with the schema from `scripts/init-db.sql`

### Manual Setup

1. Create a MySQL database
2. Run the SQL script from `scripts/init-db.sql` to create all tables

## Configuration

### Database Connection

1. Launch the application
2. Go to **System Settings** → **Database Settings**
3. Enter your database connection details:
   - Server/Host
   - Port (default: 3306)
   - Database name
   - Username
   - Password
4. Click **Test Connection** to verify
5. Save the settings

The connection string is stored encrypted in `App.config`.

## Features

### Main Modules

1. **Dashboard**: Real-time KPIs, revenue charts, recent orders, low-stock alerts
2. **Products**: Product catalog management with stock tracking
3. **Pricing & Customer Tiers**: Tier-based pricing management
4. **Warehouses & Inventory**: Warehouse management and inventory operations
5. **Customers/Dealers**: Customer management with tier assignment
6. **Orders**: Order creation, approval workflow, inventory reservation
7. **Reports**: Revenue, inventory, and order status reports with PDF/Excel export
8. **System Settings**: User management, permissions, workflow configuration

### Key Features

- **Tier-based Pricing**: Automatic price application based on customer tier
- **Inventory Reservation**: Automatic inventory reservation when orders are created
- **Approval Workflow**: Order status management (Created → Pending Approval → Approved → Delivering → Completed)
- **Multi-warehouse Support**: Track inventory across multiple warehouses
- **Vietnamese Localization**: Full UI in Vietnamese language
- **Secure Authentication**: User login with role-based access

## Building the Solution

1. Open `HenikenERP.sln` in Visual Studio
2. Restore NuGet packages
3. Build the solution (Ctrl+Shift+B)
4. Set `HenikenERP.Presentation` as the startup project
5. Run the application (F5)

## Project Structure

```
DEV.Heniken.ERPDemo/
├── src/
│   ├── HenikenERP.Core/          # Core entities and interfaces
│   ├── HenikenERP.Data/           # Data access layer
│   ├── HenikenERP.Business/       # Business logic services
│   ├── HenikenERP.Presentation/   # WinForms UI
│   └── HenikenERP.Common/         # Shared utilities
├── docker/
│   └── docker-compose.yml         # MySQL container setup
├── scripts/
│   └── init-db.sql                # Database schema
├── docs/                          # Documentation
└── README.md                      # This file
```

## Database Schema

The application uses the following main tables:

- `CUSTOMER_TIERS`: Customer tier definitions
- `PRODUCTS`: Product catalog
- `PRICE_LIST`: Tier-based pricing
- `WAREHOUSES`: Warehouse locations
- `INVENTORY`: Inventory tracking
- `CUSTOMERS`: Customer/dealer information
- `ORDERS`: Order headers
- `ORDER_DETAILS`: Order line items
- `USERS`: User accounts for authentication
- `APP_SETTINGS`: Application configuration

## Development

### Code Standards

- **Comments**: All code comments in English
- **UI Text**: All user-facing text in Vietnamese
- **Naming**: Follow C# naming conventions (PascalCase for classes, camelCase for variables)

### Adding New Features

1. Add entities in `HenikenERP.Core/Entities`
2. Create repository interface in `HenikenERP.Core/Interfaces`
3. Implement repository in `HenikenERP.Data/Repositories`
4. Create service in `HenikenERP.Business/Services`
5. Create UI forms in `HenikenERP.Presentation/Forms`

## License

Copyright © 2026

## Support

For issues and questions, please refer to the documentation in the `docs` folder.

