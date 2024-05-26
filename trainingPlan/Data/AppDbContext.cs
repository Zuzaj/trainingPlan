using Microsoft.EntityFrameworkCore;
using trainingPlan.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<PlanView> PlanViews { get; set; }
    public DbSet<Training> Trainings { get; set; }
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<TrainingType> TrainingTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuring one-to-many relationship between User and PlanView
        modelBuilder.Entity<PlanView>()
            .HasOne(p => p.User)
            .WithMany(u => u.PlanViews)
            .HasForeignKey(p => p.UserId);

        modelBuilder.Entity<Training>()
            .HasOne(t => t.PlanView)
            .WithMany(p => p.Trainings)
            .HasForeignKey(t => t.PlanViewId);

        // Configuring one-to-many relationship between Difficulty and Training
        modelBuilder.Entity<Training>()
            .HasOne(t => t.Difficulty)
            .WithMany(d => d.Trainings)
            .HasForeignKey(t => t.DifficultyId);

        // Configuring one-to-many relationship between TrainingType and Training
        modelBuilder.Entity<Training>()
            .HasOne(t => t.TrainingType)
            .WithMany(tt => tt.Trainings)
            .HasForeignKey(t => t.TrainingTypeId);
            modelBuilder.Entity<PlanView>()
        .HasMany(p => p.Trainings)
        .WithOne(t => t.PlanView)
        .OnDelete(DeleteBehavior.Cascade);    
    }
}
