// Entities/HabitCategory.cs
using System.Collections.Generic;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class HabitCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#cccccc";
        public string? Icon { get; set; }
        public int UserId { get; set; }
        public int OrderIndex { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Habit> Habits { get; set; } = new List<Habit>();
    }
}