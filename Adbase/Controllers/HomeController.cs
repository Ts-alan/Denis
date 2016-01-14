using System.Web.Mvc;
using Enums.Selectors;

namespace Sciencecom.Controllers
{

    public class HomeController : Controller
    {
  
        public ActionResult Index()
        {
            Session["collectionMap"] = null;
            if (RolesList.IsInRole(RolesEnum.OnlyBillboards, User))
            {
                return RedirectToAction("BillboardsIndex");
            }
            return View();
        }

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