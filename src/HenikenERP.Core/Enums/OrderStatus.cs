namespace HenikenERP.Core.Enums
{
    /// <summary>
    /// Order status enumeration
    /// </summary>
    public enum OrderStatus
    {
        Created = 0,
        PendingApproval = 1,
        Approved = 2,
        Delivering = 3,
        Completed = 4,
        Cancelled = 5
    }
}

