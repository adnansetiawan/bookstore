using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Contracts.DAL
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, System.Linq.IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");

        TEntity GetById(object id);

        TEntity GetById(params object[] keyValues);

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);

        void InsertOrUpdate(TEntity entity);

        void Delete(object id);

        void Delete(TEntity entityToDelete);

        bool Exists(TEntity entity);

        int Count(Expression<Func<TEntity, bool>> filter = null);
    }
}
