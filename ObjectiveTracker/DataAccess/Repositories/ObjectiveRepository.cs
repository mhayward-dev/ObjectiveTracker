using ObjectiveTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectiveTracker.DataAccess.Repositories
{
    public class ObjectiveRepository : BaseRepository<Objective, ObjectiveRepository>
    {
        public ObjectiveRepository(ObjectiveTrackerContext context) : base(context)
        {
            CurrentRepository = this;
        }

        public override ObjectiveRepository EagerLoad()
        {
            Query = Query.Include(o => o.Tasks);

            return this;
        }

        public ObjectiveRepository ForEmployeeId (int emplId)
        {
            Query = Query.Where(o => o.EmployeeId == emplId);

            return this;
        }

        public Task<Objective> GetByIdAsync(int id, bool eagerLoad = false)
        {
            return eagerLoad ?
                Task.FromResult(All().For(o => o.Id == id).EagerLoad().Results().FirstOrDefault())
                : base.GetByIdAsync(id);
        }
    }
}
