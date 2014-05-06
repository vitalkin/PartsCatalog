using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace PartsCatalog.Util
{
    public class ImageManager
    {
        public const string imagesPath = "~/Resources/Images/";

        public static string ImagesPath { get { return imagesPath; } }

        public HttpServerUtilityBase Server { get; set; }

        public ImageManager(HttpServerUtilityBase server)
        {
            Server = server;
        }

        public string SaveImageWithHash(HttpPostedFileBase file)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var targetFolder = Server.MapPath(ImagesPath);
                var hashName = Convert.ToBase64String(md5.ComputeHash(file.InputStream));
                hashName += Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(targetFolder, hashName));

                return hashName;
            }
        }

        public void DeleteImage(string name)
        {
            var target = Server.MapPath(ImagesPath);
            target = Path.Combine(target, name);
            File.Delete(target);
        }

    }
}