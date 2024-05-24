﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace trainingPlan.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240523175225_AddPlans")]
    partial class AddPlans
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("PlanTraining", b =>
                {
                    b.Property<int>("PlanId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TrainingsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlanId", "TrainingsId");

                    b.HasIndex("TrainingsId");

                    b.ToTable("PlanTrainings", (string)null);
                });

            modelBuilder.Entity("trainingPlan.Models.Plan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comments")
                        .HasColumnType("TEXT");

                    b.Property<int>("TotalDuration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("WeekStart")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("trainingPlan.Models.Training", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Difficulty")
                        .HasColumnType("TEXT");

                    b.Property<int>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Trainings");
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

            modelBuilder.Entity("PlanTraining", b =>
                {
                    b.HasOne("trainingPlan.Models.Plan", null)
                        .WithMany()
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("trainingPlan.Models.Training", null)
                        .WithMany()
                        .HasForeignKey("TrainingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
