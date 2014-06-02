using PartsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PartsCatalog.DAL
{
    public interface IModelsRepository : IGenericRepository<Model>
    {
        IEnumerable<Model> GetByMakeId(int makeId);

        void SaveOrUpdate(Model model, IEnumerable<HttpPostedFileBase> files);
    }
}
