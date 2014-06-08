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
        private IMakesRepository makesRepository;
        private IModelsRepository modelsRepository;

        public MakesController(IMakesRepository makesRepository, IModelsRepository modelsRepository)
        {
            this.makesRepository = makesRepository;
            this.modelsRepository = modelsRepository;
        }

        public ActionResult List()
        {
            return View(makesRepository.Get(orderBy: make => make.Name));
        }

        [HttpGet]
        public ActionResult Edit(int makeId)
        {
            var make = makesRepository.GetById(makeId);
            return make == null ? RedirectToAction("List") : (ActionResult) View(make);
        }

        [HttpPost]
        public ActionResult Edit(Make make, HttpPostedFileBase file)
        {
            makesRepository.SaveOrUpdate(make, file);
            return RedirectToAction("Edit", new { makeId = make.Id });
        }

        public ActionResult Add()
        {
            return View("Edit", new Make());
        }

        public ActionResult Delete(int makeId)
        {
            var entity = makesRepository.GetById(makeId);
            entity.Models.ToList().ForEach(m => modelsRepository.Delete(m));
            makesRepository.Delete(entity);
            return RedirectToAction("List");
        }

    }
}
