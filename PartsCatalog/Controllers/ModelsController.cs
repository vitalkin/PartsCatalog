using PartsCatalog.DAL;
using PartsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartsCatalog.Controllers
{
    public class ModelsController : Controller
    {
        private IModelsRepository modelsRepository;
        private IMakesRepository makesRepository;

        public ModelsController(IModelsRepository modelsRepository, IMakesRepository makesRepository)
        {
            this.modelsRepository = modelsRepository;
            this.makesRepository = makesRepository;
        }

        public ActionResult List(int? makeId)
        {
            ViewBag.MakeId = makeId;
            return View(modelsRepository.GetByMakeId(makeId.Value));
        }

        [HttpGet]
        public ActionResult Edit(int makeId, int modelId)
        {
            var model = modelsRepository.GetById(modelId);
            return model == null ? RedirectToAction("List", new { makeId = makeId }) : (ActionResult)View(model);
        }

        [HttpPost]
        public ActionResult Edit(Model model, IEnumerable<HttpPostedFileBase> files)
        {
            model.Make = makesRepository.GetById(model.Make.Id);
            modelsRepository.SaveOrUpdate(model, files);
            return RedirectToAction("Edit", new { makeId = model.Make.Id, modelId = model.Id });
        }

        public ActionResult Add(int? makeId)
        {
            var make = makesRepository.GetById(makeId.Value);
            return make == null ? RedirectToAction("List", "Makes") : (ActionResult)View("Edit", new Model() { Make = make });
        }

        public ActionResult Delete(int makeId, int modelId)
        {
            modelsRepository.Delete(modelId);
            return RedirectToAction("List", new { makeId = makeId });
        }
                
    }
}
