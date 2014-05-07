using PartsCatalog.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PartsCatalog.Tests.Mocks
{
    public class GenericRepositoryMock<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public List<TEntity> Entities { get; set; }

        public void Delete(object id)
        {
            Entities.Remove(GetById(id));
        }

        public void Delete(TEntity entityToDelete)
        {
            Entities.Remove(entityToDelete);
        }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null, 
            Expression<Func<TEntity, object>> orderBy = null)
        {
            IQueryable<TEntity> queryable = Entities.AsQueryable();

            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }
            if (orderBy != null)
            {
                queryable = queryable.OrderBy(filter);
            }

            return queryable.ToList();
        }

        public TEntity GetById(object id)
        {
            var property = typeof(TEntity).GetProperty("Id");
            if (property == null)
            {
                return null;
            }
            return Entities.FirstOrDefault(entity => property.GetValue(entity).Equals(id));
        }

        public void Insert(TEntity entity)
        {
            Entities.Add(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
        }

        public void Dispose()
        {
        }
    }
}
