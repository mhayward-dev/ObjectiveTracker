using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ObjectiveTracker.DataAccess;
using ObjectiveTracker.DataAccess.Repositories;
using ObjectiveTracker.Models.DTOs;

namespace ObjectiveTracker.Controllers
{
    public class TasksController : Controller
    {
        private readonly ObjectiveTrackerContext _context;
        private ObjectiveTaskRepository taskRepository;
        private ObjectiveRepository objectiveRepository;

        public TasksController(ObjectiveTrackerContext context)
        {
            _context = context;
            taskRepository = new ObjectiveTaskRepository(context);
            objectiveRepository = new ObjectiveRepository(context);
        }

        public async Task<IActionResult> Create(int objectiveId)
        {
            var objective = await objectiveRepository.GetByIdAsync(objectiveId);

            if (objective is null)
            {
                return NotFound();
            }

            return View(ObjectiveTaskDTO.ToDTO(null, objective.EmployeeId, objective.Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ObjectiveId,EmployeeId,Description,IsComplete")] ObjectiveTaskDTO dto)
        {
            if (ModelState.IsValid)
            {
                taskRepository.Add(ObjectiveTaskDTO.ToModel(dto));

                await taskRepository.UpdateAsync();

                return Redirect($"/Objectives/Index/{dto.EmployeeId}");
            }

            ViewData["ObjectiveId"] = dto.ObjectiveId;
            return View(dto);
        }

        public async Task<IActionResult> Edit(int? id, int employeeId)
        {
            if (id is null)
            {
                return NotFound();
            }

            var ot = await taskRepository.GetByIdAsync(id.GetValueOrDefault());

            if (ot is null)
            {
                return NotFound();
            }

            return View(ObjectiveTaskDTO.ToDTO(ot, employeeId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Description,IsComplete")] ObjectiveTaskDTO dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if (dto is null)
            {
                throw new ArgumentNullException(nameof(dto));
            }

            if (ModelState.IsValid)
            {
                var ot = taskRepository.GetById(id);
                ot.Description = dto.Description;
                ot.IsComplete = dto.IsComplete;

                await taskRepository.UpdateAsync();

                return Redirect($"/Objectives/Index/{ dto.EmployeeId }");
            }

            return View(dto);
        }
    }
}
