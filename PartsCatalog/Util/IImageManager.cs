using System;
using System.Web;
namespace PartsCatalog.Util
{
    public interface IImageManager
    {
        void DeleteImage(string name);

        string SaveImageWithHash(HttpPostedFileBase file);
    }
}
