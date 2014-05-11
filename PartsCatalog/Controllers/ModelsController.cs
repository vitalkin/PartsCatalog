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
        private IGenericRepository<Model> repository;

        public ModelsController(IGenericRepository<Model> repository)
        {
            this.repository = repository;
        }

        public ActionResult List(int? makeId)
        {
            return View(repository.Get(model => model.Make.Id == makeId));
        }

    }
}
