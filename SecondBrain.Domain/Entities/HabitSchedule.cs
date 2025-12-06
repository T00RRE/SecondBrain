// Entities/HabitSchedule.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class HabitSchedule : BaseEntity
    {
        public int HabitId { get; set; }
        public int DayOfWeek { get; set; } // 0-6 (Sun-Sat)
        public TimeSpan? TimeOfDay { get; set; } // Preferowany czas
        public bool IsActive { get; set; } = true;

        public Habit Habit { get; set; } = null!;
    }
}