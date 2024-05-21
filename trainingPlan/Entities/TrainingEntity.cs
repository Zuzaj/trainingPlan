using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trainingPlan.Entities
{
    public class TrainingEntity
    {
        public int Id { get; set;}
        public string Name {get; set; } = default!;
    public string Description {get; set; } = default!;
        public string? Type {get; set; }
        public int Duration {get; set; } = default!;
        public string? Difficulty {get; set;}
    }
}