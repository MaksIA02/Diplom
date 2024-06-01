using DAL;

namespace Project_DAL.Repositories
{
    public class GoalRepository : GenericRepository<Guid, Goal>, IGoalRepository
    {
        public  GoalRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
