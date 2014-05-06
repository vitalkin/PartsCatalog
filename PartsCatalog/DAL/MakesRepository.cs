using PartsCatalog.Models;
using PartsCatalog.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PartsCatalog.DAL
{
    public class MakesRepository : GenericRepository<Make>
    {
        private ImageManager imageManger;

        public MakesRepository(HttpServerUtilityBase server)
        {
            imageManger = new ImageManager(server);
        }

        public void SaveOrUpdate(Make make, HttpPostedFileBase file = null)
        {
            if (file != null && file.ContentLength > 0)
            {
                make.Image = imageManger.SaveImageWithHash(file);
            }

            if (make.Id > 0)
            {
                Update(make);
            }
            else
            {
                Insert(make);
            }
        }

        public override void Delete(Make entityToDelete)
        {
            base.Delete(entityToDelete);
            if (!String.IsNullOrEmpty(entityToDelete.Image) && 
                Get(make => make.Image == entityToDelete.Image).Count() == 0)
            {
                imageManger.DeleteImage(entityToDelete.Image);
            }
        }
    }
}