using PartsCatalog.DAL.Context;
using PartsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PartsCatalog.DAL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private IDbContextAdapter<TEntity> dbContextAdapter;

        private IDbSet<TEntity> dbSet;

        public GenericRepository(IDbContextAdapter<TEntity> dbContextAdapter)
        {
            this.dbContextAdapter = dbContextAdapter;
            dbSet = dbContextAdapter.DbSet;
        }

        public virtual TEntity GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
            dbContextAdapter.Save();
        }

        public virtual void Delete(int id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (dbContextAdapter.GetState(entityToDelete) == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            dbContextAdapter.Save();
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dbContextAdapter.SetState(entityToUpdate, EntityState.Modified);
            dbContextAdapter.Save();
        }

        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> orderBy = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = query.OrderBy(orderBy);
            }

            return query.ToList();
        }

        public void Dispose()
        {
            dbContextAdapter.Dispose();
        }

        public virtual void SaveOrUpdate(TEntity entity, Func<TEntity, int> idSelector)
        {
            var id = idSelector(entity);
            if (id == 0)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }
        }
    }
}