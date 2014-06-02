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
        public List<string> DeletedImages { get; set; }

        public ImageManagerMock()
        {
            DeletedImages = new List<string>();
        }

        public void DeleteImage(string name)
        {
            DeletedImages.Add(name);
        }

        public string SaveImageWithHash(HttpPostedFileBase file)
        {
            return file.FileName;
        }
    }
}
