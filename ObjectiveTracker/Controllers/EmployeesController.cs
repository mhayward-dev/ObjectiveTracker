using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ObjectiveTracker.DataAccess;
using ObjectiveTracker.DataAccess.Repositories;
using ObjectiveTracker.Models.DTOs;

namespace ObjectiveTracker.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ObjectiveTrackerContext _context;
        private EmployeeRepository employeeRepository;

        public EmployeesController(ObjectiveTrackerContext context)
        {
            _context = context;
            employeeRepository = new EmployeeRepository(context);
        }

        public async Task<IActionResult> Index()
        {
            var employees = await employeeRepository.All().ResultsAsync();

            return View(employees.Select(e => EmployeeDTO.ToDTO(e)));
        }
    }
}
