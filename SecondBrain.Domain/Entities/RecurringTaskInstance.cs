using System;
using SecondBrain.Domain.Common;

namespace SecondBrain.Domain.Entities
{
    public class RecurringTaskInstance : BaseEntity
    {
        public int TemplateId { get; set; }
        public int TaskId { get; set; }
        public DateTime ScheduledDate { get; set; }

        public RecurringTaskTemplate Template { get; set; } = null!;
        public Task Task { get; set; } = null!;
    }
}