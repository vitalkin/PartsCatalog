using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartsCatalog.DAL;
using PartsCatalog.Tests.Mocks;
using PartsCatalog.Models;
using PartsCatalog.Util;
using System.Data.Entity;
using System.Collections.Generic;

namespace PartsCatalog.Tests.Tests.DAL
{
    [TestClass]
    public class MakesRepositoryTest
    {

        private MakesRepository unit;
        private ImageManagerMock imageManager;
        private DbContextAdapterMock<Make> dbContextAdapter;

        [TestInitialize]
        public void Init()
        {
            imageManager = new ImageManagerMock();
            dbContextAdapter = new DbContextAdapterMock<Make>();
            unit = new MakesRepository(dbContextAdapter, imageManager);
        }

        [TestMethod]
        public void TestSaveWithFile()
        {
            var make = new Make() { Name = "Test" };
            var file = new HttpPostedFileMock("testImage.png");

            unit.SaveOrUpdate(make, file);

            var collection = dbContextAdapter.Data;
            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual("Test", collection[0].Name);
            Assert.AreEqual("testImage.png", collection[0].Image);
        }

    }
}
