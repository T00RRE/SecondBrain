// Entities/HabitCompletion.cs (Poprawiona)
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class HabitCompletion : BaseEntity
    {
        public int HabitId { get; set; }
        public DateTime CompletedDate { get; set; }
        public decimal? Value { get; set; } // Dodane pole dla mierzalnych nawyków
        public string? Note { get; set; }

        public Habit Habit { get; set; } = null!;
    }
}