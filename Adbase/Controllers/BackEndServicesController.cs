using System.Web.Mvc;

namespace Sciencecom.Controllers
{
    public class BackEndServicesController : Controller
    {
        
       
        [HttpPost]
        public void SaveSearch(string collectionMap)
        {
            Session["collectionMap"] = collectionMap;
        }
    }
}