using System.Collections.Generic;

namespace ObjectiveTracker.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<Objective> Objectives { get; set; }
    }
}
