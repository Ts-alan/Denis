using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OddBasyBY.Models;

namespace OddBasyBY.Controllers
{
    public class HomeController : Controller
    {
        OddAdbaseEntity db=new OddAdbaseEntity();
        public ActionResult Index()
        {
            using (OddAdbaseEntity db = new OddAdbaseEntity())
            {
                ViewBag.City = db.City.First().Name;
            }

            return View();
        }

        public ActionResult SaveSuccess(City city, Street street , [ModelBinder(typeof(CustomModelBinderForSegment))] List<Segment> segment)
        {
            db.City.Add(new City() {Name = city.Name});
            
            db.Street.Add(new Street() {});
            return RedirectToAction("Index");
        }

    }
}