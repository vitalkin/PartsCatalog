using PartsCatalog.DAL.Context;
using PartsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace PartsCatalog.DAL
{
    public class PartsRepository : GenericRepository<Part>, IPartsRepository
    {
        public PartsRepository(IDbContextAdapter<Part> dbContextAdapter)
            : base(dbContextAdapter)
        {
        }

        public IEnumerable<Part> Get(int? categoryId, int? modelId)
        {
            return Get(p => (categoryId != null ? categoryId == p.CategoryId : true) 
                    && (modelId != null ? modelId == p.ModelId : true), p => p.Name);
        }

    }
}