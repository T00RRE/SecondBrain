// Entities/RecurringTaskTemplate.cs
using System;
using System.Collections.Generic;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class RecurringTaskTemplate : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public int? PriorityId { get; set; }
        public string RecurrencePattern { get; set; } = "weekly"; // "daily", "weekly", "monthly"
        public int RecurrenceInterval { get; set; } = 1;
        public string? DaysOfWeek { get; set; } // JSON "[1,3,5]"
        public int? DayOfMonth { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime NextDueDate { get; set; }
        public int UserId { get; set; }

        public User User { get; set; } = null!;
        public TaskCategory? Category { get; set; }
        public TaskPriority? Priority { get; set; }
        public ICollection<RecurringTaskInstance> Instances { get; set; } = new List<RecurringTaskInstance>();
    }
}