using System.Collections.Generic;

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
        public TrainingType? TrainingType { get; set; }
        public int? PlanViewId { get; set; } = null;
        public PlanView? PlanView { get; set; } = null;

    }
}
