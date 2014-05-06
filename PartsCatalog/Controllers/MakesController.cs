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
        private IMakesRepository repository;

        private IImageManager imageManger;

        public MakesController(IMakesRepository repository, IImageManager imageManger)
        {
            this.repository = repository;
            this.imageManger = imageManger;
        }

        public ActionResult List()
        {
            return View(repository.Get(orderBy: make => make.Name));
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var make = repository.GetById(id);
            return make == null ? RedirectToAction("List") : (ActionResult) View(make);
        }

        [HttpPost]
        public ActionResult Edit(Make make, HttpPostedFileBase file)
        {
            repository.SaveOrUpdate(make, file);
            return RedirectToAction("Edit", new { id = make.Id });
        }

        public ActionResult Add()
        {
            return View("Edit", new Make());
        }

        public ActionResult Delete(int id)
        {
            repository.Delete(id);
            return RedirectToAction("List");
        }

    }
}
