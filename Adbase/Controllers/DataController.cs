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
            var owner = _context.Owners.First(m => m.Name == name);
            ViewBag.BackRef = backTo;
            return View(owner);
        }
        
        public IEnumerable<AdvertisingStructure> SearchAdversing(AdvertisingStructure advertisin, string owner,
            string typeOfAdvertisingStructure, string locality, int? countSize, string backlight, string endDate,int? areaConstruction,int? CountSurface)
        {
            var result = _dbw.SearchAdversing( advertisin, owner,typeOfAdvertisingStructure, locality, countSize, backlight,
                endDate, areaConstruction, CountSurface);
            return result;
        }

        public ActionResult FindStreets(string term, string cityname)
        {
            cityname.Normalize();
            var streets = from m in _context.Streets where m.Street1.Contains(term) select m;
            var projection = from street in streets
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
            var data = _context.AdvertisingStructures;
           
            return View(data);

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
            AdvertisingStructure mc = _context.AdvertisingStructures.SingleOrDefault(a => a.Id_show == id);
            if (mc==null)
            {
                return RedirectToAction("NotFound");
            }
            _dbw.DeleteAdvertisingDesign(id);
            
            //удаление картинок

            DeletePic(mc.Id_show.ToString(), "Scan1SidesWithFinancialManagement");

            DeletePic(mc.Id_show.ToString(), "ScanPassport_1Sides");

            DeletePic(mc.Id_show.ToString(), "ScanPassport_2Sides");

            DeletePic(mc.Id_show.ToString(), "Scan1Side");

            DeletePic(mc.Id_show.ToString(), "Scan2Side");

            DeletePic(mc.Id_show.ToString(), "Application");

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
            var result = TempData.Peek("surface");
            
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
            if (data == null)
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
            TempData["surface"] = surfaces;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            switch (type)
            {
                case "BB":
                    {
                        ViewBag.Type = "BB";
                        ViewBag.Id = data.Id_show;

                        ViewBag.Scan1Sides = LoadPic(data.Id_show.ToString(), "Scan1SidesWithFinancialManagement");
                        ViewBag.ScanPassport_1 = LoadPic(data.Id_show.ToString(), "ScanPassport_1Sides");
                        ViewBag.ScanPassport_2 = LoadPic(data.Id_show.ToString(), "ScanPassport_2Sides");
                        
                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = _context.AdvertisingStructures.Single(a => a.Id_show == id);
            return View(mc);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateAdvertisingDesign()
        {
            ViewBag.Code = "BB";
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
            //удаление временно номера из базы данных
            if (_context.ListUniqueNumbers.Any(a => a.UniqueNumber == structures.UniqueNumber))
            {
                _context.ListUniqueNumbers.RemoveRange(
                    _context.ListUniqueNumbers.Where(x => x.UniqueNumber == structures.UniqueNumber));
            }
            structures = ValidateCoords(structures, Bcoord, Hcoord);

            if (countSize > 0)
            {
                for (int j = 0; j < countSize; j++)
                {
                    sides[j].AdvertisingStructures_Id = structuresId;
                    sides[j].Name = (j + 1).ToString();
                    sides[j].Id = Guid.NewGuid();
                }

                _context.Sides.AddRange(sides);
                structures.Area = CountSquare(surfaces);
                _context.AdvertisingStructures.Add(structures);
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
            if (mc.Sides.Count == 0)
            {
                mc.Sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
            }
            foreach (var sides in mc.Sides.OrderBy(a => a.Name))
            {
                foreach (var surface in sides.Surfaces)
                {
                    surfaces.Add(surface);
                }
            }
            
            if (surfaces.Count == 0)
            {
                surfaces.Add(new Surface() {}
                    );
            }
            TempData["surface"] = surfaces;
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

            AdvertisingStructure mc = _context.AdvertisingStructures.Single(a => a.Id_show == id);
           
            var tempId = mc.Id;
            if (structures.UniqueNumber == null)
            {
                structures.UniqueNumber = TableAdapterExtensions.StringSymvol();
            }
            if (structures.Code == null)
            {
                structures.Code = "BB";
            }
            
            foreach (var side in mc.Sides)
            {
                _context.Surfaces.RemoveRange(side.Surfaces);

            }

            _context.Sides.RemoveRange(mc.Sides);
            _context.AdvertisingStructures.Remove(mc);
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
                structures = ValidateCoords(structures, Bcoord, Hcoord);

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

            foreach (var photo in photos)
            {
                _phw.ValidatePic(photos, picIndexes, structures.Id_show.ToString(), mc.Id_show.ToString());
            }

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

            if (data == null)
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
            TempData["surface"] = surfaces;
           
            switch (type)
            {
                case "MP":
                    {
                        ViewBag.Type = "MP";
                        ViewBag.Id = data.Id_show;
                        ViewBag.Scan1Side = LoadPic(data.Id_show.ToString(), "Scan1Side");
                        ViewBag.Scan2Side = LoadPic(data.Id_show.ToString(), "Scan2Side");
                        ViewBag.Scan2Side = LoadPic(data.Id_show.ToString(), "photo1");
                        
                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = _context.AdvertisingStructures.Single(a => a.Id_show == id);
            if (mc.coordB != null)
            {
                mc.coordB = double.Parse(mc.coordB.ToString().Substring(0, 7));
            }
            if (mc.coordH != null)
            {
                mc.coordH = double.Parse(mc.coordH.ToString().Substring(0, 7));
            }
            return View(mc);
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
            int countSize = 1)
        {

            Guid structuresId = Guid.NewGuid();
            structures.Id = structuresId;
            //удаление временно номера из базы данных
            if (_context.ListUniqueNumbers.Any(a => a.UniqueNumber == structures.UniqueNumber))
            {
                _context.ListUniqueNumbers.RemoveRange(
                    _context.ListUniqueNumbers.Where(x => x.UniqueNumber == structures.UniqueNumber));

            }
            if (structures.Code == null)
            {
                structures.Code = "MP";
            }
            if (sides.Count == 0)
            {
                sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
            }

            for (int j = 0; j < countSize; j++)
            {

                sides[j].AdvertisingStructures_Id = structuresId;
                sides[j].Name = (j + 1).ToString();
                sides[j].Id = Guid.NewGuid();
            }

            _context.Sides.AddRange(sides);
            structures.Area = CountSquare(surfaces);
             structures = ValidateCoords(structures, Bcoord, Hcoord);

            _context.AdvertisingStructures.Add(structures);
            List<Surface> listSurface = new List<Surface>();
            foreach (var i in surfaces)
            {
                i.Side_Id = sides.Single(a => a.Name == i.SideOfSurface).Id;
                listSurface.Add(i);

            }
            _context.Surfaces.AddRange(listSurface);

            _context.SaveChanges();

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
            TempData["surface"] = surfaces;
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
            HttpPostedFileBase scan1SidesWithFinancialManagement, string Scan1SideInd, string Scan2SideInd, string photo1Ind,
            string Bcoord, string Hcoord,
            int countSize = 1)
        {

            AdvertisingStructure mc = _context.AdvertisingStructures.Single(a => a.Id_show == id);
            var tempId = mc.Id;
            DeletePic(mc.Id_show.ToString(), "scanPassport1Sides");
            DeletePic(mc.Id_show.ToString(), "scanPassport2Sides");
            foreach (var side in mc.Sides)
            {
                _context.Surfaces.RemoveRange(side.Surfaces);
            }

            _context.Sides.RemoveRange(mc.Sides);
            _context.AdvertisingStructures.Remove(mc);
            _context.SaveChanges();
            if (structures.Code == null)
            {
                structures.Code = "MP";
            }
            if (sides.Count == 0)
            {
                sides.Add(new Side() {DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
            }

            for (int j = 0; j < countSize; j++)
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

            _context.Sides.RemoveRange(mc.Sides);

            _context.SaveChanges();

            //сохранение картинок

            ValidatePic(Scan1Side, Scan1SideInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "Scan1Side");
            ValidatePic(Scan2Side, Scan2SideInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "Scan2Side");
            ValidatePic(photo1, photo1Ind, structures.Id_show.ToString(), mc.Id_show.ToString(), "photo1");
           
           
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
            if (data == null)
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
            TempData["surface"] = surfaces;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            switch (type)
            {
                case "LD":
                    {
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

                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = _context.AdvertisingStructures.Single(a => a.Id_show == id);
            if (mc.coordB != null)
            {
                mc.coordB = double.Parse(mc.coordB.ToString().Substring(0, 7));
            }
            if (mc.coordH != null) { 
                    mc.coordH = double.Parse(mc.coordH.ToString().Substring(0, 7));
            }
            return View(mc);
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
            HttpPostedFileBase ScanPassport_1Sides, HttpPostedFileBase ScanPassport_2Sides,
            [ModelBinder(typeof(CustomModelBinderForPicsForAD))] Dictionary<string, HttpPostedFileBase> photos,
            HttpPostedFileBase Application,
            HttpPostedFileBase scan1Side, HttpPostedFileBase scan2Side,
            string Bcoord, string Hcoord, int countSize = 1)
        {

            Guid structuresId = Guid.NewGuid();
            structures.Id = structuresId;
            //удаление временно номера из базы данных
            if (_context.ListUniqueNumbers.Any(a => a.UniqueNumber == structures.UniqueNumber))
            {
                _context.ListUniqueNumbers.RemoveRange(
                    _context.ListUniqueNumbers.Where(x => x.UniqueNumber == structures.UniqueNumber));

            }

            for (int j = 0; j < countSize; j++)
            {
                sides[j].AdvertisingStructures_Id = structuresId;
                sides[j].Name = (j + 1).ToString();
                sides[j].Id = Guid.NewGuid();
            }

            _context.Sides.AddRange(sides);
            structures.Area = CountSquare(surfaces);
            structures = ValidateCoords(structures, Bcoord, Hcoord);

            _context.AdvertisingStructures.Add(structures);
            List<Surface> listSurface = new List<Surface>();
            foreach (var i in surfaces)
            {
                i.Side_Id = sides.Single(a => a.Name == i.SideOfSurface).Id;
                listSurface.Add(i);
            }
            _context.Surfaces.AddRange(listSurface);

            _context.SaveChanges();
            string id = structures.Id_show.ToString();

            SavePic(id, "ScanPassport_1Sides", ScanPassport_1Sides);

            SavePic(id, "ScanPassport_2Sides", ScanPassport_2Sides);

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
            TempData["surface"] = surfaces;
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
            HttpPostedFileBase ScanPassport_1Sides, HttpPostedFileBase ScanPassport_2Sides, HttpPostedFileBase Application,
            HttpPostedFileBase scan1Side, HttpPostedFileBase scan2Side, 
            string ScanPassport_1SidesInd, string ScanPassport_2SidesInd, string scan1SideInd, string scan2SideInd, 
            string photo1Ind, string photo2Ind, string ApplicationInd, 
            string Bcoord, string Hcoord, int countSize = 1)
        {

            AdvertisingStructure mc = _context.AdvertisingStructures.Single(a => a.Id_show == id);
            
            var tempId = mc.Id;
            if (structures.Code == null)
            {
                structures.Code = "LD";
            }
            foreach (var side in mc.Sides)
            {
                _context.Surfaces.RemoveRange(side.Surfaces);
            }

            _context.Sides.RemoveRange(mc.Sides);
            _context.AdvertisingStructures.Remove(mc);
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
            ValidatePic(ScanPassport_1Sides, ScanPassport_1SidesInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "ScanPassport_1Sides");
            ValidatePic(ScanPassport_2Sides, ScanPassport_2SidesInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "ScanPassport_2Sides");
            ValidatePic(scan1Side, scan1SideInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "Scan1Side");
            ValidatePic(scan2Side, scan2SideInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "Scan2Side");
            ValidatePic(Application, ApplicationInd, structures.Id_show.ToString(), mc.Id_show.ToString(), "Application");

            foreach (var photo in photos)
            {
                _phw.ValidatePic(photos, picIndexes, structures.Id_show.ToString(), mc.Id_show.ToString());
            }

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
            
            if (data == null)
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
            TempData["surface"] = surfaces;
            
            switch (type)
            {
                case "UI":
                    {
                        ViewBag.Type = "UI";
                        ViewBag.Id = data.Id_show;

                        ViewBag.photo1 = LoadPic(data.Id_show.ToString(), "photo1");

                        ViewBag.photo2 = LoadPic(data.Id_show.ToString(), "photo2");

                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = _context.AdvertisingStructures.Single(a => a.Id_show == id);
            return View(mc);
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
            TempData["surface"] = surfaces;
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
            string Bcoord, string Hcoord, int countSize = 1)
        {

            AdvertisingStructure mc = _context.AdvertisingStructures.Single(a => a.Id_show == id);
            var tempId = mc.Id;
            foreach (var side in mc.Sides)
            {
                _context.Surfaces.RemoveRange(side.Surfaces);
            }
            if (structures.Code == null)
            {
                structures.Code = "UI";
            }
            _context.Sides.RemoveRange(mc.Sides);
            _context.AdvertisingStructures.Remove(mc);
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
            //удаление временно номера из базы данных
            if (_context.ListUniqueNumbers.Any(a => a.UniqueNumber == structures.UniqueNumber))
            {
                _context.ListUniqueNumbers.RemoveRange(
                    _context.ListUniqueNumbers.Where(x => x.UniqueNumber == structures.UniqueNumber));

            }

            if (structures.Code == null)
            {
                structures.Code = "UI";
            }
            
            structures = ValidateCoords(structures, Bcoord, Hcoord);
            _context.AdvertisingStructures.Add(structures);
            

            _context.SaveChanges();

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

