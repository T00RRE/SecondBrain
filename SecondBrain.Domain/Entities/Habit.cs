using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class Habit : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Icon { get; set; } 
        public string Color { get; set; } = "#4A90E2"; 

        // Kiedy wykonywać
        public bool IsDaily { get; set; } = true;
        public string? SpecificDays { get; set; } // JSON "[1,3,5]" = Pon, Śr, Pt

        // Streak tracking
        public int CurrentStreak { get; set; } = 0;
        public int BestStreak { get; set; } = 0;

        // Relacje
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public ICollection<HabitCompletion> Completions { get; set; } = new List<HabitCompletion>();
    }
}
