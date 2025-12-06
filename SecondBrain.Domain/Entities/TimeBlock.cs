// Entities/TimeBlock.cs (Poprawiona)
using System;
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

        // Zmieniono sztywną kategorię na FK i relacje
        public int CategoryId { get; set; }
        public TimeCategory Category { get; set; } = null!; // FK → TimeCategories

        public int? TaskId { get; set; }
        public Task? Task { get; set; } // FK → Tasks (Opcjonalne powiązanie)

        public int UserId { get; set; }
        public User User { get; set; } = null!;
    }
}