using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
                ViewBag.City = db.City.First();
            }

            return View();
        }

        public void SaveSuccess(City city, Street street , [ModelBinder(typeof(CustomModelBinderForSegment))] ICollection<Segment> segment)
        {
            var streetInfo = new Street()
            {
                Name = street.Name,
                BreadthS = street.BreadthS,
                BreadthE = street.BreadthE,
                LengthE = street.LengthE,
                LengthS = street.LengthS,
                City_id = city.id
            };
            db.Street.Add(streetInfo);

            streetInfo.Segment = segment;
            db.Segment.Add(segment.First());
            db.SaveChanges();

            //return RedirectToAction("Index");
        }
        public ActionResult FindStreets(string term)
        {
            var streets = from m in db.IntelliSenseStreet where m.Street.Contains(term) select m;
            var projection = from street in streets
                             select new
                             {
                                 id = street.id,
                                 label = street.Street + " " + street.Type,
                                 value = street.Street + " " + street.Type
                             };
            return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
        }

    }
}