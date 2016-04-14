using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Sciencecom.DAL;

namespace Sciencecom.Controllers
{
    using Models;
    using Models.MapJsonModels;

    public class MapController : BaseDataController
    {

        SciencecomEntities _context = new SciencecomEntities();

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
            int? CountSurface, string Backlight, string ContractFinancialManagement, string PassportAdvertising, 
            string startEndDate, string endEndDate, string startStartDate, string endStartDate,
            string house1, string support, int? id = null)
        {
            List<AdvertisingConstructionJsonModel> objectsForJson = new List<AdvertisingConstructionJsonModel>();
            List<Surface> Surfaces = new List<Surface>();
            if (id.HasValue)
            {
                //используется для отображения едининого объекта при прееходе по ссылке "Показать на карте"
                var construction = _context.AdvertisingStructures.Single(a => a.Id_show == id);
                
                if (_context.Owners.FirstOrDefault(a => a.Id == construction.Owner_Id) != null && !string.IsNullOrWhiteSpace(_context.Owners.FirstOrDefault(a => a.Id == construction.Owner_Id).Name))
                {
                    var Owner = _context.Owners.Single(a => a.Id == construction.Owner_Id).Name;
                    construction.OwnerName = Owner;
                }
                
                var enumeratorSide = _context.AdvertisingStructures.Single(a => a.Id_show == id).Sides;

                for (int i = 0; i < enumeratorSide.Count; i++)
                {
                    for (int j = 0;
                        j < _context.AdvertisingStructures.Single(a => a.Id_show == id).Sides.ElementAt(i).Surfaces.Count;
                        j++)
                    {
                        Surfaces.Add(
                            _context.AdvertisingStructures.Single(a => a.Id_show == id)
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
                C_PassportAdvertising = PassportAdvertising
            };
            IEnumerable<AdvertisingStructure> result =
            dataController.SearchAdversing(mc, owner, TypeOfAdvertisingStructure, Locality, CountSize,
                Backlight, startEndDate, endEndDate, startStartDate, endStartDate, AreaConstruction, CountSurface, house1, support).ToList();
            for (int i = 0; i < result.Count(); i++)
            {
                objectsForJson.Add(new AdvertisingConstructionJsonModel(result.ElementAt(i), null));
            }
            return Json(objectsForJson, JsonRequestBehavior.AllowGet);
        }

        public string FindPictures(int id)
        {
            string markup = "";
            AdvertisingStructure mc = _context.AdvertisingStructures.SingleOrDefault(a => a.Id_show == id);

            if (mc == null)
            {
                return markup = "";
            }

            PhotoWorkerController phw = new PhotoWorkerController();
            List<string> photoPathes = new List<string>();
            List<string> docNames = new List<string>
            {
                "photo1",
                "photo2"
            };

            if (mc.Code == "BB" | mc.Code == "LD")
            {
                foreach (string path in phw.LoadPic(mc.Id_show.ToString()))
                {
                    var s = path.Substring(path.IndexOf("Image", StringComparison.Ordinal));
                    photoPathes.Add(s);
                }
            }
            
            for (int i = 0; i < docNames.Count; i++)
            {
                string src = "~/Images/" + docNames[i] + "/" + mc.Id_show + docNames[i] + ".jpg";
                string path = Url.Content(src);
                if (System.IO.File.Exists(path))
                {
                    photoPathes.Add(src);
                }
            }
            if (photoPathes.Count == 0)
            {
                return markup = "";
            }
            //Container for carousel
            markup += "<div class=''>";

            markup += "<div id = 'myCarousel' class='carousel slide carousel-fit' data-ride='carousel'>";
            //Indicators
            markup += "<ol class='carousel-indicators'>";
            markup += "<li data-target='#carousel-example-generic' data-slide-to='0' class='active'></li>";
            for (int i = 0; i < photoPathes.Count-1; i++)
            {
                markup += "<li data-target='#carousel-example-generic' data-slide-to='"+ (i+1) +"></li>";
            }
            markup += "</ol>";
 
            //Wrapper for slides
            markup += "<div class='carousel-inner'>";
            markup += "<img height='150' src = '" + photoPathes[0] + "' alt='...'>";
            //markup += "<div class='carousel-caption'>";
            //markup += "<h3>Caption Text</h3>";
            //markup += "</div>";
            markup += "</div>";
            for (int i = 1; i < photoPathes.Count; i++)
            {
                markup += "<div class='item'><img height='150' src = '" + photoPathes[i] + "' alt = '...' >< div class='carousel-caption'><h3>Caption Text</h3></div></div>";
            }
           
            
            //Controls
            markup += "<a class='left carousel-control' href='#carousel-example-generic' role='button' data-slide='prev'>";
            markup += "<span class='glyphicon glyphicon-chevron-left'></span></a>";
            markup += "<a class='right carousel-control' href='#carousel-example-generic' role='button' data-slide='next'>";
            markup += "<span class='glyphicon glyphicon-chevron-right'></span></a>";
            markup += "</div>"; //carousel-inner
            markup += "</div>"; //first carousel div
            markup += "</div>"; //container div
            return markup;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}