using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sciencecom.Models.MapJsonModels;

namespace Sciencecom.Controllers
{
    using Models;
    using DAL;
    using System.Net;
    public class DataController : BaseDataController
    {
        private DbWorker _dbw = new DbWorker();
        private PhotoWorkerController _phw = new PhotoWorkerController();
        private SciencecomEntities _context = new SciencecomEntities();

        [Authorize]
        public ActionResult Owner(string name, string backTo)
        {
            var owner = _dbw.Owner(name);
            ViewBag.BackRef = backTo;
            return View(owner);
        }
        
        public IEnumerable<AdvertisingStructure> SearchAdversing(AdvertisingStructure advertisin, string owner,
            string typeOfAdvertisingStructure, string locality, int? countSize, string backlight, string startEndDate, 
            string endEndDate, string startStartDate, string endStartDate, int? areaConstruction,int? CountSurface,
            string house1, string support)
        {
            var result = _dbw.SearchAdversing( advertisin, owner,typeOfAdvertisingStructure, locality, countSize, backlight,
                startEndDate, endEndDate, startStartDate, endStartDate, areaConstruction, CountSurface, house1, support);
            return result;
        }

        public ActionResult FindStreets(string term, string cityname)
        {
            cityname.Normalize();
            //var streets = _dbw.FindStreets(term);
            var projection = from street in _dbw.FindStreets(term)
                             where street.Locality.NameLocality == cityname
                             select new
                             {
                                 id = street.id,
                                 label = street.Street1 + " " + street.Type,
                                 value = street.Street1 + " " + street.Type

                             };
            return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
        }
      
        
        [Authorize]
        [HttpGet]
        public ActionResult AdvertisingDesign()
        {
            Session["action"] = RouteData.Values["action"];
            Session["controller"] = RouteData.Values["controller"];
            return View();
        }

        public JsonResult SearchAdvertisingDesign(int page, string sidx, string sord,
            int rows, string Собственник, string Вид_конструкции, string Населенный_пункт,
            string Улица, string Со_стороны, string Ближайшая_по_ходу, string Дом, string Номер_опоры, string Количество_сторон,
            string Количестов_поверхностей, string Площадь_конструкции, string Разреш_по, string НачалоРазреш_по,
            string КонецРазреш_по, string Разреш_с, string НачалоРазреш_с, string КонецРазреш_с)

        {
            JSONTableData jd = new JSONTableData();
            jd = _dbw.SearchAdvertisingDesign(page, sidx, sord,
            rows, Собственник, Вид_конструкции, Населенный_пункт,
            Улица, Со_стороны, Ближайшая_по_ходу, Дом, Номер_опоры, Количество_сторон,
            Количестов_поверхностей, Площадь_конструкции, Разреш_по, НачалоРазреш_по,
            КонецРазреш_по, Разреш_с, НачалоРазреш_с, КонецРазреш_с);
            return Json(jd, JsonRequestBehavior.AllowGet);
        }



        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult DeleteAdvertisingDesign(int? id,string switchtoMap)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }
            AdvertisingStructure mc = _dbw.RetrieveStructure(id);
            if (mc==null)
            {
                return RedirectToAction("NotFound");
            }
            
            //удаление картинок

            DeletePic(mc.Id_show.ToString(), "Scan1SidesWithFinancialManagement");

            DeletePic(mc.Id_show.ToString(), "ScanPassport_1Sides");

            DeletePic(mc.Id_show.ToString(), "ScanPassport_2Sides");

            DeletePic(mc.Id_show.ToString(), "Scan1Side");

            DeletePic(mc.Id_show.ToString(), "Scan2Side");

            DeletePic(mc.Id_show.ToString(), "Application");

            if (mc.Code == "BB" | mc.Code=="LD")
            {
                foreach (string name in _phw.LoadPic(mc.Id_show.ToString()))
                {
                    _phw.DeleteSurfacePhotos(name);
                }
            }

            _dbw.DeleteAdvertisingDesign(id);

