using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ObjectiveTracker.Models.DTOs
{
    public class ObjectiveDTO
    {
        public int Id { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public List<ObjectiveTaskDTO> Tasks { get; set; }
        public bool IsComplete { get; set; }

        public static ObjectiveDTO ToDTO(Objective o, int employeeId = 0)
        {
            return new ObjectiveDTO
            {
                Id = o?.Id ?? 0,
                EmployeeId = o?.EmployeeId ?? employeeId,
                Title = o?.Title,
                Description = o?.Description,
                Tasks = o?.Tasks != null ? o.Tasks.Select(t => ObjectiveTaskDTO.ToDTO(t, o.EmployeeId)).ToList() : null,
                IsComplete = o?.Tasks != null ? o.Tasks.All(t => t.IsComplete) : false
            };
        }

        public static Objective ToModel(ObjectiveDTO dto)
        {
            return new Objective
            {
                Id = dto.Id,
                EmployeeId = dto.EmployeeId,
                Title = dto.Title,
                Description = dto.Description
            };
        }
    }
}
