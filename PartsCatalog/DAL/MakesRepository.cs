using PartsCatalog.Models;
using PartsCatalog.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PartsCatalog.DAL
{
    public class MakesRepository : GenericRepository<Make>, IMakesRepository
    {
        private IImageManager imageManger;

        public MakesRepository(DbContext dbContext, IImageManager imageManger)
            : base(dbContext)
        {
            this.imageManger = imageManger;
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
                Get(make => make.Image == entityToDelete.Image).Count() == 0)   // check if there are no references to image
            {
                imageManger.DeleteImage(entityToDelete.Image);
            }
        }
    }
}