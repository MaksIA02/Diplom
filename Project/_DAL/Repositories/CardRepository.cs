using DAL;

namespace Project_DAL.Repositories
{
    public class CardRepository : GenericRepository<Guid, Card>, ICardRepository
    {
        public CardRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
