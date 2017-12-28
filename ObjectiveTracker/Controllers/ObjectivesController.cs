using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ObjectiveTracker.DataAccess;
using ObjectiveTracker.DataAccess.Repositories;
using ObjectiveTracker.Models.DTOs;

namespace ObjectiveTracker.Controllers
{
    public class ObjectivesController : Controller
    {
        private readonly ObjectiveTrackerContext _context;
        private ObjectiveRepository objectiveRepository;

        public ObjectivesController(ObjectiveTrackerContext context)
        {
            _context = context;
            objectiveRepository = new ObjectiveRepository(context);
        }

        public async Task<IActionResult> Index(int id)
        {
            var objectives = await objectiveRepository.All().EagerLoad().ForEmployeeId(id).ResultsAsync();

            ViewData["EmployeeId"] = id;
            return View(objectives.Select(o => ObjectiveDTO.ToDTO(o)));
        }

        public IActionResult Create(int employeeId)
        {
            return View(ObjectiveDTO.ToDTO(null, employeeId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,Title,Description")] ObjectiveDTO dto)
        {
            if (ModelState.IsValid)
            {
                objectiveRepository.Add(ObjectiveDTO.ToModel(dto));

                await objectiveRepository.UpdateAsync();

                return RedirectToAction(nameof(Index), new { id = dto.EmployeeId });
            }

            return View(dto);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var objective = await objectiveRepository.GetByIdAsync(id.GetValueOrDefault());

            if (objective is null)
            {
                return NotFound();
            }

            return View(ObjectiveDTO.ToDTO(objective));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,Title,Description")] ObjectiveDTO dto)
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
                var objective = objectiveRepository.GetById(id);
                objective.Title = dto.Title;
                objective.Description = dto.Description;

                await objectiveRepository.UpdateAsync();

                return RedirectToAction(nameof(Index), new { id = dto.EmployeeId });
            }

            return View(dto);
        }
    }
}
