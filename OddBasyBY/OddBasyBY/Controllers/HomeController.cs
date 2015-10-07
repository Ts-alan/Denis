﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OddBasyBY.Models;

namespace OddBasyBY.Controllers
{
    public class HomeController : Controller
    {
        OddAdbaseEntity db = new OddAdbaseEntity();
        public ActionResult Index()
        {
            using (OddAdbaseEntity db = new OddAdbaseEntity())
            {
                ViewBag.City = db.City.First();
            }

            return View();
        }

        public ActionResult Table()
        {


            return View();
        }

        public void SaveSuccess(City city, Street street, [ModelBinder(typeof(CustomModelBinderForSegment))] ICollection<Segment> segment, [ModelBinder(typeof(CustomModelBinderForModels))]ICollection<SpecificationofRM> models)
        {
            //if (
            //    db.Street.Any(
            //        a =>
            //            a.BreadthE == street.BreadthE && a.BreadthS == street.BreadthS && a.LengthE == street.LengthE &&
            //            a.LengthS == street.LengthS))
            //{
            //    db.Entry(street).State = EntityState.Modified;
            //}
            //else
            //{


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
                streetInfo.SpecificationofRM = models;

                db.Segment.AddRange(segment);
                db.SpecificationofRM.AddRange(models);
            //}
            db.SaveChanges();


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