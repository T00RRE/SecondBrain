// Entities/PomodoroSession.cs (Poprawiona)
using System;
using SecondBrain.Domain.Common;
// using SecondBrain.Domain.Enums; // Zakładamy, że PomodoroType jest Enumem

namespace SecondBrain.Domain.Entities
{
    public class PomodoroSession : BaseEntity
    {
        public int UserId { get; set; }
        public int? TaskId { get; set; }

        public string SessionType { get; set; } = "work"; // "work", "short_break", "long_break"

        // Poprawiono nazwy pól
        public int PlannedDuration { get; set; }
        public int? ActualDuration { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool IsCompleted { get; set; } = false;
        public bool IsInterrupted { get; set; } = false; // Dodane
        public string? Note { get; set; }

        public User User { get; set; } = null!;
        public Task? Task { get; set; }
    }
}