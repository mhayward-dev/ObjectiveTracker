using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ObjectiveTracker.Controllers;
using ObjectiveTracker.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ObjectiveTrackerTests.Controllers
{
    public class TasksControllerTests
    {
        [Fact]
        public async Task Create_AddsNewTask()
        {
            var context = MockDbContext.CreateContext();
            var employeeCount = context.Employees.CountAsync().Result;
            var controller = new TasksController(context);

            var objective = context.Objectives.Include(o => o.Tasks).Include(o => o.Employee).SingleAsync().Result;
            var taskCount = objective.Tasks.Count;

            var dto = new ObjectiveTaskDTO
            {
                ObjectiveId = objective.Id,
                EmployeeId = objective.Employee.Id,
                Description = "A Task Description",
                IsComplete = false
            };

            var result = await controller.Create(dto);
            var viewResult = Assert.IsType<RedirectResult>(result);

            Assert.Equal($"/Objectives/Index/{ dto.EmployeeId }", viewResult.Url);
            Assert.Equal(taskCount + 1, context.ObjectiveTasks.Count());
        }

        [Fact]
        public async Task Edit_UpdatesTask()
        {
            var context = MockDbContext.CreateContext();
            var employeeCount = context.Employees.CountAsync().Result;
            var controller = new TasksController(context);

            var objective = context.Objectives.Include(o => o.Tasks).Include(o => o.Employee).SingleAsync().Result;
            var dto = ObjectiveTaskDTO.ToDTO(objective.Tasks.Last(), objective.Employee.Id);
            dto.IsComplete = true;

            var result = await controller.Edit(dto.Id, dto);
            var viewResult = Assert.IsType<RedirectResult>(result);

            Assert.Equal($"/Objectives/Index/{ dto.EmployeeId }", viewResult.Url);
            Assert.True(objective.Tasks.Last().IsComplete);
        }
    }
}
