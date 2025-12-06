using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondBrain.Domain.Common;
using SecondBrain.Domain.Enums;

namespace SecondBrain.Domain.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }

        // Zmieniono enum na FK (int) i relacje
        public int? CategoryId { get; set; }
        public TaskCategory? Category { get; set; } // FK → TaskCategories

        public int? PriorityId { get; set; }
        public TaskPriority? Priority { get; set; } // FK → TaskPriorities

        public int? EstimatedMinutes { get; set; }
        public int? ActualMinutes { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;

        // Subtaski (Self-Referencing)
        public int? ParentTaskId { get; set; }
        public Task? ParentTask { get; set; } // FK → Tasks
        public ICollection<Task> Subtasks { get; set; } = new List<Task>();

        public int? RecurringTemplateId { get; set; }
        public RecurringTaskTemplate? RecurringTemplate { get; set; } // FK → RecurringTaskTemplates

        public bool IsDeleted { get; set; } = false;

        // Relacje jeden-do-wielu
        public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
        public ICollection<TaskHistory> History { get; set; } = new List<TaskHistory>();
    }
}