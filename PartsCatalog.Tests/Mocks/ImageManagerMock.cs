using PartsCatalog.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PartsCatalog.Tests.Mocks
{
    public class ImageManagerMock : IImageManager
    {
        public void DeleteImage(string name)
        {
        }

        public string SaveImageWithHash(HttpPostedFileBase file)
        {
            return file.FileName;
        }
    }
}
