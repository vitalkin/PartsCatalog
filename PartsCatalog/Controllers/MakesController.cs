using PartsCatalog.DAL;
using PartsCatalog.Models;
using PartsCatalog.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartsCatalog.Controllers
{
    public class MakesController : Controller
    {

        public ActionResult List()
        {
            using (var repo = new MakesRepository(Server))
            {
                return View(repo.Get(orderBy: make => make.Name));
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var repo = new MakesRepository(Server))
            {
                var make = repo.GetById(id);
                return make == null ? RedirectToAction("List") : (ActionResult) View(make);
            }
        }

        [HttpPost]
        public ActionResult Edit(Make make, HttpPostedFileBase file)
        {
            using (var repo = new MakesRepository(Server))
            {
                repo.SaveOrUpdate(make, file);
            }
            return RedirectToAction("Edit", new { id = make.Id });
        }

        public ActionResult Add()
        {
            return View("Edit", new Make());
        }

        public ActionResult Delete(int id)
        {
            using (var repo = new MakesRepository(Server))
            {
                repo.Delete(id);
            }
            return RedirectToAction("List");
        }

    }
}
