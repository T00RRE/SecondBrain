// Entities/Habit.cs (Poprawiona)
using System;
using System.Collections.Generic;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class Habit : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        // Zmieniono na FK do HabitCategory
        public int? CategoryId { get; set; }
        public HabitCategory? Category { get; set; } // FK → HabitCategories

        public string? Icon { get; set; }
        public string Color { get; set; } = "#4A90E2";

        // Pola dla mierzalnych nawyków
        public string TargetType { get; set; } = "daily"; // "daily", "weekly", "monthly"
        public int TargetCount { get; set; } = 1;
        public string? Unit { get; set; } // "minutes", "pages", "km"

        public bool IsActive { get; set; } = true;
        public int UserId { get; set; }

        // Usunięto pola streak (CurrentStreak, BestStreak) na rzecz dedykowanej encji HabitStreak

        // Relacje
        public User User { get; set; } = null!;
        public HabitStreak? Streak { get; set; } // Relacja jeden-do-jednego
        public ICollection<HabitCompletion> Completions { get; set; } = new List<HabitCompletion>();
        public ICollection<HabitSchedule> Schedules { get; set; } = new List<HabitSchedule>();
        public ICollection<HabitReminder> Reminders { get; set; } = new List<HabitReminder>();
    }
}