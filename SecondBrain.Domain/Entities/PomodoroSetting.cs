// Entities/PomodoroSetting.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class PomodoroSetting : BaseEntity
    {
        public int UserId { get; set; }
        public int WorkDuration { get; set; } = 25;
        public int ShortBreakDuration { get; set; } = 5;
        public int LongBreakDuration { get; set; } = 15;
        public int SessionsBeforeLongBreak { get; set; } = 4;
        public bool AutoStartBreaks { get; set; } = false;
        public bool AutoStartWork { get; set; } = false;
        public bool TickingSound { get; set; } = false;
        public bool NotificationSound { get; set; } = true;

        // Relacja jeden-do-jednego (UserId jest unikalne)
        public User User { get; set; } = null!;
    }
}