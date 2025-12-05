using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::SecondBrain.Domain.Common;
using SecondBrain.Domain.Enums;

namespace SecondBrain.Domain.Entities
{
    public class PomodoroSession : BaseEntity
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int DurationMinutes { get; set; } = 25;
        public PomodoroType Type { get; set; } = PomodoroType.Work;
        public bool IsCompleted { get; set; } = false;
        public string? Note { get; set; }

        // Opcjonalne powiązanie z zadaniem
        public int? TaskId { get; set; }
        public Task? Task { get; set; }

        // Użytkownik
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
