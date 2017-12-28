using System.Collections.Generic;

namespace ObjectiveTracker.Models
{
    public class Objective
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<ObjectiveTask> Tasks { get; set; }
    }
}
