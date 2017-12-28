using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ObjectiveTracker.Models.DTOs
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public IEnumerable<ObjectiveDTO> Objectives { get; set; }

        public static EmployeeDTO ToDTO(Employee e)
        {
            return new EmployeeDTO {
                Id = e.Id,
                FullName = e.FullName,
                Objectives = e.Objectives != null ? e.Objectives.Select(o => ObjectiveDTO.ToDTO(o)) : null
            };
        }
    }
}
