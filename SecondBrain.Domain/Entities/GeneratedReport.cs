// Entities/GeneratedReport.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class GeneratedReport : BaseEntity
    {
        public int? TemplateId { get; set; }
        public int UserId { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string ReportData { get; set; } = string.Empty; // JSON
        public string? FileUrl { get; set; }
        public DateTime GeneratedAt { get; set; }

        public ReportTemplate? Template { get; set; }
        public User User { get; set; } = null!;
    }
}