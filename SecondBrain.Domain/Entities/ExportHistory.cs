// Entities/ExportHistory.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class ExportHistory : BaseEntity
    {
        public int UserId { get; set; }
        public string ExportType { get; set; } = "pdf"; // "pdf", "excel", "csv"
        public string EntityType { get; set; } = string.Empty; // "tasks", "habits", "report"
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string? FileUrl { get; set; }
        public DateTime ExportedAt { get; set; }

        public User User { get; set; } = null!;
    }
}