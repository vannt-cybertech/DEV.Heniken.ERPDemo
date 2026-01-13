namespace HenikenERP.Common.Constants
{
    /// <summary>
    /// Application constants
    /// </summary>
    public static class AppConstants
    {
        // Database settings key
        public const string DATABASE_CONNECTION_STRING_KEY = "DatabaseConnectionString";
        
        // Order status strings
        public const string ORDER_STATUS_CREATED = "Created";
        public const string ORDER_STATUS_PENDING_APPROVAL = "Pending Approval";
        public const string ORDER_STATUS_APPROVED = "Approved";
        public const string ORDER_STATUS_DELIVERING = "Delivering";
        public const string ORDER_STATUS_COMPLETED = "Completed";
        public const string ORDER_STATUS_CANCELLED = "Cancelled";
        
        // User roles
        public const string ROLE_ADMINISTRATOR = "Administrator";
        public const string ROLE_MANAGER = "Manager";
        public const string ROLE_SALES_STAFF = "SalesStaff";
        public const string ROLE_WAREHOUSE_STAFF = "WarehouseStaff";
        public const string ROLE_USER = "User";
        
        // ID prefixes
        public const string PREFIX_PRODUCT = "PRD";
        public const string PREFIX_CUSTOMER = "CUS";
        public const string PREFIX_TIER = "TIER";
        public const string PREFIX_PRICE = "PRC";
        public const string PREFIX_WAREHOUSE = "WH";
        public const string PREFIX_INVENTORY = "INV";
        public const string PREFIX_ORDER = "ORD";
        public const string PREFIX_ORDER_DETAIL = "OD";
        public const string PREFIX_USER = "USR";
        
        // Default values
        public const int DEFAULT_PAGE_SIZE = 50;
        public const int LOW_STOCK_THRESHOLD = 10;
    }
}

