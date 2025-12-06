using SecondBrain.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondBrain.Domain.Entities
{
    public class Notification : BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // "task_reminder", "habit_reminder"
        public string Priority { get; set; } = "normal"; // "low", "normal", "high"
        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public string? ActionUrl { get; set; }

        // Relacja nawigacyjna wiele-do-jednego
        public User User { get; set; } = null!;
    }
}
