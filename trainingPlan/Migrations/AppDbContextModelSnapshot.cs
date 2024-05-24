﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace trainingPlan.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("trainingPlan.Models.Difficulty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Difficulties");
                });

            modelBuilder.Entity("trainingPlan.Models.PlanTraining", b =>
                {
                    b.Property<int>("PlanViewId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrainingId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlanViewId", "TrainingId");

                    b.HasIndex("TrainingId");

                    b.ToTable("PlanTrainings");
                });

            modelBuilder.Entity("trainingPlan.Models.PlanView", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalDuration")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("WeekStart")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PlanViews");
                });

            modelBuilder.Entity("trainingPlan.Models.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("DifficultyId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("TrainingTypeId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DifficultyId");

                    b.HasIndex("TrainingTypeId");

                    b.ToTable("Trainings");
                });

            modelBuilder.Entity("trainingPlan.Models.TrainingType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TrainingTypes");
                });

            modelBuilder.Entity("trainingPlan.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("trainingPlan.Models.PlanTraining", b =>
                {
                    b.HasOne("trainingPlan.Models.PlanView", "PlanView")
                        .WithMany("PlanTrainings")
                        .HasForeignKey("PlanViewId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("trainingPlan.Models.Training", "Training")
                        .WithMany("PlanTrainings")
                        .HasForeignKey("TrainingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlanView");

                    b.Navigation("Training");
                });

            modelBuilder.Entity("trainingPlan.Models.PlanView", b =>
                {
                    b.HasOne("trainingPlan.Models.User", "User")
                        .WithMany("PlanViews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("trainingPlan.Models.Training", b =>
                {
                    b.HasOne("trainingPlan.Models.Difficulty", "Difficulty")
                        .WithMany("Trainings")
                        .HasForeignKey("DifficultyId");

                    b.HasOne("trainingPlan.Models.TrainingType", "TrainingType")
                        .WithMany("Trainings")
                        .HasForeignKey("TrainingTypeId");

                    b.Navigation("Difficulty");

                    b.Navigation("TrainingType");
                });

            modelBuilder.Entity("trainingPlan.Models.Difficulty", b =>
                {
                    b.Navigation("Trainings");
                });

            modelBuilder.Entity("trainingPlan.Models.PlanView", b =>
                {
                    b.Navigation("PlanTrainings");
                });

            modelBuilder.Entity("trainingPlan.Models.Training", b =>
                {
                    b.Navigation("PlanTrainings");
                });

            modelBuilder.Entity("trainingPlan.Models.TrainingType", b =>
                {
                    b.Navigation("Trainings");
                });

            modelBuilder.Entity("trainingPlan.Models.User", b =>
                {
                    b.Navigation("PlanViews");
                });
#pragma warning restore 612, 618
        }
    }
}
