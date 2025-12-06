// Entities/SystemSetting.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class SystemSetting : BaseEntity
    {
        public string SettingKey { get; set; } = string.Empty; // Unikalne
        public string SettingValue { get; set; } = string.Empty;
        public string? Description { get; set; }
        // UpdatedAt jest w BaseEntity
        public int? UpdatedBy { get; set; } // FK → Users

        public User? UpdatedByUser { get; set; }
    }
}