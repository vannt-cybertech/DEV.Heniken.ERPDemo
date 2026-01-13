using System;

namespace HenikenERP.Core.Entities
{
    /// <summary>
    /// Application setting entity
    /// </summary>
    public class AppSetting
    {
        public string Setting_Key { get; set; }
        public string Setting_Value { get; set; }
        public string Description { get; set; }
        public DateTime Modified_Date { get; set; }
    }
}

