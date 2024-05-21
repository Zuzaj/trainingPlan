using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace trainingPlan.Entities
{
    public class AppDbContext : DbContext
    {
        public DbSet<TrainingEntity> TrainingEntities { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolDB;Trusted_Connection=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}