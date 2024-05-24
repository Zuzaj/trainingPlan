using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace trainingPlan.Models
{
    public class PlanView
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; } = default!;

        [Required]
        public DateTime WeekStart { get; set; }

        public int TotalDuration { get; set; }

        public ICollection<PlanTraining> PlanTrainings { get; set; } = new List<PlanTraining>();

        public string? Comments { get; set; }
    }
}
