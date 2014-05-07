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
        private class MakesRepositoryTestClass : MakesRepository
        {
            public GenericRepositoryMock<Make> GenericRepository { get; set; }

            public MakesRepositoryTestClass(IImageManager imageManager)
                : base(new DbContextMock(), imageManager)
            {
                GenericRepository = new GenericRepositoryMock<Make>() { Entities = new List<Make>() };
            }

            public override void Insert(Make entity)
            {
                GenericRepository.Insert(entity);
            }

            public override void Update(Make entityToUpdate)
            {
                GenericRepository.Update(entityToUpdate);
            }

        }

        private MakesRepositoryTestClass unit;
        private ImageManagerMock imageManager;

        [TestInitialize]
        public void Init()
        {
            imageManager = new ImageManagerMock();
            unit = new MakesRepositoryTestClass(imageManager);
        }

        [TestMethod]
        public void TestSaveWithFile()
        {
            var make = new Make() { Name = "Test" };
            var file = new HttpPostedFileMock("testImage.png");

            unit.SaveOrUpdate(make, file);

            var collection = unit.GenericRepository.Entities;
            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual("Test", collection[0].Name);
            Assert.AreEqual("testImage.png", collection[0].Image);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var make = new Make() { Id = 1, Name = "Test"};
            var collection = unit.GenericRepository.Entities;
            collection.Add(make);

            unit.SaveOrUpdate(make, null);

            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual(make, collection[0]);

        }

    }
}
