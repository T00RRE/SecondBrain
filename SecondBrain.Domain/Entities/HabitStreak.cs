// Entities/HabitStreak.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class HabitStreak : BaseEntity
    {
        public int HabitId { get; set; }
        public int CurrentStreak { get; set; } = 0;
        public int LongestStreak { get; set; } = 0;
        public DateTime? LastCompletedDate { get; set; }
        public DateTime? StreakStartDate { get; set; }

        // Relacja jeden-do-jednego (HabitId jest unikalne)
        public Habit Habit { get; set; } = null!;
    }
}