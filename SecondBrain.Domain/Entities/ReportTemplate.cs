// Entities/ReportTemplate.cs
using System.Collections.Generic;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class ReportTemplate : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string ReportType { get; set; } = "daily"; // "daily", "weekly", "monthly"
        public bool IncludeTasks { get; set; } = true;
        public bool IncludeHabits { get; set; } = true;
        public bool IncludePomodoro { get; set; } = true;
        public bool IncludeTimeBlocks { get; set; } = true;
        public string? CustomFilters { get; set; } // JSON
        public int UserId { get; set; }

        public User User { get; set; } = null!;
        public ICollection<GeneratedReport> GeneratedReports { get; set; } = new List<GeneratedReport>();
    }
}