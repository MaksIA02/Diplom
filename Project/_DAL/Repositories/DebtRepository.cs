using DAL;

namespace Project_DAL.Repositories
{
    public class DebtRepository : GenericRepository<Guid, Debt>, IDebtRepository
    {
        public DebtRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
