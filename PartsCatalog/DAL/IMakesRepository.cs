using PartsCatalog.Models;
using System;
using System.Web;
namespace PartsCatalog.DAL
{
    public interface IMakesRepository : IGenericRepository<Make>
    {
        void SaveOrUpdate(Make make, HttpPostedFileBase file = null);
    }
}
