// Entities/TimeBlockTemplate.cs
using System.Collections.Generic;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class TimeBlockTemplate : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // "Typowy dzień pracy"
        public string? Description { get; set; }
        public int UserId { get; set; }
        public bool IsDefault { get; set; } = false;

        public User User { get; set; } = null!;
        public ICollection<TimeBlockTemplateItem> Items { get; set; } = new List<TimeBlockTemplateItem>();
    }
}