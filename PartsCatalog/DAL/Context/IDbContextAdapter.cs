using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsCatalog.DAL.Context
{
    public interface IDbContextAdapter<TEntity> : IDisposable where TEntity : class
    {
        IDbSet<TEntity> DbSet { get; set; }

        int Save();

        EntityState GetState(TEntity entity);

        void SetState(TEntity entity, EntityState state);
    }
}
