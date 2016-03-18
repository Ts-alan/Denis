using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Sciencecom.Controllers
{
    using Models;
    using Models.MapJsonModels;

    public class MapController : Controller
    {
        

        [Authorize]
        public ActionResult Index(string type = null, int? id = null)
        {
            Session["action"] = RouteData.Values["action"];
            Session["controller"] = RouteData.Values["controller"];
            ViewBag.Type = type;
                ViewBag.Id = id;
       
            return View();
        }
     
        
        public JsonResult GetDesign(string owner, string UniqueNumber, string TypeOfAdvertisingStructure, string Locality,
            string Street1, string House1, int? CountSize, int? AreaConstruction,
            int? CountSurface, string Backlight, string ContractFinancialManagement, string PassportAdvertising,  string EndDate, int? id = null)
        {


            SciencecomEntities context = new SciencecomEntities();
            List<AdvertisingConstructionJsonModel> objectsForJson = new List<AdvertisingConstructionJsonModel>();
            List<Surface> Surfaces = new List<Surface>();
            if (id.HasValue)
            {
                //используется для отображения едининого объекта при прееходе по ссылке "Показать на карте"

                var construction = context.AdvertisingStructures.Single(a => a.Id_show == id);
                
                if (context.Owners.FirstOrDefault(a => a.Id == construction.Owner_Id) != null && !string.IsNullOrWhiteSpace(context.Owners.FirstOrDefault(a => a.Id == construction.Owner_Id).Name))
                {
                    var Owner = context.Owners.Single(a => a.Id == construction.Owner_Id).Name;
                    construction.OwnerName = Owner;
                }
                
                var enumeratorSide = context.AdvertisingStructures.Single(a => a.Id_show == id).Sides;

                for (int i = 0; i < enumeratorSide.Count; i++)
                {
                    for (int j = 0;
                        j < context.AdvertisingStructures.Single(a => a.Id_show == id).Sides.ElementAt(i).Surfaces.Count;
                        j++)
                    {
                        Surfaces.Add(
                            context.AdvertisingStructures.Single(a => a.Id_show == id)
                                .Sides.ElementAt(i)
                                .Surfaces.ElementAt(j));
                    }
                }
                if (construction != null)
                {
                    objectsForJson.Add(new AdvertisingConstructionJsonModel(construction, Surfaces));
                }

                return Json(objectsForJson, JsonRequestBehavior.AllowGet);
            }
            DataController dataController = new DataController();
          

            AdvertisingStructure mc = new AdvertisingStructure()
            {
                Street1 = Street1,
                UniqueNumber = UniqueNumber,
                House1 =House1,
                C_ContractFinancialManagement = ContractFinancialManagement,
                C_PassportAdvertising = PassportAdvertising,

              
            };
            IEnumerable<AdvertisingStructure> result =
            dataController.SearchAdversing(mc, owner, TypeOfAdvertisingStructure, Locality, CountSize,
                Backlight, EndDate, AreaConstruction, CountSurface).ToList();
            for (int i = 0; i < result.Count(); i++)
            {
                objectsForJson.Add(new AdvertisingConstructionJsonModel(result.ElementAt(i), null));
            }
   


            return Json(objectsForJson, JsonRequestBehavior.AllowGet);
        }
 

 
    }
}