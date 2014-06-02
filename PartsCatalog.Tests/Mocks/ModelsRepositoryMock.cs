using PartsCatalog.DAL;
using PartsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PartsCatalog.Tests.Mocks
{
    public class ModelsRepositoryMock : GenericRepository<Model>, IModelsRepository
    {
        public ModelsRepositoryMock()
            : base(new DbContextAdapterMock<Model>())
        {
        }

        public IEnumerable<Model> GetByMakeId(int makeId)
        {
            return Get(m => m.Make.Id == makeId);
        }

        public void SaveOrUpdate(Model model, IEnumerable<HttpPostedFileBase> files)
        {
            SaveOrUpdate(model, m => m.Id);
        }
    }
}
