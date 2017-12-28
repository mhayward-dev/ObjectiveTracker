using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using ObjectiveTracker.Controllers;
using ObjectiveTracker.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ObjectiveTrackerTests.Controllers
{
    public class ObjectivesControllerTests
    {
        [Fact]
        public async Task Index_ReturnsObjectiveEnumerable()
        {
            var context = MockDbContext.CreateContext();
            var employeeCount = context.Employees.CountAsync().Result;
            var controller = new ObjectivesController(context);

            var employee = context.Employees.Include(e => e.Objectives).SingleAsync().Result;
            var result = await controller.Index(employee.Id);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ObjectiveDTO>>(viewResult.ViewData.Model);

            Assert.Equal(employee.Objectives.Count, model.Count());
        }

        [Fact]
        public async Task Create_AddsNewObjective()
        {
            var context = MockDbContext.CreateContext();
            var employeeCount = context.Employees.CountAsync().Result;
            var controller = new ObjectivesController(context);

            var employee = context.Employees.Include(e => e.Objectives).SingleAsync().Result;
            var objectiveCount = employee.Objectives.Count;

            var dto = new ObjectiveDTO {
                EmployeeId = employee.Id,
                Title = "An Objective Title",
                Description = "An Objective Description"
            };

            var result = await controller.Create(dto);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            var expectedValues = new Dictionary<string, int>();
            expectedValues.Add("id", dto.EmployeeId);
            Assert.Equal(expectedValues, viewResult.RouteValues.ToDictionary(x => x.Key, x => (int)x.Value ));

            Assert.Equal(objectiveCount + 1, context.Objectives.Count());
        }

        [Fact]
        public async Task Edit_UpdatesObjective()
        {
            var context = MockDbContext.CreateContext();
            var controller = new ObjectivesController(context);

            var employee = context.Employees.Include(e => e.Objectives).SingleAsync().Result;
            var updatedDescription = "New Description Value";

            var dto = ObjectiveDTO.ToDTO(employee.Objectives.Single());
            dto.Description = updatedDescription;

            var result = await controller.Edit(dto.Id, dto);
            var viewResult = Assert.IsType<RedirectToActionResult>(result);

            var expectedValues = new Dictionary<string, int>();
            expectedValues.Add("id", dto.EmployeeId);
            Assert.Equal(expectedValues, viewResult.RouteValues.ToDictionary(x => x.Key, x => (int)x.Value));

            Assert.Equal(updatedDescription, employee.Objectives.Single().Description);
        }
    }
}
