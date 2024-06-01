using System.Linq.Expressions;

namespace DAL
{
    public interface IRepository<TKey, TEntity> where TEntity : BaseEntity<TKey>
    {
        IEnumerable<TEntity> GetAll();

        void Create(TEntity item);

        void Delete(TEntity item);

        void Update(TEntity item);

        public TEntity FindById(TKey id);

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);

    }
}
