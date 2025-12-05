using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecondBrain.Domain.Common;
using SecondBrain.Domain.Enums;

namespace SecondBrain.Domain.Entities
{
    public class Task : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
        public TaskCategory Category { get; set; } = TaskCategory.Personal;

        // Każde zadanie należy do użytkownika
        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}