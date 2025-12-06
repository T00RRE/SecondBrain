using SecondBrain.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondBrain.Domain.Entities
{
    public class UserSettings : BaseEntity
    {
        public int UserId { get; set; }
        public string Theme { get; set; } = "light";
        public int PomodoroWorkDuration { get; set; } = 25;
        public int PomodoroShortBreak { get; set; } = 5;
        public int PomodoroLongBreak { get; set; } = 15;
        public int DailyTaskGoal { get; set; } = 5;
        public int WeekStartsOn { get; set; } = 1; // 1=Monday
        public bool NotificationsEnabled { get; set; } = true;
        public bool SoundEnabled { get; set; } = true;
        public bool AutoStartPomodoro { get; set; } = false;
        public bool ShowCompletedTasks { get; set; } = true;

        // Relacja nawigacyjna jeden-do-jednego
        public User User { get; set; } = null!;
    }
}
