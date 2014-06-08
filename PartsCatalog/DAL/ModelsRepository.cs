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
    public class ModelsRepository : GenericRepository<Model>, IModelsRepository
    {
        private IImageManager imageManager;

        public ModelsRepository(IDbContextAdapter<Model> dbContextAdapter, IImageManager imageManager)
            : base(dbContextAdapter)
        {
            this.imageManager = imageManager;
        }

        public IEnumerable<Model> GetByMakeId(int makeId)
        {
            return Get(model => model.Make.Id == makeId);
        }

        public void SaveOrUpdate(Model model, IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                var hashNames = new List<string>();
                foreach (var file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        hashNames.Add(imageManager.SaveImageWithHash(file));
                    }
                }
                model.Images = String.Join(";", hashNames.ToArray());
            }
            SaveOrUpdate(model, m => m.Id);
        }

        public override void Update(Model entityToUpdate)
        {
            var entity = GetById(entityToUpdate.Id);
            dbContextAdapter.SetState(entity, EntityState.Detached);

            if (String.IsNullOrEmpty(entityToUpdate.Images))
            {
                entityToUpdate.Images = entity.Images;
            }

            base.Update(entityToUpdate);

            if (entity.Images != entityToUpdate.Images)
            {
                DeleteImages(entity.GetImages());
            }
        }

        public override void Delete(Model entityToDelete)
        {
            base.Delete(entityToDelete);

            if (!String.IsNullOrEmpty(entityToDelete.Images))
            {
                DeleteImages(entityToDelete.GetImages());
            }
        }

        private void DeleteImages(string[] images)
        {
            foreach (var image in images)
            {
                var models = Get(m => !String.IsNullOrEmpty(m.Images));
                if (models.Where(model => model.GetImages().Contains(image)).Count() == 0)
                {
                    imageManager.DeleteImage(image);
                }
            }
        }
    }
}