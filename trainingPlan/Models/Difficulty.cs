namespace trainingPlan.Models
{
    public class Difficulty
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;

        public ICollection<Training> Trainings { get; set; } = new List<Training>();
    }
}