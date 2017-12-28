using ObjectiveTracker.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ObjectiveTracker.DataAccess.Repositories
{
    public class ObjectiveTaskRepository : BaseRepository<ObjectiveTask, ObjectiveTaskRepository>
    {
        public ObjectiveTaskRepository(ObjectiveTrackerContext context) : base(context)
        {
            CurrentRepository = this;
        }

        public override ObjectiveTaskRepository EagerLoad()
        {
            Query = Query.Include(t => t.Objective);

            return this;
        }

        public ObjectiveTask GetById(int id, bool eagerLoad = false)
        {
            return eagerLoad ?
                All().For(t => t.Id == id).EagerLoad().Results().FirstOrDefault()
                : base.GetById(id);
        }
    }
}
