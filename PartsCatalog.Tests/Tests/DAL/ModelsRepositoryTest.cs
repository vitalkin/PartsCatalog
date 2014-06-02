using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartsCatalog.Tests.Mocks;
using PartsCatalog.DAL;
using PartsCatalog.Models;
using PartsCatalog.Util;
using System.Collections.Generic;
using System.Web;
using System.Linq;
using System.Linq.Expressions;

namespace PartsCatalog.Tests.Tests.DAL
{
    [TestClass]
    public class ModelsRepositoryTest
    {
        private ImageManagerMock imageManager;
        private ModelsRepository unit;
        private DbContextAdapterMock<Model> dbContextAdapter;

        [TestInitialize]
        public void Init()
        {
            imageManager = new ImageManagerMock();
            dbContextAdapter = new DbContextAdapterMock<Model>();
            unit = new ModelsRepository(dbContextAdapter, imageManager);
        }

        [TestMethod]
        public void TestSaveWithFile()
        {
            var model = new Model() { Name = "Test" };

            var files = new [] { 
                new HttpPostedFileMock("testImage1.png"),
                new HttpPostedFileMock("testImage2.png"),
                new HttpPostedFileMock("testImage3.png")
            };

            unit.SaveOrUpdate(model, files);

            var collection = dbContextAdapter.Data;
            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual("Test", collection[0].Name);
            Assert.AreEqual("testImage1.png;testImage2.png;testImage3.png", collection[0].Images);
        }

        [TestMethod]
        public void TestDelete()
        {
            var modelToDelete = new Model() { Images = "img2;img4" };

            var collection = dbContextAdapter.Data;
            collection.AddRange(new[] {
                new Model { Images = "img1;img2;img3" },
                new Model { Images = "img2;img3" },
            });

            unit.Delete(modelToDelete);

            Assert.AreEqual(1, imageManager.DeletedImages.Count);
            Assert.AreEqual("img4", imageManager.DeletedImages[0]);
        }
    }
}
