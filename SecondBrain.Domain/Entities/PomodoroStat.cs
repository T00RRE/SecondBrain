// Entities/PomodoroStat.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class PomodoroStat : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public int TotalSessions { get; set; } = 0;
        public int CompletedSessions { get; set; } = 0;
        public int TotalFocusMinutes { get; set; } = 0;
        public int TotalBreakMinutes { get; set; } = 0;
        public int InterruptedSessions { get; set; } = 0;

        // Unikalny klucz: UserId + Date
        public User User { get; set; } = null!;
    }
}