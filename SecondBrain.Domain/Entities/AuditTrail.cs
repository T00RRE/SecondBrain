// Entities/AuditTrail.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class AuditTrail : BaseEntity
    {
        public int? UserId { get; set; } // Opcjonalny
        public string Action { get; set; } = string.Empty; // "Create", "Update", "Delete", "Login"
        public string EntityType { get; set; } = string.Empty;
        public int? EntityId { get; set; }
        public string? OldValues { get; set; } // JSON
        public string? NewValues { get; set; } // JSON
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public User? User { get; set; }
    }
}