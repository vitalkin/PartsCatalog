using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PartsCatalog.Controllers;
using PartsCatalog.Models;
using PartsCatalog.DAL;
using PartsCatalog.Tests.Mocks;
using System.Linq;

namespace PartsCatalog.Tests.Tests.Controllers
{
    [TestClass]
    public class CategoriesControllerTest
    {
        private CategoriesController unit;
        private GenericRepository<Category> repository;

        [TestInitialize]
        public void Init()
        {
            repository = new GenericRepository<Category>(new DbContextAdapterMock<Category>());
            unit = new CategoriesController(repository);
        }

        [TestInitialize]
        public void TestDelete()
        {
            var parentCategory = new Category() { Id = 1 };
            var childCategory1 = new Category() { Id = 2, ParentId = 1, Parent = parentCategory };
            var childCategory2 = new Category() { Id = 3, ParentId = 1, Parent = parentCategory };
            var childCategory3 = new Category() { Id = 4, ParentId = 2, Parent = childCategory2 };
            childCategory1.Children = new[] { childCategory3 };
            parentCategory.Children = new[] { childCategory1, childCategory2 };
            var category = new Category() { Id = 5 };

            Array.ForEach(new[] { category, parentCategory, childCategory1, childCategory2, childCategory3 }, c => repository.Insert(c));

            unit.Delete(1);

            var items = repository.Get();

            Assert.AreEqual(1, items.Count());
            Assert.AreEqual(category, items.First());
        }
    }
}
