using SecondBrain.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondBrain.Domain.Entities
{
    public class ActivityLog : BaseEntity
    {
        public int UserId { get; set; }
        public string Action { get; set; } = string.Empty; // "task_created", "habit_completed"
        public string EntityType { get; set; } = string.Empty; // "Task", "Habit"
        public int? EntityId { get; set; }
        public string? OldValues { get; set; } // JSON
        public string? NewValues { get; set; } // JSON
        public string? IpAddress { get; set; }
        public string? UserAgent { get; set; }

        // Relacja nawigacyjna wiele-do-jednego
        public User User { get; set; } = null!;
    }
}
