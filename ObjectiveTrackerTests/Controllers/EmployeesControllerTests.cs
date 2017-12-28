using Microsoft.EntityFrameworkCore;
using ObjectiveTracker.Controllers;
using ObjectiveTracker.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System.Linq;

namespace ObjectiveTrackerTests.Controllers
{
    public class EmployeesControllerTests
    {
        [Fact]
        public async Task Index_ReturnsEmployeeEnumerable()
        {
            var context = MockDbContext.CreateContext();
            var employeeCount = context.Employees.CountAsync().Result;
            var controller = new EmployeesController(context);

            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<EmployeeDTO>>(viewResult.ViewData.Model);

            Assert.Equal(employeeCount, model.Count());
        }
    }
}
