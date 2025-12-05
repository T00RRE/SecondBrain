using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        // Relacje - jeden użytkownik ma wiele zadań, nawyków itd.
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
        public ICollection<Habit> Habits { get; set; } = new List<Habit>();
        public ICollection<PomodoroSession> PomodoroSessions { get; set; } = new List<PomodoroSession>();
        public ICollection<TimeBlock> TimeBlocks { get; set; } = new List<TimeBlock>();
    }
}