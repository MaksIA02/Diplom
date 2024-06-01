using DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Project_DAL
{
    public class GenericRepository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        private readonly ApplicationContext _db;
        readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }
        public void Create(TEntity item)
        {
            _dbSet.Add(item);
        }

        public void Delete(TEntity item)
        {
            _dbSet.Remove(item);
        }

        public IEnumerable<TEntity> GetAll()
        {
            var itemList = _dbSet.ToList();
            return itemList;
        }

        public void Update(TEntity item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public TEntity FindById(TKey id)
        {
                return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var itemList = Include(includeProperties).ToList();
            return itemList;
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
