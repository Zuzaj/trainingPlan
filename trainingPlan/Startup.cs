using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using trainingPlan.Models;
using System.Security.Cryptography;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // dodajemy kontekst bazy
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

        services.AddSession();
        services.AddControllersWithViews();
        services.AddRazorPages();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseSession();

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
        CreateAdminUser(app.ApplicationServices);
        InitData(app.ApplicationServices);
    }
    private void CreateAdminUser(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    Username = "admin",
                    PasswordHash = HashPassword("admin123"),
                };

                context.Users.Add(adminUser);
                context.SaveChanges();
            }
            // if (!context.Difficulties.Any())
            // {
            //     context.Difficulties.AddRange(
            //         new Difficulty { Name = "Recovery" },
            //         new Difficulty { Name = "Easy" },
            //         new Difficulty { Name = "Medium" },
            //         new Difficulty { Name = "Hard" },
            //         new Difficulty { Name = "Turbo Hard" }
            //     );
            // }

            // Check and add initial data for TrainingType
            // if (!context.TrainingTypes.Any())
            // {
            //     context.TrainingTypes.AddRange(
            //         new TrainingType { Name = "Strength" },
            //         new TrainingType { Name = "Cardio" },
            //         new TrainingType { Name = "Endurance" },
            //         new TrainingType { Name = "Intervals" },
            //         new TrainingType { Name = "Mobility" },
            //         new TrainingType { Name = "Functional" },
            //         new TrainingType { Name = "Sport-Specific" },
            //         new TrainingType { Name = "Active Recovery" }
            //     );
            // }
            //  if (!context.Trainings.Any()){

            //  }
            // context.SaveChanges();
        }
    }
