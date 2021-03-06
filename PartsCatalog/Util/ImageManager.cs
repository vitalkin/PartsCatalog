﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace PartsCatalog.Util
{
    public class ImageManager : IImageManager
    {
        public string ImagesPath { get; private set; }

        public HttpServerUtilityBase Server { get; private set; }

        public ImageManager(HttpServerUtilityBase server, string imagesPath)
        {
            Server = server;
            ImagesPath = imagesPath;
        }

        public string SaveImageWithHash(HttpPostedFileBase file)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var targetFolder = Server.MapPath(ImagesPath);
                var hashName = Convert.ToBase64String(md5.ComputeHash(file.InputStream)).Replace("/","_");
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