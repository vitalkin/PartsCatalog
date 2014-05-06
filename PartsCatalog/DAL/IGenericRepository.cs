using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PartsCatalog.DAL
{
    public interface IGenericRepository<TEntity> : IDisposable
    {
        void Delete(object id);

        void Delete(TEntity entityToDelete);

        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Expression<Func<TEntity, object>> orderBy = null);

        TEntity GetById(object id);

        void Insert(TEntity entity);

        void Update(TEntity entityToUpdate);
    }
}
