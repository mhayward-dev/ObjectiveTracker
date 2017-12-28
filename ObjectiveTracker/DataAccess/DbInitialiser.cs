using ObjectiveTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectiveTracker.DataAccess
{
    public static class DbInitialiser
    {
        public static void Initialise(ObjectiveTrackerContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Employees.Any())
            {
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
            }
        }
    }
}
