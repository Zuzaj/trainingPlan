using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using trainingPlan.Models;


    public class AppDbContext : DbContext
    {

         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<User> Users { get; set; }
    }
