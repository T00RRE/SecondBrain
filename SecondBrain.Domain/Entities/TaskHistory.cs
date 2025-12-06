// Entities/TaskHistory.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class TaskHistory : BaseEntity
    {
        public int TaskId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime ChangedAt { get; set; }
        public int ChangedBy { get; set; } // FK → Users

        public Task Task { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}