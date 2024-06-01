using DAL;

namespace Project_DAL.Repositories
{
    public class ReceiptRepository : GenericRepository<Guid, Receipt>, IReceiptRepository
    {
        public ReceiptRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
