using PartsCatalog.DAL.Context;
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
        private IImageManager imageManager;

        public MakesRepository(IDbContextAdapter<Make> dbContextAdapter, IImageManager imageManager)
            : base(dbContextAdapter)
        {
            this.imageManager = imageManager;
        }

        public void SaveOrUpdate(Make make, HttpPostedFileBase file = null)
        {
            if (file != null && file.ContentLength > 0)
            {
                make.Image = imageManager.SaveImageWithHash(file);
            }
            SaveOrUpdate(make, m => m.Id);
        }

        public override void Delete(Make entityToDelete)
        {
            base.Delete(entityToDelete);
            if (!String.IsNullOrEmpty(entityToDelete.Image) && 
                Get(make => make.Image == entityToDelete.Image).Count() == 0)   // check if there are no references to image
            {
                imageManager.DeleteImage(entityToDelete.Image);
            }
        }
    }
}