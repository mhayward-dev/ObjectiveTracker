using Microsoft.EntityFrameworkCore;
using ObjectiveTracker.DataAccess;
using ObjectiveTracker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectiveTrackerTests
{
    public static class MockDbContext
    {
        public static ObjectiveTrackerContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ObjectiveTrackerContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new ObjectiveTrackerContext(options);

            var empl = new Employee
            {
                FullName = "Test Employee",
                Objectives = new List<Objective> {
                        new Objective {
                            Title = "Complete the new project",
                            Description = "Project to be deployed by May 2018",
                            Tasks = new List<ObjectiveTask> {
                                new ObjectiveTask { Description = "Complete project planning", IsComplete = true },
                                new ObjectiveTask { Description = "Complete all development", IsComplete = false },
                                new ObjectiveTask { Description = "Sign-off development", IsComplete = false },
                                new ObjectiveTask { Description = "Deploy project", IsComplete = false }
                            }
                        }
                    }
            };

            context.Employees.Add(empl);
            context.SaveChanges();

            return context;
        }
    }
}
