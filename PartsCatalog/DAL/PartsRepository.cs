using PartsCatalog.DAL.Context;
using PartsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsCatalog.DAL
{
    public class PartsRepository : GenericRepository<Part>, IPartsRepository
    {
        public PartsRepository(IDbContextAdapter<Part> dbContextAdapter)
            : base(dbContextAdapter)
        {
        }
    }
}