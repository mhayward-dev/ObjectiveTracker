using System.ComponentModel.DataAnnotations;

namespace ObjectiveTracker.Models.DTOs
{
    public class ObjectiveTaskDTO
    {
        public int Id { get; set; }
        [Required]
        public int ObjectiveId { get; set; }
        public int EmployeeId { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsComplete { get; set; }

        public static ObjectiveTaskDTO ToDTO(ObjectiveTask t, int employeeId, int objectiveId = 0)
        {
            return new ObjectiveTaskDTO {
                Id = t?.Id ?? 0,
                ObjectiveId = t?.ObjectiveId ?? objectiveId,
                EmployeeId = employeeId,
                Description = t?.Description,
                IsComplete = t?.IsComplete ?? false
            };
        }

        public static ObjectiveTask ToModel(ObjectiveTaskDTO dto)
        {
            return new ObjectiveTask {
                Id = dto.Id,
                ObjectiveId = dto.ObjectiveId,
                Description = dto.Description,
                IsComplete = dto.IsComplete
            };
        }
    }
}