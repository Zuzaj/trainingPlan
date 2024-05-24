using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trainingPlan.Models
{

    public class Training
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Duration { get; set; } = default!;

        public int? DifficultyId { get; set; }
        public Difficulty? Difficulty { get; set; }

        public int? TrainingTypeId { get; set; }
        public TrainingType? Type { get; set; }

        public ICollection<PlanView> Plans { get; set; } = new List<PlanView>();
    }
}