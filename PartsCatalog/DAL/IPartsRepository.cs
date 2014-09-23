using PartsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartsCatalog.DAL
{
    public interface IPartsRepository : IGenericRepository<Part>
    {
        IEnumerable<Part> Get(int? categoryId, int? modelId);
    }
}
