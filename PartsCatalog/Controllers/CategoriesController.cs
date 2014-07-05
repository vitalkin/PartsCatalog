using PartsCatalog.DAL;
using PartsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartsCatalog.Controllers
{
    public class CategoriesController : Controller
    {
        private IGenericRepository<Category> categoriesRepository;

        public CategoriesController(IGenericRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public ActionResult List(int? makeId, int? modelId)
        {
            var categories = categoriesRepository.Get().ToList();
            return View(categories);
        }

        [HttpPost]
        public ActionResult Add(string name, int? parent)
        {
            var category = new Category() { Name = name, ParentId = parent };
            categoriesRepository.Insert(category);
            return Json(new { message = "Saved successfully", id = category.Id });
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var category = categoriesRepository.GetById(id);
            DeleteCategoryAndSubcategories(category);
            return Json(new { message = "Removed successfully" });
        }

        private void DeleteCategoryAndSubcategories(Category category)
        {
            var children = new List<Category>(category.Children);
            foreach (var subcategory in children)
            {
                DeleteCategoryAndSubcategories(subcategory);
            }
            categoriesRepository.Delete(category);
        }

    }
}
