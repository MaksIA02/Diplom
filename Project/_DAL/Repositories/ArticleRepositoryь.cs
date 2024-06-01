using DAL;

namespace Project_DAL.Repositories
{
    public class ArticleRepository : GenericRepository<Guid, Article>, IArticleRepository
    {
        public ArticleRepository(ApplicationContext db) : base(db)
        {
        }
    }
}
