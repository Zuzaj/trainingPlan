using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace trainingPlan.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;

        [NotMapped]
        public string Password { get; set; } = default!;

        public ICollection<PlanView> PlanViews { get; set; } = new List<PlanView>();

        // Konstruktor
        public User()
        {
            PasswordHash = string.Empty;
        }
    }
}