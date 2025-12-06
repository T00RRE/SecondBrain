using SecondBrain.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondBrain.Domain.Entities
{
    public class UserStat : BaseEntity
    {
        public int UserId { get; set; }
        public int TotalTasks { get; set; } = 0;
        public int CompletedTasks { get; set; } = 0;
        public int TotalPomodoros { get; set; } = 0;
        public int TotalFocusMinutes { get; set; } = 0;
        public int CurrentHabitStreak { get; set; } = 0;
        public int LongestHabitStreak { get; set; } = 0;
        public DateTime? LastActivityDate { get; set; }

        // Pole UpdatedAt w BaseEntity powinno wystarczyć, ale zostawiamy dla spójności ze schematem
        // public DateTime UpdatedAt { get; set; } 

        // Relacja nawigacyjna jeden-do-jednego
        public User User { get; set; } = null!;
    }
}
