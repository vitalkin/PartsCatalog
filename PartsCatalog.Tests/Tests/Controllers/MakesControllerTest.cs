using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartsCatalog.Controllers;
using PartsCatalog.Tests.Mocks;
using PartsCatalog.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PartsCatalog.Tests.Controllers
{
    [TestClass]
    public class MakesControllerTest
    {
        private MakesController unit;
        private MakesRepositoryMock repository;

        [TestInitialize]
        public void Init()
        {
            repository = new MakesRepositoryMock();
            repository.Entities = new List<Make>();
            unit = new MakesController(repository, new ImageManagerMock());
        }

        [TestMethod]
        public void TestEdit()
        {
            var make = new Make() { Id = 1 };
            repository.Entities.Add(make);
            var result = unit.Edit(1);
            Assert.IsTrue(result is ViewResult);
            Assert.AreEqual(make, ((ViewResult)result).Model);
        }

        [TestMethod]
        public void TestEditRedirect()
        {
            var result = unit.Edit(1);
            Assert.IsTrue(result is RedirectToRouteResult);
            Assert.AreEqual("List", ((RedirectToRouteResult)result).RouteValues["action"]);
        }
    }
}
