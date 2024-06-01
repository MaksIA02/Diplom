using DAL;

namespace Project_DAL.Repositories
{
    public class TransferRepository : GenericRepository<Guid, Transfer>, ITransferRepository
    {
        public TransferRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