            if (switchtoMap == "true")
            {
                return RedirectToAction("Index", "Map");
            }
            return RedirectToAction((string)Session["action"], (string)Session["controller"]);
        }

        public ActionResult AddSurface(string side, int? startCountForSurface, int? endCountForSurface, bool isEdit = false)
        {
            
            ViewBag.EndCountForSurface = endCountForSurface ?? 1 ;
            ViewBag.StartCountForSurface = startCountForSurface ?? 0;
            ViewBag.Side = side;
            ViewBag.Guid = Guid.NewGuid().GetHashCode();
            return View("Surface");
        }

        public ActionResult EditSurface(string side, int? startCountForSurface, int? endCountForSurface)
        {
            ViewBag.EndCountForSurface = endCountForSurface ?? 1;
            ViewBag.StartCountForSurface = startCountForSurface ?? 0;
            ViewBag.Side = side;
            var tempRresult = (List<Side>)Session["sides"];
            var result = tempRresult[int.Parse(side) - 1].Surfaces.ToList();
            return View("Surface", result);
        }

        #region Щиты

        public ActionResult Documents(int? id, string type = "BB")
        {
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }
            var data = _dbw.RetrieveStructure(id);
            if (data == null || data.Code != "BB")
            {
                return RedirectToAction("NotFound");
            }
            List<Surface> surfaces = new List<Surface>();
            foreach (var sides in data.Sides.OrderBy(a => a.Name))
            {
                foreach (var surface in sides.Surfaces)
                {
                    surfaces.Add(surface);
                }
            }
            
            Session["sides"] = data.Sides.OrderBy(side => int.Parse(side.Name)).ToList();
            ViewBag.Type = "BB";
            ViewBag.Id = data.Id_show;

            ViewBag.Scan1Sides = LoadPic(data.Id_show.ToString(), "Scan1SidesWithFinancialManagement");
            ViewBag.ScanPassport_1 = LoadPic(data.Id_show.ToString(), "ScanPassport_1Sides");
            ViewBag.ScanPassport_2 = LoadPic(data.Id_show.ToString(), "ScanPassport_2Sides");
            List<string> photoNames = _phw.LoadPic(data.Id_show.ToString());

            Session["PhotoNames"] = photoNames;
            Session["IdShow"] = data.Id_show;
            
            return View(data);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateAdvertisingDesign()
        {
            ViewBag.Code = "BB";
            Session["PhotoNames"] = null;
            ViewBag.UniqueNumber = TableAdapterExtensions.StringSymvol();
            return View();
        }


        [Authorize]
        [HttpPost]
        public ActionResult CreateAdvertisingDesign(AdvertisingStructure structures,
            [ModelBinder(typeof(CustomModelBinderForSideForAD))] List<Side> sides,
            [ModelBinder(typeof(CustomModelBinderForPicsForAD))] Dictionary<string, HttpPostedFileBase> photos,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase ScanPassport_1Sides, HttpPostedFileBase ScanPassport_2Sides,
            string Bcoord, string Hcoord,
            int countSize = 0)
        {
            
            Guid structuresId = Guid.NewGuid();
            structures.Id = structuresId;
            structures = ValidateCoords(structures, Bcoord, Hcoord);

            _dbw.CreateAdvertisingDesign(structures, sides, surfaces, countSize);

            SavePic(structures.Id_show.ToString(), "ScanPassport_1Sides", ScanPassport_1Sides);

            SavePic(structures.Id_show.ToString(), "ScanPassport_2Sides", ScanPassport_2Sides);

            foreach (var photo in photos)
            {
                _phw.SavePic(structures.Id_show.ToString(), photo.Key + "photo", photo.Value);
            }

            return RedirectToAction("AdvertisingDesign");
        }

        
        [HttpGet]
        public ActionResult EditAdvertisingDesign(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }

            AdvertisingStructure mc = _dbw.RetrieveStructure(id);
            if (mc == null)
            {
                return RedirectToAction("NotFound");
            }
            List<Surface> surfaces = new List<Surface>();
            if (mc.Sides.Count != 0)
            {
                foreach (var side in mc.Sides.OrderBy(a => a.Name))
                {
                    foreach (var surface in side.Surfaces)
                    {
                        surfaces.Add(surface);
                    }
                }
                if (surfaces.Count == 0)
                {
                    surfaces.Add(new Surface() { });
                }
                Session["sides"] = mc.Sides.OrderBy(side => int.Parse(side.Name)).ToList();
            }
            
            ViewBag.Bcoord = mc.coordB;
            ViewBag.Hcoord = mc.coordH;
            int idShow = mc.Id_show;
            ViewBag.Id = idShow;
            ViewBag.photo1 = LoadPic(mc.Id_show.ToString(), "ScanPassport_1Sides");
            ViewBag.photo2 = LoadPic(mc.Id_show.ToString(), "ScanPassport_2Sides");

            List<string> photoNames = _phw.LoadPic(idShow.ToString());
            Session["PhotoNames"] = photoNames;
            return View(mc);
        }

        [HttpPost]
        public ActionResult EditAdvertisingDesign(int id, AdvertisingStructure structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            [ModelBinder(typeof(CustomModelBinderForPicsForAD))] Dictionary<string, HttpPostedFileBase> photos,
            [ModelBinder(typeof(CustomModelBinderForPicInd))] Dictionary<string, string> picIndexes,
            HttpPostedFileBase ScanPassport_1Sides, HttpPostedFileBase ScanPassport_2Sides,
            HttpPostedFileBase scan1SidesWithFinancialManagement, 
            string Bcoord, string Hcoord, string photoInd1, string photoInd2,
            int countSize = 0)
        {
            if (structures.UniqueNumber == null)
            {
                structures.UniqueNumber = TableAdapterExtensions.StringSymvol();
            }
            if (structures.Code == null)
            {
                structures.Code = "BB";
            }
            structures = ValidateCoords(structures, Bcoord, Hcoord);
            AdvertisingStructure mc = _dbw.RetrieveStructure(id);
            var tempId = mc.Id;

            foreach (var side in mc.Sides)
            {
                _context.Surfaces.RemoveRange(from s in mc.Sides from sur in s.Surfaces select _context.Surfaces.FirstOrDefault(si => si.Id == sur.Id));
            }

            _context.Sides.RemoveRange(mc.Sides.Select(side => _context.Sides.FirstOrDefault(x => x.Id == side.Id)));
            _context.AdvertisingStructures.Remove(_context.AdvertisingStructures.FirstOrDefault(x => x.Id_show == mc.Id_show));
            _context.SaveChanges();
            if (countSize > 0)
            {
                for (int j = 0; j < countSize; j++)
                {
                    try
                    {
                        sides[j].AdvertisingStructures_Id = mc.Id;
                    }
                    catch (Exception e)
                    {
                        sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
                    }
                    
                    sides[j].AdvertisingStructures_Id = mc.Id;
                    sides[j].Name = (j + 1).ToString();
                    sides[j].Id = Guid.NewGuid();
                }
                structures.Id = tempId;
                structures.Area = CountSquare(surfaces);
                
                _context.AdvertisingStructures.Add(structures);
                
                _context.Sides.AddRange(sides);
                
                _context.SaveChanges();
                
                List<Surface> listSurface = new List<Surface>();
                foreach (var i in surfaces)
                {
                    i.Side_Id = sides.Single(a => a.Name == i.SideOfSurface).Id;
                    listSurface.Add(i);
                }
                _context.Surfaces.AddRange(listSurface);
                _context.SaveChanges();
            }
            else
            {
                structures.Area = CountSquare(surfaces);
                _context.AdvertisingStructures.Add(structures);
                _context.SaveChanges();
            }

            //картики
            ValidatePic(ScanPassport_1Sides, photoInd1, structures.Id_show.ToString(), mc.Id_show.ToString(), "ScanPassport_1Sides");
            ValidatePic(ScanPassport_2Sides, photoInd2, structures.Id_show.ToString(), mc.Id_show.ToString(), "ScanPassport_2Sides");
            
            _phw.ValidatePic(photos, picIndexes, structures.Id_show.ToString(), mc.Id_show.ToString());
            
            return RedirectToAction((string)Session["action"], (string)Session["controller"]);
        }

        #endregion

        #region Металлические указатели

        public ActionResult Pointer(int? id, string type = "MP")
        {
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }
            AdvertisingStructure data = new AdvertisingStructure();
            
            data = _dbw.RetrieveStructure(id);

            if (data == null || data.Code != "MP")
            {
                return RedirectToAction("NotFound");
            }
            
            List<Surface> surfaces = new List<Surface>();
            foreach (var sides in data.Sides.OrderBy(a => a.Name))
            {
                foreach (var surface in sides.Surfaces)
                {
                    surfaces.Add(surface);
                }
            }
            Session["sides"] = data.Sides.OrderBy(side => int.Parse(side.Name)).ToList();

            ViewBag.Type = "MP";
            ViewBag.Id = data.Id_show;
            ViewBag.Scan1Side = LoadPic(data.Id_show.ToString(), "Scan1Side");
            ViewBag.Scan2Side = LoadPic(data.Id_show.ToString(), "Scan2Side");
            ViewBag.photo1 = LoadPic(data.Id_show.ToString(), "photo1");
            ViewBag.photo2 = LoadPic(data.Id_show.ToString(), "photo2");

            
            if (data.coordB != null)
            {
                data.coordB = double.Parse(data.coordB.ToString().Substring(0, 7));
            }
            if (data.coordH != null)
            {
                data.coordH = double.Parse(data.coordH.ToString().Substring(0, 7));
            }
            return View(data);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateMetalPointerDesign()
        {
            ViewBag.Code = "MP";
            ViewBag.UniqueNumber = TableAdapterExtensions.StringSymvol();
            ViewBag.SeizesCount = 1;
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public ActionResult CreateMetalPointerDesign(AdvertisingStructure structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase Scan1Side, HttpPostedFileBase Scan2Side, HttpPostedFileBase photo1,
            HttpPostedFileBase scan1SidesWithFinancialManagement, List<HttpPostedFileBase> severalPhoto,
            string Bcoord, string Hcoord,
            int countSize = 0)
        {
            if (structures.Code == null)
            {
                structures.Code = "MP";
            }
            if (sides.Count == 0)
            {
                sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
            }
            Guid structuresId = Guid.NewGuid();
            structures.Id = structuresId;
            structures = ValidateCoords(structures, Bcoord, Hcoord);
            _dbw.CreateAdvertisingDesign(structures, sides, surfaces, countSize);
            
            SavePic(structures.Id_show.ToString(), "Scan1SidesWithFinancialManagement", scan1SidesWithFinancialManagement);

            SavePic(structures.Id_show.ToString(), "Scan1Side", Scan1Side);

            SavePic(structures.Id_show.ToString(), "Scan2Side", Scan2Side);

            SavePic(structures.Id_show.ToString(), "Scan1Side", Scan1Side);

            SavePic(structures.Id_show.ToString(), "Scan2Side", Scan2Side);

            SavePic(structures.Id_show.ToString(), "Scan2Side", Scan2Side);

            SavePic(structures.Id_show.ToString(), "photo1", photo1);

            return RedirectToAction("AdvertisingDesign");

        }

        [HttpGet]
        public ActionResult EditMetalPointerDesign(int? id)
        {
            
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }

            AdvertisingStructure mc = _dbw.RetrieveStructure(id);
            if (mc == null)
            {
                return HttpNotFound();
            }
            List<Surface> surfaces = new List<Surface>();
            foreach (var sides in mc.Sides.OrderBy(a => a.Name))
            {
                foreach (var surface in sides.Surfaces)
                {
                    surfaces.Add(surface);
                }
            }
            Session["sides"] = mc.Sides.OrderBy(side => int.Parse(side.Name)).ToList();
            mc.Code = "MP";
            if(mc.coordB!=null)
            mc.coordB = double.Parse(mc.coordB.ToString());
            if(mc.coordH!=null)
            mc.coordH = double.Parse(mc.coordH.ToString());
            ViewBag.Id = mc.Id_show;
            ViewBag.Scan1Side = LoadPic(mc.Id_show.ToString(), "Scan1Side");
            ViewBag.Scan2Side = LoadPic(mc.Id_show.ToString(), "Scan2Side");
            ViewBag.photo1 = LoadPic(mc.Id_show.ToString(), "photo1");
            ViewBag.Bcoord = mc.coordB;
            ViewBag.Hcoord = mc.coordH;
            return View(mc);
        }

        [HttpPost]
        public ActionResult EditMetalPointerDesign(int id, AdvertisingStructure structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase Scan1Side, HttpPostedFileBase Scan2Side, HttpPostedFileBase photo1,
            HttpPostedFileBase scan1SidesWithFinancialManagement, string Scan1SideInd, string Scan2SideInd, string PhotoInd1,
            string Bcoord, string Hcoord,
            int countSize = 0)
        {

            AdvertisingStructure mc = _dbw.RetrieveStructure(id);
            var tempId = mc.Id;
            DeletePic(mc.Id_show.ToString(), "scanPassport1Sides");
            DeletePic(mc.Id_show.ToString(), "scanPassport2Sides");
            foreach (var side in mc.Sides)
            {
                _context.Surfaces.RemoveRange(from s in mc.Sides from sur in s.Surfaces select _context.Surfaces.FirstOrDefault(si => si.Id == sur.Id));
            }

            _context.Sides.RemoveRange(mc.Sides.Select(side => _context.Sides.FirstOrDefault(x => x.Id == side.Id)));
            _context.AdvertisingStructures.Remove(_context.AdvertisingStructures.FirstOrDefault(x => x.Id_show == mc.Id_show));
            _context.SaveChanges();
            if (structures.Code == null)
            {
                structures.Code = "MP";
            }
            if (sides.Count == 0)
            {
                sides.Add(new Side() {DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
            }

            for (int j = 0; j < 1; j++)
            {
               
                sides[j].AdvertisingStructures_Id = mc.Id;
                sides[j].Name = (j + 1).ToString();
                sides[j].Id = Guid.NewGuid();
            }
            structures.Id = tempId;
            structures.Area = CountSquare(surfaces);
            structures = ValidateCoords(structures, Bcoord, Hcoord);

            _context.AdvertisingStructures.Add(structures);
            _context.Sides.AddRange(sides);
            
            List<Surface> listSurface = new List<Surface>();
            foreach (var i in surfaces)
            {
                i.Side_Id = sides.Single(a => a.Name == i.SideOfSurface).Id;
                listSurface.Add(i);
            }
            _context.Surfaces.AddRange(listSurface);

           

            _context.SaveChanges();

            //сохранение картинок

            ValidatePic(Scan1Side, Scan1SideInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "Scan1Side");
            ValidatePic(Scan2Side, Scan2SideInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "Scan2Side");
            ValidatePic(photo1, PhotoInd1, structures.Id_show.ToString(), mc.Id_show.ToString(), "photo1");
           
            return RedirectToAction((string)Session["action"], (string)Session["controller"]);

        }

        #endregion

        #region Световые короба

        public ActionResult LightDuct(int? id, string type = "LD")
        {
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }
            var data = _dbw.RetrieveStructure(id);
            if (data == null || data.Code != "LD")
            {
                return RedirectToAction("NotFound");
            }
            List<Surface> surfaces = new List<Surface>();
            foreach (var sides in data.Sides.OrderBy(a => a.Name))
            {
                foreach (var surface in sides.Surfaces)
                {
                    surfaces.Add(surface);
                }
            }
            Session["sides"] = data.Sides.OrderBy(side => int.Parse(side.Name)).ToList();

            ViewBag.Type = "LD";
            ViewBag.Id = data.Id_show;
            ViewBag.Scan1Sides = LoadPic(data.Id_show.ToString(), "Scan1SidesWithFinancialManagement");
            ViewBag.ScanPassport_1 = LoadPic(data.Id_show.ToString(), "ScanPassport_1Sides");
            ViewBag.ScanPassport_2 = LoadPic(data.Id_show.ToString(), "ScanPassport_2Sides");
            ViewBag.Scan1Side = LoadPic(data.Id_show.ToString(), "Scan1Side");
            ViewBag.Scan2Side = LoadPic(data.Id_show.ToString(), "Scan2Side");
            ViewBag.photo1 = LoadPic(data.Id_show.ToString(), "photo1");
            ViewBag.photo2 = LoadPic(data.Id_show.ToString(), "photo2");
            ViewBag.Application = LoadPic(data.Id_show.ToString(), "Application");
            if (data.coordB != null)
            {
                data.coordB = double.Parse(data.coordB.ToString().Substring(0, 7));
            }
            if (data.coordH != null) {
                data.coordH = double.Parse(data.coordH.ToString().Substring(0, 7));
            }
            return View(data);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateLightDuctDesign()
        {
            ViewBag.Code = "LD";
            ViewBag.UniqueNumber = TableAdapterExtensions.StringSymvol();
            ViewBag.SeizesCount = 2;
            Session["PhotoNames"] = null;

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateLightDuctDesign(AdvertisingStructure structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase scanPassport1Sides, HttpPostedFileBase scanPassport2Sides,
            [ModelBinder(typeof(CustomModelBinderForPicsForAD))] Dictionary<string, HttpPostedFileBase> photos,
            HttpPostedFileBase Application,
            HttpPostedFileBase scan1Side, HttpPostedFileBase scan2Side,
            string Bcoord, string Hcoord, int countSize = 0)
        {
            
            Guid structuresId = Guid.NewGuid();
            structures.Id = structuresId;
            structures = ValidateCoords(structures, Bcoord, Hcoord);
            
            _dbw.CreateAdvertisingDesign(structures, sides, surfaces, countSize);
            string id = structures.Id_show.ToString();
            SavePic(id, "ScanPassport_1Sides", scanPassport1Sides);

            SavePic(id, "ScanPassport_2Sides", scanPassport2Sides);

            SavePic(id, "Scan1Side", scan1Side);

            SavePic(id, "Scan2Side", scan2Side);

            SavePic(id, "Application", Application);

            foreach (var photo in photos)
            {
                _phw.SavePic(id, photo.Key + "photo", photo.Value);
            }

            return RedirectToAction("AdvertisingDesign");
        }

        [HttpGet]
        public ActionResult EditLightDuctDesign(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }

            AdvertisingStructure mc = _dbw.RetrieveStructure(id);
            if (mc == null)
            {
                return RedirectToAction("NotFound");
            }
            List<Surface> surfaces = mc.Sides.OrderBy(a => a.Name).SelectMany(sides => sides.Surfaces).ToList();
            Session["sides"] = mc.Sides.OrderBy(side => int.Parse(side.Name)).ToList();
            mc.Code = "LD";
            if (mc.coordB != null)
            {
                mc.coordB = double.Parse(mc.coordB.ToString());
            }
            if (mc.coordH != null)
            {
                mc.coordH = double.Parse(mc.coordH.ToString());
            }
            int idShow = mc.Id_show;
            ViewBag.Id = idShow;
            ViewBag.Scan1Side = LoadPic(idShow.ToString(), "Scan1Side");
            ViewBag.Scan2Side = LoadPic(idShow.ToString(), "Scan2Side");
            ViewBag.ScanPassport_1Sides = LoadPic(idShow.ToString(), "ScanPassport_1Sides");
            ViewBag.ScanPassport_2Sides = LoadPic(idShow.ToString(), "ScanPassport_2Sides");
           
            List<string> photoNames = _phw.LoadPic(idShow.ToString());
            if (mc.Sides != null)
            {
                Session["sides"] = mc.Sides.OrderBy(side => int.Parse(side.Name)).ToList();
            }
           
            Session["PhotoNames"] = photoNames;
            ViewBag.Application = LoadPic(mc.Id_show.ToString(), "Application");
            ViewBag.Bcoord = mc.coordB;
            ViewBag.Hcoord = mc.coordH;
            return View(mc);
        }

        [HttpPost]
        public ActionResult EditLightDuctDesign(int id, AdvertisingStructure structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            [ModelBinder(typeof(CustomModelBinderForPicsForAD))] Dictionary<string, HttpPostedFileBase> photos,
            [ModelBinder(typeof(CustomModelBinderForPicInd))] Dictionary<string, string> picIndexes,
            HttpPostedFileBase scanPassport1Sides, HttpPostedFileBase scanPassport2Sides, HttpPostedFileBase Application,
            HttpPostedFileBase scan1Side, HttpPostedFileBase scan2Side, 
            string scanPassport1SidesInd, string scanPassport2SidesInd, string scan1SideInd, string scan2SideInd, 
            string photo1Ind, string photo2Ind, string ApplicationInd, 
            string Bcoord, string Hcoord, int countSize = 0)
        {

            AdvertisingStructure mc = _dbw.RetrieveStructure(id);
            var tempId = mc.Id;
            if (structures.Code == null)
            {
                structures.Code = "LD";
            }
            
            foreach (var side in mc.Sides)
            {
                _context.Surfaces.RemoveRange(from s in mc.Sides from sur in s.Surfaces select _context.Surfaces.FirstOrDefault(si => si.Id == sur.Id));
            }
            
            _context.Sides.RemoveRange(mc.Sides.Select(side => _context.Sides.FirstOrDefault(x => x.Id == side.Id)));
            _context.AdvertisingStructures.Remove(_context.AdvertisingStructures.FirstOrDefault(x=>x.Id_show == mc.Id_show));
            _context.SaveChanges();

            for (int j = 0; j < countSize; j++)
            {
                try
                {
                    sides[j].AdvertisingStructures_Id = mc.Id;
                }
                catch (System.IndexOutOfRangeException e)
                {
                    sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
                }
                sides[j].AdvertisingStructures_Id = mc.Id;
                sides[j].Name = (j + 1).ToString();
                sides[j].Id = Guid.NewGuid();
            }
            structures.Id = tempId;
            structures.Area = CountSquare(surfaces);
             structures = ValidateCoords(structures, Bcoord, Hcoord);
            _context.AdvertisingStructures.Add(structures);
            _context.Sides.AddRange(sides);

            List<Surface> listSurface = new List<Surface>();
            foreach (var i in surfaces)
            {
                i.Side_Id = sides.Single(a => a.Name == i.SideOfSurface).Id;
                listSurface.Add(i);
            }
            _context.Surfaces.AddRange(listSurface);

            _context.SaveChanges();

            //картики
            ValidatePic(scanPassport1Sides, scanPassport1SidesInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "ScanPassport_1Sides");
            ValidatePic(scanPassport2Sides, scanPassport2SidesInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "ScanPassport_2Sides");
            ValidatePic(scan1Side, scan1SideInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "Scan1Side");
            ValidatePic(scan2Side, scan2SideInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "Scan2Side");
            ValidatePic(Application, ApplicationInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "Application");

            _phw.ValidatePic(photos, picIndexes, structures.Id_show.ToString(), mc.Id_show.ToString());
       
            return RedirectToAction((string)Session["action"], (string)Session["controller"]);

        }

        #endregion

        #region Неопознанные конструкции

        public ActionResult Illegal(int? id, string type = "UI")
        {

            if (id == null)
            {
                return RedirectToAction("NotFound");
            }
            AdvertisingStructure data = _dbw.RetrieveStructure(id);

            if (data == null || data.Code != "UI")
            {
                return RedirectToAction("NotFound");
            }
            List<Surface> surfaces = new List<Surface>();
            foreach (var sides in data.Sides.OrderBy(a => a.Name))
            {
                foreach (var surface in sides.Surfaces)
                {
                    surfaces.Add(surface);
                }
            }
            Session["sides"] = data.Sides.OrderBy(side => int.Parse(side.Name)).ToList();

            ViewBag.Type = "UI";
            ViewBag.Id = data.Id_show;
            ViewBag.photo1 = LoadPic(data.Id_show.ToString(), "photo1");
            ViewBag.photo2 = LoadPic(data.Id_show.ToString(), "photo2");

            return View(data);
        }

        [HttpGet]
        public ActionResult EditIllegalDesign(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("NotFound");
            }

            AdvertisingStructure mc = _dbw.RetrieveStructure(id);
            if (mc == null)
            {
                return RedirectToAction("NotFound");
            }
            
            List<Surface> surfaces = new List<Surface>();
            foreach (var sides in mc.Sides.OrderBy(a => a.Name))
            {
                foreach (var surface in sides.Surfaces)
                {
                    surfaces.Add(surface);
                }
            }
            Session["sides"] = mc.Sides.OrderBy(side => int.Parse(side.Name)).ToList();
            mc.Code = "UI";
            
            ViewBag.Type = "UI";
            ViewBag.Id = mc.Id_show;

            ViewBag.photo1 = LoadPic(mc.Id_show.ToString(), "photo1");
            ViewBag.photo2 = LoadPic(mc.Id_show.ToString(), "photo2");

            ViewBag.Bcoord = mc.coordB;
            ViewBag.Hcoord = mc.coordH;

            return View(mc);
        }

        [HttpPost]
        public ActionResult EditIllegalDesign(int id, AdvertisingStructure structures,
            HttpPostedFileBase photo1, HttpPostedFileBase photo2,string photoInd1, string photoInd2, 
            string Bcoord, string Hcoord, int countSize = 0)
        {

            AdvertisingStructure mc = _dbw.RetrieveStructure(id);
            var tempId = mc.Id;
            foreach (var side in mc.Sides)
            {
                _context.Surfaces.RemoveRange(from s in mc.Sides from sur in s.Surfaces select _context.Surfaces.FirstOrDefault(si => si.Id == sur.Id));
            }

            _context.Sides.RemoveRange(mc.Sides.Select(side => _context.Sides.FirstOrDefault(x => x.Id == side.Id)));
            _context.AdvertisingStructures.Remove(_context.AdvertisingStructures.FirstOrDefault(x => x.Id_show == mc.Id_show));
            if (structures.Code == null)
            {
                structures.Code = "UI";
            }

            _context.SaveChanges();
            
            structures.Id = tempId;
            structures.Code = "UI";

            structures = ValidateCoords(structures, Bcoord, Hcoord);

            _context.AdvertisingStructures.Add(structures);
            
            _context.SaveChanges();

            //картики

            ValidatePic(photo1, photoInd1, structures.Id_show.ToString(), mc.Id_show.ToString(), "photo1");
            ValidatePic(photo2, photoInd2, structures.Id_show.ToString(), mc.Id_show.ToString(), "photo2");

            return RedirectToAction((string)Session["action"], (string)Session["controller"]);

        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateIllegalDesign()
        {
            ViewBag.Code = "UI";
            ViewBag.UniqueNumber = TableAdapterExtensions.StringSymvol();
            ViewBag.SeizesCount = 0;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateIllegalDesign(AdvertisingStructure structures,
            HttpPostedFileBase photo1, HttpPostedFileBase photo2,
            List<HttpPostedFileBase> severalPhoto, string Bcoord, string Hcoord,
            int countSize = 0)
        {
            
            Guid structuresId = Guid.NewGuid();
            structures.Id = structuresId;
            if (structures.Code == null)
            {
                structures.Code = "UI";
            }
            structures = ValidateCoords(structures, Bcoord, Hcoord);
            _dbw.CreateAdvertisingDesign(structures, null, null, 0);

            SavePic(structures.Id_show.ToString(), "photo1", photo1);

            SavePic(structures.Id_show.ToString(), "photo2", photo2);

            return RedirectToAction("AdvertisingDesign");
        }

        public ActionResult NotFound()
        {
            return View("DesignNotFound");
        }

        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
                _dbw.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

