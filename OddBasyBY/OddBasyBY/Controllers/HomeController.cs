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
        public ActionResult Index()
        {
            using (OddAdbaseEntity db = new OddAdbaseEntity())
            {
                ViewBag.City = db.City.First().Name;
            }

            return View();
        }

        public ActionResult SaveSuccess()
        {
            return View();
        }

    }
}