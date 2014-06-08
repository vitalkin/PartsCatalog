using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PartsCatalog.DAL.Context
{
    public class DbContextAdapter<TEntity> : IDbContextAdapter<TEntity> where TEntity : class
    {
        public DbContext DbContext { get; private set; }

        public IDbSet<TEntity> DbSet { get; set; }

        public DbContextAdapter(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<TEntity>();
        }

        public int Save()
        {
            return DbContext.SaveChanges();
        }

        public EntityState GetState(TEntity entity)
        {
            return DbContext.Entry(entity).State;
        }

        public void SetState(TEntity entity, EntityState state)
        {
            DbContext.Entry(entity).State = state;
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }

    }
}