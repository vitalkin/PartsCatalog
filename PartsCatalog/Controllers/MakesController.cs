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

        public MakesController(IMakesRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult List()
        {
            return View(repository.Get(orderBy: make => make.Name));
        }

        [HttpGet]
        public ActionResult Edit(int makeId)
        {
            var make = repository.GetById(makeId);
            return make == null ? RedirectToAction("List") : (ActionResult) View(make);
        }

        [HttpPost]
        public ActionResult Edit(Make make, HttpPostedFileBase file)
        {
            repository.SaveOrUpdate(make, file);
            return RedirectToAction("Edit", new { makeId = make.Id });
        }

        public ActionResult Add()
        {
            return View("Edit", new Make());
        }

        public ActionResult Delete(int makeId)
        {
            repository.Delete(makeId);
            return RedirectToAction("List");
        }

    }
}
