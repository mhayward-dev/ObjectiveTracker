using ObjectiveTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ObjectiveTracker.Models.DTOs;

namespace ObjectiveTracker.DataAccess
{
    public class ObjectiveTrackerContext : DbContext
    {
        public ObjectiveTrackerContext(DbContextOptions<ObjectiveTrackerContext> options) : base(options)
        { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Objective> Objectives { get; set; }
        public DbSet<ObjectiveTask> ObjectiveTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
