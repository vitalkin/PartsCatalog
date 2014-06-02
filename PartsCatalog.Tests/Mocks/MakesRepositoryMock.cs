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
    public class MakesRepositoryMock : GenericRepository<Make>, IMakesRepository
    {
        public MakesRepositoryMock()
            : base(new DbContextAdapterMock<Make>())
        {
        }

        public void SaveOrUpdate(Make make, HttpPostedFileBase file = null)
        {
        }
    }
}
