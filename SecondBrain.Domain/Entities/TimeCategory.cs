using System.Collections.Generic;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class TimeCategory : BaseEntity
    {
        public string Name { get; set; } = string.Empty; // "Praca", "Nauka", "Sport"
        public string Color { get; set; } = "#cccccc";
        public string? Icon { get; set; }
        public int UserId { get; set; }
        public bool IsProductiveTime { get; set; } = true;
        public int OrderIndex { get; set; }

        public User User { get; set; } = null!;
        public ICollection<TimeBlock> TimeBlocks { get; set; } = new List<TimeBlock>();
        public ICollection<TimeBlockTemplateItem> TemplateItems { get; set; } = new List<TimeBlockTemplateItem>();
    }
}