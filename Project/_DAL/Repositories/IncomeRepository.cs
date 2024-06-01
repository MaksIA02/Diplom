using DAL;

namespace Project_DAL.Repositories
{
    public class IncomeRepository : GenericRepository<Guid, Income>, IIncomeRepository
    {
        public IncomeRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
