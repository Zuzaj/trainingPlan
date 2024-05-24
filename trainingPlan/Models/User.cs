using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace trainingPlan.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Password { get; set; }
        public ICollection<PlanView> Plans { get; set; } = new List<PlanView>();
    }
}