private void InitData(IServiceProvider serviceProvider)
    {
         using (var scope = serviceProvider.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!context.Difficulties.Any())
            {
                context.Difficulties.AddRange(
                    new Difficulty { Name = "Recovery" },
                    new Difficulty { Name = "Easy" },
                    new Difficulty { Name = "Medium" },
                    new Difficulty { Name = "Hard" },
                    new Difficulty { Name = "Turbo Hard" }
                );
            }

            // Check and add initial data for TrainingType
            if (!context.TrainingTypes.Any())
            {
                context.TrainingTypes.AddRange(
                    new TrainingType { Name = "Strength" },
                    new TrainingType { Name = "Cardio" },
                    new TrainingType { Name = "Endurance" },
                    new TrainingType { Name = "Intervals" },
                    new TrainingType { Name = "Mobility" },
                    new TrainingType { Name = "Functional" },
                    new TrainingType { Name = "Sport-Specific" },
                    new TrainingType { Name = "Active Recovery" }
                );
            }
             if (!context.Trainings.Any()){
                    context.Trainings.AddRange(
                new Training { Name = "Morning Run", Description = "A quick morning run to start the day", Duration = 30, DifficultyId = 2, TrainingTypeId = 2 },
                new Training { Name = "Evening Cycling", Description = "Relaxing evening cycling session", Duration = 45, DifficultyId = 3, TrainingTypeId = 3 },
                new Training { Name = "Lap Swimming", Description = "Swim laps to build endurance", Duration = 60, DifficultyId = 4, TrainingTypeId = 3 },
                new Training { Name = "Rowing Workout", Description = "High-intensity rowing workout", Duration = 40, DifficultyId = 3, TrainingTypeId = 1 },
                new Training { Name = "Jump Rope Session", Description = "Cardio workout with jump rope", Duration = 20, DifficultyId = 1, TrainingTypeId = 2 },
                new Training { Name = "HIIT Blast", Description = "High-intensity interval training", Duration = 30, DifficultyId = 4, TrainingTypeId = 4 },
                new Training { Name = "Weightlifting Basics", Description = "Introduction to weightlifting", Duration = 60, DifficultyId = 2, TrainingTypeId = 1 },
                new Training { Name = "Bodyweight Exercises", Description = "Full body workout with bodyweight exercises", Duration = 45, DifficultyId = 3, TrainingTypeId = 1 },
                new Training { Name = "Powerlifting Routine", Description = "Powerlifting session focusing on strength", Duration = 90, DifficultyId = 4, TrainingTypeId = 1 },
                new Training { Name = "Olympic Weightlifting", Description = "Training for Olympic weightlifting", Duration = 90, DifficultyId = 4, TrainingTypeId = 1 },
                new Training { Name = "Resistance Band Training", Description = "Strength training with resistance bands", Duration = 30, DifficultyId = 2, TrainingTypeId = 1 },
                new Training { Name = "Yoga for Beginners", Description = "Introductory yoga session", Duration = 60, DifficultyId = 2, TrainingTypeId = 5 },
                new Training { Name = "Pilates Class", Description = "Pilates session for core strength", Duration = 50, DifficultyId = 3, TrainingTypeId = 5 },
                new Training { Name = "Stretching Routine", Description = "Full body stretching routine", Duration = 30, DifficultyId = 1, TrainingTypeId = 5 },
                new Training { Name = "Tai Chi Practice", Description = "Tai Chi session for balance and relaxation", Duration = 45, DifficultyId = 2, TrainingTypeId = 5 },
                new Training { Name = "Marathon Training", Description = "Long-distance run training for marathons", Duration = 120, DifficultyId = 4, TrainingTypeId = 3 },
                new Training { Name = "Trail Hiking", Description = "Hiking session on a nature trail", Duration = 180, DifficultyId = 3, TrainingTypeId = 3 },
                new Training { Name = "Balance Board Training", Description = "Exercises using a balance board", Duration = 40, DifficultyId = 3, TrainingTypeId = 6 },
                new Training { Name = "Stability Ball Workout", Description = "Workout session with stability ball", Duration = 50, DifficultyId = 3, TrainingTypeId = 6 },
                new Training { Name = "Single-Leg Exercises", Description = "Strength and balance training on one leg", Duration = 30, DifficultyId = 3, TrainingTypeId = 6 },
                new Training { Name = "Kettlebell Workout", Description = "Functional training with kettlebells", Duration = 45, DifficultyId = 4, TrainingTypeId = 6 },
                new Training { Name = "TRX Suspension Training", Description = "Functional training using TRX", Duration = 50, DifficultyId = 3, TrainingTypeId = 6 },
                new Training { Name = "Plyometrics Session", Description = "Explosive movement training", Duration = 35, DifficultyId = 4, TrainingTypeId = 6 },
                new Training { Name = "Soccer Drills", Description = "Sport-specific soccer training", Duration = 60, DifficultyId = 3, TrainingTypeId = 7 },
                new Training { Name = "Basketball Training", Description = "Sport-specific basketball training", Duration = 90, DifficultyId = 3, TrainingTypeId = 7 },
                new Training { Name = "Tennis Practice", Description = "Sport-specific tennis training", Duration = 60, DifficultyId = 3, TrainingTypeId = 7 },
                new Training { Name = "Football Conditioning", Description = "Sport-specific football training", Duration = 90, DifficultyId = 4, TrainingTypeId = 7 },
                new Training { Name = "Martial Arts Practice", Description = "Training in martial arts techniques", Duration = 60, DifficultyId = 4, TrainingTypeId = 7 },
                new Training { Name = "Meditation Session", Description = "Guided meditation for relaxation", Duration = 30, DifficultyId = 2, TrainingTypeId = 8 },
                new Training { Name = "Mindfulness Training", Description = "Mindfulness practices for mental clarity", Duration = 45, DifficultyId = 2, TrainingTypeId = 8 },
                new Training { Name = "Breathing Exercises", Description = "Techniques for better breathing", Duration = 20, DifficultyId = 2, TrainingTypeId = 8 },
                new Training { Name = "Physical Therapy Session", Description = "Rehabilitation exercises", Duration = 60, DifficultyId = 2, TrainingTypeId = 8 },
                new Training { Name = "Foam Rolling", Description = "Recovery session with foam roller", Duration = 30, DifficultyId = 2, TrainingTypeId = 8 },
                new Training { Name = "Aqua Therapy", Description = "Low-impact exercises in water", Duration = 45, DifficultyId = 2, TrainingTypeId = 8 },
                new Training { Name = "CrossFit WOD", Description = "High-intensity CrossFit workout", Duration = 60, DifficultyId = 4, TrainingTypeId = 6 },
                new Training { Name = "Boot Camp", Description = "Intense group training session", Duration = 60, DifficultyId = 3, TrainingTypeId = 6 },
                new Training { Name = "Group Fitness Class", Description = "Instructor-led group fitness class", Duration = 60, DifficultyId = 3, TrainingTypeId = 6 }
            );
             }
            context.SaveChanges();
    }
    }
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            var builder = new System.Text.StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}