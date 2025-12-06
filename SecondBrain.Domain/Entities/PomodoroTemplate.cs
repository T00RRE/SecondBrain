// Entities/PomodoroTemplate.cs
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class PomodoroTemplate : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // "Quick Focus", "Deep Work"
        public int WorkDuration { get; set; }
        public int ShortBreakDuration { get; set; }
        public int LongBreakDuration { get; set; }
        public int SessionsBeforeLongBreak { get; set; }
        public int UserId { get; set; }
        public bool IsDefault { get; set; } = false;

        public User User { get; set; } = null!;
    }
}