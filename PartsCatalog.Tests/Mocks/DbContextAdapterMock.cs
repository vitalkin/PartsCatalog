using PartsCatalog.DAL.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PartsCatalog.Tests.Mocks
{
    public class DbSetMock<TEntity> : IDbSet<TEntity> where TEntity : class
    {
        public List<TEntity> Data { get; private set; }

        private IQueryable query;

        public DbSetMock()
        {
            Data = new List<TEntity>();
            query = Data.AsQueryable();
        }

        public TEntity Add(TEntity entity)
        {
            Data.Add(entity);
            return entity;
        }

        public TEntity Attach(TEntity entity)
        {
            Data.Add(entity);
            return entity;
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
        {
            throw new NotImplementedException();
        }

        public TEntity Create()
        {
            return Activator.CreateInstance<TEntity>();
        }

        public TEntity Find(params object[] keyValues)
        {
            var prop = typeof(TEntity).GetProperty("Id");
            if (prop != null && keyValues.Length > 0)
            {
                return Data.FirstOrDefault(item => prop.GetValue(item).Equals(keyValues[0]));
            }
            return null;
        }

        public ObservableCollection<TEntity> Local
        {
            get { return new ObservableCollection<TEntity>(Data); }
        }

        public TEntity Remove(TEntity entity)
        {
            Data.Remove(entity);
            return entity;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }

        public Type ElementType
        {
            get { return query.ElementType; }
        }

        public Expression Expression
        {
            get { return query.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return query.Provider; }
        }
    }

    public class DbContextAdapterMock<TEntity> : IDbContextAdapter<TEntity> where TEntity : class
    {
        private DbSetMock<TEntity> dbSet = new DbSetMock<TEntity>();

        public IDbSet<TEntity> DbSet
        {
            get
            {
                return dbSet;
            }
            set
            {
            }
        }

        public List<TEntity> Data { get { return dbSet.Data; } }

        public int Save()
        {
            return 0;
        }

        public EntityState GetState(TEntity entity)
        {
            return EntityState.Unchanged;
        }

        public void SetState(TEntity entity, EntityState state)
        {
        }

        public void Dispose()
        {
        }
    }
}
