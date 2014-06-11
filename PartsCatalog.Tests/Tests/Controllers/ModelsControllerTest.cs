using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartsCatalog.Controllers;
using PartsCatalog.Tests.Mocks;
using PartsCatalog.Models;
using System.Web.Mvc;

namespace PartsCatalog.Tests.Tests.Controllers
{
    [TestClass]
    public class ModelsControllerTest
    {
        private ModelsController unit;
        private ModelsRepositoryMock modelsRepository;
        private MakesRepositoryMock makesRepository;

        [TestInitialize]
        public void Init()
        {
            modelsRepository = new ModelsRepositoryMock();
            makesRepository = new MakesRepositoryMock();
            unit = new ModelsController(modelsRepository, makesRepository);
        }

        [TestMethod]
        public void TestEdit()
        {
            var make = new Make() { Id = 1 };
            var model = new Model() { Id = 2, Make = make };
            modelsRepository.Insert(model);
            makesRepository.Insert(make);
            var result = unit.Edit(1, 2);
            Assert.IsTrue(result is ViewResult);
            Assert.AreEqual(model, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void TestSave()
        {
            var make = new Make() { Id = 1 };
            var model = new Model() { Id = 2, Make = make };
            makesRepository.Insert(make);
            var result = unit.Edit(model, null);
            Assert.IsTrue(result is RedirectToRouteResult);
            Assert.AreEqual(model, modelsRepository.GetById(2));
        }
    }
}
