using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SecondBrain.Domain.Enums
{
    public enum TaskPriority
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Urgent = 4
    }

    public enum TaskCategory
    {
        Personal = 1,
        Work = 2,
        Study = 3,
        Health = 4,
        Other = 5
    }
}