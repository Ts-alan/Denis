using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Enums.Selectors;

namespace Sciencecom.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            Session["collectionMap"] = null;
            if (RolesList.IsInRole(RolesEnum.OnlyBillboards, User))
            {
                return RedirectToAction("BillboardsIndex");
            }
            return View();
        }

        [Authorize]
        public ActionResult BillboardsIndex()
        {
            if (!RolesList.IsInRole(RolesEnum.OnlyBillboards, User))
            {
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}