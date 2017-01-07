using BookStore.Contracts.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DAL
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal BookStoreEntities Context;
        internal DbSet<TEntity> Entities;

        public EFGenericRepository(BookStoreEntities dbContext)
        {
            Context = dbContext;
            Entities = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        /// <summary>
        /// Get entity the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        /// <summary>
        /// Get entity by identifiers.
        /// </summary>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        public TEntity GetById(params object[] keyValues)
        {
            return Entities.Find(keyValues);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Insert(TEntity entity)
        {
            Entities.Add(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        public void Update(TEntity entityToUpdate)
        {
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// Inserts the entity if not exist or update the entity if exist.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void InsertOrUpdate(TEntity entity)
        {
            if (!Exists(entity))
                Insert(entity);
            else
                Update(entity);
        }

        /// <summary>
        /// Deletes entity by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void Delete(object id)
        {
            var entityToDelete = Entities.Find(id);
            Entities.Remove(entityToDelete);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entityToDelete">The entity to delete.</param>
        public void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                Entities.Attach(entityToDelete);
            }
            Entities.Remove(entityToDelete);
        }

        /// <summary>
        /// Check if the entity exist.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public bool Exists(TEntity entity)
        {
            var objContext = ((IObjectContextAdapter)Context).ObjectContext;
            var objSet = objContext.CreateObjectSet<TEntity>();
            var entityKey = objContext.CreateEntityKey(objSet.EntitySet.Name, entity);

            Object foundEntity;
            var exists = objContext.TryGetObjectByKey(entityKey, out foundEntity);
            return (exists);
        }

        /// <summary>
        /// Counts the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public long Count(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.Count();
        }
    }

}
