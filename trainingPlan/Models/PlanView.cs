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

        public ICollection<Training> Trainings { get; set; } = new List<Training>();

        public string? Comments { get; set; }
    }
}
