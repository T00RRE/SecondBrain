using System.Collections.Generic;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class TaskCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = "#cccccc";
        public string? Icon { get; set; }
        public int UserId { get; set; }
        public bool IsDefault { get; set; } = false;
        public int OrderIndex { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}