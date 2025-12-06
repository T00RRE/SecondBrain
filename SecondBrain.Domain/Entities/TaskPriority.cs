// Entities/TaskPriority.cs
using System.Collections.Generic;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class TaskPriority : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // "Low", "Medium", "High"
        public int Level { get; set; } // 1-4
        public string Color { get; set; } = "#cccccc";
        public string? Icon { get; set; }
        public int? UserId { get; set; } // null = systemowy
        public bool IsDefault { get; set; }

        public User? User { get; set; }
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}