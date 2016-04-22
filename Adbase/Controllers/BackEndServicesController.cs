using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sciencecom.DAL;
using Sciencecom.Models;

namespace Sciencecom.Controllers
{
    public class BackEndServicesController : Controller
    {
        private DbWorker _dbw = new DbWorker();
        [HttpPost]
        public void SaveSearch(string collectionMap)
        {
            Session["collectionMap"] = collectionMap;
        }

        public ActionResult EditSurface(int surfaceId)
        {
            List<Surface> result = new List<Surface>();
            Surface surface = new Surface();
            surface  = _dbw.RetrieveSurface(surfaceId);
            ViewBag.EndCountForSurface = 1;
            ViewBag.StartCountForSurface =  0;
            ViewBag.Side = surface.Side.Name;
            result.Add(surface);
            return View("../Data/Surface", result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing){
                
                _dbw.Context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}