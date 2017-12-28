﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using ObjectiveTracker.DataAccess;
using System;

namespace ObjectiveTracker.Migrations
{
    [DbContext(typeof(ObjectiveTrackerContext))]
    partial class ObjectiveTrackerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ObjectiveTracker.Models.DTOs.ObjectiveTaskDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("EmployeeId");

                    b.Property<bool>("IsComplete");

                    b.Property<int>("ObjectiveId");

                    b.HasKey("Id");

                    b.ToTable("ObjectiveTaskDTO");
                });

            modelBuilder.Entity("ObjectiveTracker.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullName");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ObjectiveTracker.Models.Objective", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<int>("EmployeeId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Objectives");
                });

            modelBuilder.Entity("ObjectiveTracker.Models.ObjectiveTask", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsComplete");

                    b.Property<int>("ObjectiveId");

                    b.HasKey("Id");

                    b.HasIndex("ObjectiveId");

                    b.ToTable("ObjectiveTasks");
                });

            modelBuilder.Entity("ObjectiveTracker.Models.Objective", b =>
                {
                    b.HasOne("ObjectiveTracker.Models.Employee", "Employee")
                        .WithMany("Objectives")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ObjectiveTracker.Models.ObjectiveTask", b =>
                {
                    b.HasOne("ObjectiveTracker.Models.Objective", "Objective")
                        .WithMany("Tasks")
                        .HasForeignKey("ObjectiveId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
