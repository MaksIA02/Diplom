using DAL;

namespace Project_DAL.Repositories
{
    public class ExpenseRepository : GenericRepository<Guid, Expense>, IExpenseRepository
    {
        public ExpenseRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
