// Entities/DailyReviewStatus.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class DailyReviewStatus : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedAt { get; set; }
        public int? MoodRating { get; set; } // 1-5
        public string? DaySummary { get; set; }
        public decimal? ProductiveHours { get; set; }

        // Unikalny klucz: UserId + ReviewDate
        public User User { get; set; } = null!;
    }
}