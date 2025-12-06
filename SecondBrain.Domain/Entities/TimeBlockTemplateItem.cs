// Entities/TimeBlockTemplateItem.cs
using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class TimeBlockTemplateItem : BaseEntity
    {
        public int TemplateId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Activity { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int OrderIndex { get; set; }

        public TimeBlockTemplate Template { get; set; } = null!;
        public TimeCategory Category { get; set; } = null!;
    }
}