using ObjectiveTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectiveTracker.DataAccess.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee, EmployeeRepository>
    {
        public EmployeeRepository(ObjectiveTrackerContext context) : base(context)
        {
            CurrentRepository = this;
        }

        public override EmployeeRepository EagerLoad()
        {
            Query = Query.Include(e => e.Objectives).ThenInclude(o => o.Tasks);

            return this;
        }

        public Task<Employee> GetByIdAsync(int id, bool eagerLoad = false)
        {
            return eagerLoad ?
                Task.FromResult(All().For(e => e.Id == id).EagerLoad().Results().FirstOrDefault())
                : base.GetByIdAsync(id);
        }
    }
}
