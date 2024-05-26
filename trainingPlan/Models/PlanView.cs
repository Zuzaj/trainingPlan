using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace trainingPlan.Models
{
    public class PlanView
    {
        public int Id { get; set; }

        public int? UserId { get; set; } = null;
        public User? User { get; set; } = null;

        [Required]
        public DateTime WeekStart { get; set; }

        public int TotalDuration { get; set; }

        public ICollection<Training>? Trainings { get; set; } = new List<Training>();
        [NotMapped]
        public List<int> TrainingIds { get; set; } = new List<int>();
        public string? Comments { get; set; }
    }
}
