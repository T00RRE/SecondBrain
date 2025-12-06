// Entities/ErrorLog.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class ErrorLog : BaseEntity
    {
        public string ErrorLevel { get; set; } = "Error"; // "Info", "Warning", "Error", "Critical"
        public string Message { get; set; } = string.Empty;
        public string? StackTrace { get; set; }
        public string? Source { get; set; }
        public int? UserId { get; set; } // Opcjonalny
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        public User? User { get; set; }
    }
}