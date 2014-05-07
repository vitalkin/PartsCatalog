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
    public class MakesRepositoryMock : GenericRepositoryMock<Make>, IMakesRepository
    {
        public void SaveOrUpdate(Make make, HttpPostedFileBase file = null)
        {
        }
    }
}
