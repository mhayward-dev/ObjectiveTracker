namespace ObjectiveTracker.Models
{
    public class ObjectiveTask
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsComplete { get; set; }
        public int ObjectiveId { get; set; }
        public Objective Objective { get; set; }
    }
}
