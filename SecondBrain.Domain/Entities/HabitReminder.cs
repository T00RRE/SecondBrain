// Entities/HabitReminder.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class HabitReminder : BaseEntity
    {
        public int HabitId { get; set; }
        public TimeSpan ReminderTime { get; set; }
        public string DaysOfWeek { get; set; } = "[]"; // JSON "[1,2,3,4,5]"
        public string? Message { get; set; }
        public bool IsActive { get; set; } = true;

        public Habit Habit { get; set; } = null!;
    }
}