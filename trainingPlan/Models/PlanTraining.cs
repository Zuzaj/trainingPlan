namespace trainingPlan.Models
{
    public class PlanTraining
    {
        public int PlanViewId { get; set; }
        public PlanView PlanView { get; set; } = default!;

        public int TrainingId { get; set; }
        public Training Training { get; set; } = default!;
    }
}
