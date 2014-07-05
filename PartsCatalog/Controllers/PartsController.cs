using PartsCatalog.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PartsCatalog.Controllers
{
    public class PartsController : Controller
    {
        private IPartsRepository partsRepository;

        public PartsController(IPartsRepository partsRepository)
        {
            this.partsRepository = partsRepository;
        }

        public ActionResult List(int? categoryId)
        {
            return View(partsRepository.Get());
        }
    }
}
