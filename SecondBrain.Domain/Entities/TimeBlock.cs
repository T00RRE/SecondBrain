using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class TimeBlock : BaseEntity
    {
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Activity { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = string.Empty; // "Praca", "Nauka", "Sport"
        public string Color { get; set; } = "#6B7280";

        // Użytkownik
        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}