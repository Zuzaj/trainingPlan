using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace trainingPlan.Models{

    public class Plan
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = default!;

        [Required]
        public DateTime WeekStart { get; set; }

        public int TotalDuration { get; set; }

        public ICollection<Training> Trainings { get; set; } = new List<Training>();

        public string? Comments { get; set; }
    }
}