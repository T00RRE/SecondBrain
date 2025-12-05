using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class HabitCompletion : BaseEntity
    {
        public DateTime CompletedDate { get; set; }
        public string? Note { get; set; }

        // Relacja do nawyku
        public int HabitId { get; set; }
        public Habit Habit { get; set; } = null!;
    }
}
