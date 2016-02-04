﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sciencecom.Controllers
{
    using Models;
    using System.Net;
    public class DataController : BaseDataController
    {
        private SciencecomEntities context = new SciencecomEntities();

        [Authorize]
        public ActionResult Owner(string name, string backTo)
        {
            var owner = context.Owners.First(m => m.Name == name);
            ViewBag.BackRef = backTo;
            return View(owner);
        }
        
        public IEnumerable<AdvertisingStructure> SearchAdversing(AdvertisingStructure Advertisin, string owner,
            string TypeOfAdvertisingStructure, string Locality, int? CountSize, string Backlight, string EndDate,int? AreaConstruction)
        {

            context = new SciencecomEntities();
            var owner_id = context.Owners.SingleOrDefault(a => a.Name.ToLower().Contains(owner.ToLower()));
            var typeOfAdvertisingStructure_id =
                context.TypeOfAdvertisingStructures.SingleOrDefault(a => a.Name.ToLower().Contains(TypeOfAdvertisingStructure.ToLower()));
            var locality_id = context.TypeOfAdvertisingStructures.SingleOrDefault(a => a.Name.ToLower().Contains(Locality.ToLower()));
            List<Backlight> backlights=null;
            if (Backlight != null && Backlight != "")  
            backlights = context.Backlights.Where(a => a.Name.Contains(Backlight)).ToList();
            IEnumerable<AdvertisingStructure> result;
            //var Foo = context.AdvertisingStructures.ToList();
            if (CountSize!=null)
            {
                 result = context.AdvertisingStructures.Where(a => a.Sides.Count == CountSize && a.Breadth != null && a.Height != null).ToList();
            }
            else
            {
                result = context.AdvertisingStructures.Where(a => a.Breadth != null && a.Height != null).ToList();
            }
            if (AreaConstruction != null)
            {
              int sum;
              result= result.Where(a =>
                 {
                    sum = 0;
                    foreach (var side in a.Sides)
                    {
                        foreach (var surface in side.Surfaces)
                        {
                            sum += surface.Space;
                        }

                    }
                    if (AreaConstruction == sum)
                    {
                        return true;
                    }
                    return false;
                });
            }
            if (owner_id != null)
            {
                result = result.Where(m => m.Owner.Id == owner_id.Id);
            }
            if (typeOfAdvertisingStructure_id != null)
            {
                result = result.Where(m => m.Code == typeOfAdvertisingStructure_id.Code);
            }
            if (locality_id != null)
            {
                result = result.Where(m => m.Locality_id == locality_id.id);
            }
            if (backlights != null)
            {
             result = result.Where(m =>
                    {
                        if (m.Backlight_Id.HasValue)
                        {
                            if (backlights.Select(a=>a.id.ToString()).Contains(m.Backlight_Id.ToString()))
                            return true;
                        }
                            return false;  
                      
                    }).ToList();
            }
            if (!string.IsNullOrEmpty(Advertisin.Street1))
            {
                result = result.Where(m => m.Street1.ToLower().Contains(Advertisin.Street1.ToLower()));
            }
            if (!string.IsNullOrEmpty(Advertisin.UniqueNumber))
            {
                result = result.Where(m => m.UniqueNumber.ToLower().Contains(Advertisin.UniqueNumber.ToLower()));
            }
            if ( !string.IsNullOrEmpty(Advertisin.House1))
            {
                result = result.Where(m => m.House1.ToLower().Contains(Advertisin.House1.ToLower()));
            }
            if (!string.IsNullOrEmpty(Advertisin.C_ContractFinancialManagement))
            {
                result = result.Where(m => m.C_ContractFinancialManagement.ToLower().Contains(Advertisin.C_ContractFinancialManagement.ToLower()));
            }
            if (!string.IsNullOrEmpty(Advertisin.C_PassportAdvertising))
            {
                result = result.Where(m => m.C_PassportAdvertising.ToLower().Contains(Advertisin.C_PassportAdvertising.ToLower()));
            }
            if (!string.IsNullOrEmpty(EndDate))
            {
                result = result.Where(m => m.EndDate.ToString().ToLower().Trim().Contains(EndDate));
            }



            return result;
        }

        public ActionResult FindStreets(string term, string cityname)
        {
            cityname.Normalize();
            var streets = from m in context.Streets where m.Street1.Contains(term) select m;
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
            var data = context.AdvertisingStructures;

            return View(data);

        }
        

        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult DeleteAdvertisingDesign(int? id,string switchtoMap)
        {
            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            if (mc.UniqueNumber == null)
            {
                mc.UniqueNumber = TableAdapterExtensions.StringSymvol();
            }

            context.ListUniqueNumbers.Add(new ListUniqueNumber() { UniqueNumber = mc.UniqueNumber, Code_id = mc.Code, TimeOpen = DateTime.Now });

            foreach (var side in mc.Sides)
            {
                if (side.Surfaces.Count > 0)
                {
                    context.Surfaces.RemoveRange(side.Surfaces);
                }
            }
            //context.SaveChanges();
            if (mc.Sides.Count > 0)
            {
                context.Sides.RemoveRange(mc.Sides);
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (DbEntityValidationResult validationError in e.EntityValidationErrors)
                    {
                        Response.Write("Object: " + validationError.Entry.Entity.ToString());
                        Response.Write("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            Response.Write(err.ErrorMessage + "");
                        }
                    }
                }
            }

            context.AdvertisingStructures.Remove(mc);
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (DbEntityValidationResult validationError in e.EntityValidationErrors)
                {
                    Response.Write("Object: " + validationError.Entry.Entity.ToString());
                    Response.Write("");
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        Response.Write(err.ErrorMessage + "");
                    }
                }
            }
            
            //удаление картинок

            DeletePic(mc.Id.ToString(), "Scan1SidesWithFinancialManagement");

            DeletePic(mc.Id.ToString(), "ScanPassport_1Sides");

            DeletePic(mc.Id.ToString(), "ScanPassport_2Sides");

            DeletePic(mc.Id.ToString(), "Scan1Side");

            DeletePic(mc.Id.ToString(), "Scan2Side");

            if (switchtoMap == "true")
            {
                return RedirectToAction("Index", "Map");
            }
            return RedirectToAction("AdvertisingDesign");
        }

        public ActionResult AddSurface(string side, int? StartCountForSurface, int? EndCountForSurface, bool IsEdit = false)
        {
            
            ViewBag.EndCountForSurface = EndCountForSurface ?? 1 ;
            ViewBag.StartCountForSurface = StartCountForSurface ?? 0;
            ViewBag.Side = side;

            return View("Surface");
        }

        public ActionResult EditSurface(string side, int? StartCountForSurface, int? EndCountForSurface)
        {
            ViewBag.EndCountForSurface = EndCountForSurface ?? 1;
            ViewBag.StartCountForSurface = StartCountForSurface ?? 0;
            ViewBag.Side = side;
            var result = TempData.Peek("surface");
            
            return View("Surface", result);
        }

        #region Щиты

        public ActionResult Documents(int? id, string type = "BB")
        {
            var data = context.AdvertisingStructures.Single(a => a.Id_show == id);
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
                        ViewBag.Id = data.Id;

                        ViewBag.Scan1Sides = LoadPic(data.Id.ToString(), "Scan1SidesWithFinancialManagement");
                        ViewBag.ScanPassport_1 = LoadPic(data.Id.ToString(), "ScanPassport_1Sides");
                        ViewBag.ScanPassport_2 = LoadPic(data.Id.ToString(), "ScanPassport_2Sides");
                        
                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            return View(mc);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateAdvertisingDesign(AdvertisingStructure Structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> Sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase ScanPassport_1Sides, HttpPostedFileBase ScanPassport_2Sides,
            HttpPostedFileBase Scan1SidesWithFinancialManagement, List<HttpPostedFileBase> SeveralPhoto,
            int CountSize = 0)
        {

            Guid StructuresId = Guid.NewGuid();
            Structures.Id = StructuresId;
            //удаление временно номера из базы данных
            if (context.ListUniqueNumbers.Any(a => a.UniqueNumber == Structures.UniqueNumber))
            {
               context.ListUniqueNumbers.RemoveRange(context.ListUniqueNumbers.Where(x => x.UniqueNumber == Structures.UniqueNumber));
            }

            if (CountSize > 0)
            {
                if (Sides.Count == 0)
                {
                    Sides.Add(
                        new Side(){ DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") }
                        );
                }
                for (int j = 0; j < CountSize; j++)
                {
                    Sides[j].AdvertisingStructures_Id = StructuresId;
                    Sides[j].Name = (j + 1).ToString();
                    Sides[j].Id = Guid.NewGuid();
                }

                context.Sides.AddRange(Sides);
                context.AdvertisingStructures.Add(Structures);
                List<Surface> ListSurface = new List<Surface>();
                foreach (var i in surfaces)
                {
                    i.Side_Id = Sides.Single(a => a.Name == i.SideOfSurface).Id;
                    ListSurface.Add(i);

                }

                context.Surfaces.AddRange(ListSurface);

                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (DbEntityValidationResult validationError in e.EntityValidationErrors)
                    {
                        Response.Write("Object: " + validationError.Entry.Entity.ToString());
                        Response.Write("");
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            Response.Write(err.ErrorMessage + "");
                        }
                    }
                }
            }
            else
            {
                context.AdvertisingStructures.Add(Structures);
                context.SaveChanges();
            }

            SavePic(Structures.Id.ToString(), "Scan1SidesWithFinancialManagement", Scan1SidesWithFinancialManagement);

            SavePic(Structures.Id.ToString(), "ScanPassport_1Sides", ScanPassport_1Sides);

            SavePic(Structures.Id.ToString(), "ScanPassport_2Sides", ScanPassport_2Sides);

            return RedirectToAction("AdvertisingDesign");


        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateAdvertisingDesign()
        {
            ViewBag.Code = "BB";
            ViewBag.UniqueNumber = TableAdapterExtensions.StringSymvol();
            return View();
        }

        [HttpGet]
        public ActionResult EditAdvertisingDesign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
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
            if (surfaces.Count == 0)
            {
                surfaces.Add(new Surface() {}
                    );
            }
            TempData["surface"] = surfaces;
            return View(mc);
        }

        [HttpPost]
        public ActionResult EditAdvertisingDesign(int id, AdvertisingStructure Structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> Sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase ScanPassport_1Sides, HttpPostedFileBase ScanPassport_2Sides,
            HttpPostedFileBase Scan1SidesWithFinancialManagement,
            int CountSize = 0)
        {

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            var TempId = mc.Id;
            if (Structures.UniqueNumber == null)
            {
                Structures.UniqueNumber = TableAdapterExtensions.StringSymvol();
            }
            if (Structures.Code == null)
            {
                Structures.Code = "BB";
            }
            
            foreach (var side in mc.Sides)
            {
                context.Surfaces.RemoveRange(side.Surfaces);

            }

            context.Sides.RemoveRange(mc.Sides);
            context.AdvertisingStructures.Remove(mc);
            context.SaveChanges();
            if (CountSize > 0)
            {
                for (int j = 0; j < CountSize; j++)
                {
                    try
                    {
                        Sides[j].AdvertisingStructures_Id = mc.Id;
                    }
                    catch (Exception e)
                    {
                        Sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
                    }
                    
                    Sides[j].AdvertisingStructures_Id = mc.Id;
                    Sides[j].Name = (j + 1).ToString();
                    Sides[j].Id = Guid.NewGuid();
                }
                Structures.Id = TempId;

                context.AdvertisingStructures.Add(Structures);
                
                context.Sides.AddRange(Sides);
                
                context.SaveChanges();
                
                List<Surface> ListSurface = new List<Surface>();
                foreach (var i in surfaces)
                {
                    i.Side_Id = Sides.Single(a => a.Name == i.SideOfSurface).Id;
                    ListSurface.Add(i);

                }
                context.Surfaces.AddRange(ListSurface);
                
                context.SaveChanges();

            }
            else
            {
                context.AdvertisingStructures.Add(Structures);
                
                context.SaveChanges();
               
            }
            
            //картики
            if (Scan1SidesWithFinancialManagement != null)
            {
                string src = "~/Images/Scan1SidesWithFinancialManagement/" + Structures.Id +
                             "FinancialManagement.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                Scan1SidesWithFinancialManagement.SaveAs(path);
            }

            if (ScanPassport_1Sides != null)
            {

                string src = "~/Images/ScanPassport_1Sides/" + Structures.Id + "passport.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                ScanPassport_1Sides.SaveAs(path);
            }
            if (ScanPassport_2Sides != null)
            {
                string src = "~/Images/ScanPassport_2Sides/" + Structures.Id + "ScanPassport_2Sides.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                ScanPassport_2Sides.SaveAs(path);
            }

            return RedirectToAction("AdvertisingDesign");
        }

        #endregion

        #region Металлические указатели

        public ActionResult Pointer(int? id, string type = "MP")
        {
            var data = context.AdvertisingStructures.Single(a => a.Id_show == id);
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
                case "MP":
                    {
                        ViewBag.Type = "MP";
                        ViewBag.Id = data.Id;

                        ViewBag.Scan1Sides = LoadPic(data.Id.ToString(), "Scan1SidesWithFinancialManagement");
                        ViewBag.ScanPassport_1 = LoadPic(data.Id.ToString(), "ScanPassport_1Sides");
                        ViewBag.ScanPassport_2 = LoadPic(data.Id.ToString(), "ScanPassport_2Sides");
                        
                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
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
        public ActionResult CreateMetalPointerDesign(AdvertisingStructure Structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> Sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase ScanPassport_1Sides, HttpPostedFileBase ScanPassport_2Sides,
            HttpPostedFileBase Scan1SidesWithFinancialManagement, List<HttpPostedFileBase> SeveralPhoto,
            int CountSize = 1)
        {

            Guid StructuresId = Guid.NewGuid();
            Structures.Id = StructuresId;
            //удаление временно номера из базы данных
            if (context.ListUniqueNumbers.Any(a => a.UniqueNumber == Structures.UniqueNumber))
            {
                context.ListUniqueNumbers.Remove(
                    context.ListUniqueNumbers.Single(x => x.UniqueNumber == Structures.UniqueNumber));

            }
            if (Structures.Code == null)
            {
                Structures.Code = "MP";
            }
            if (Sides.Count == 0)
            {
                Sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
            }

            for (int j = 0; j < CountSize; j++)
            {

                Sides[j].AdvertisingStructures_Id = StructuresId;
                Sides[j].Name = (j + 1).ToString();
                Sides[j].Id = Guid.NewGuid();
            }

            context.Sides.AddRange(Sides);
            context.AdvertisingStructures.Add(Structures);
            List<Surface> ListSurface = new List<Surface>();
            foreach (var i in surfaces)
            {
                i.Side_Id = Sides.Single(a => a.Name == i.SideOfSurface).Id;
                ListSurface.Add(i);

            }
            context.Surfaces.AddRange(ListSurface);

            context.SaveChanges();

            SavePic(Structures.Id.ToString(), "Scan1SidesWithFinancialManagement", Scan1SidesWithFinancialManagement);

            SavePic(Structures.Id.ToString(), "ScanPassport_1Sides", ScanPassport_1Sides);

            SavePic(Structures.Id.ToString(), "ScanPassport_2Sides", ScanPassport_2Sides);

            return RedirectToAction("AdvertisingDesign");

        }

        [HttpGet]
        public ActionResult EditMetalPointerDesign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
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
            return View(mc);
        }

        [HttpPost]
        public ActionResult EditMetalPointerDesign(int id, AdvertisingStructure Structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> Sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase ScanPassport_1Sides, HttpPostedFileBase ScanPassport_2Sides,
            HttpPostedFileBase Scan1SidesWithFinancialManagement,
            int CountSize = 1)
        {

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            var TempId = mc.Id;
           
            foreach (var side in mc.Sides)
            {
                context.Surfaces.RemoveRange(side.Surfaces);
            }

            context.Sides.RemoveRange(mc.Sides);
            context.AdvertisingStructures.Remove(mc);
            context.SaveChanges();
            if (Structures.Code == null)
            {
                Structures.Code = "MP";
            }
            if (Sides.Count == 0)
            {
                Sides.Add(new Side() {DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
            }

            for (int j = 0; j < CountSize; j++)
            {
               
                Sides[j].AdvertisingStructures_Id = mc.Id;
                Sides[j].Name = (j + 1).ToString();
                Sides[j].Id = Guid.NewGuid();
            }
            Structures.Id = TempId;

            context.AdvertisingStructures.Add(Structures);
            context.Sides.AddRange(Sides);
            
            List<Surface> ListSurface = new List<Surface>();
            foreach (var i in surfaces)
            {
                i.Side_Id = Sides.Single(a => a.Name == i.SideOfSurface).Id;
                ListSurface.Add(i);
            }
            context.Surfaces.AddRange(ListSurface);

            context.Sides.RemoveRange(mc.Sides);
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (DbEntityValidationResult validationError in e.EntityValidationErrors)
                {
                    Response.Write("Object: " + validationError.Entry.Entity.ToString());
                    Response.Write("");
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        Response.Write(err.ErrorMessage + "");
                    }
                }
            }

            //картики
            if (Scan1SidesWithFinancialManagement != null)
            {
                string src = "~/Images/Scan1SidesWithFinancialManagement/" + Structures.Id +
                             "FinancialManagement.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                Scan1SidesWithFinancialManagement.SaveAs(path);
            }

            if (ScanPassport_1Sides != null)
            {

                string src = "~/Images/ScanPassport_1Sides/" + Structures.Id + "passport.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                ScanPassport_1Sides.SaveAs(path);
            }
            if (ScanPassport_2Sides != null)
            {
                string src = "~/Images/ScanPassport_2Sides/" + Structures.Id + "ScanPassport_2Sides.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                ScanPassport_2Sides.SaveAs(path);
            }

            return RedirectToAction("AdvertisingDesign");

        }

        #endregion

        #region Световые короба

        public ActionResult LightDuct(int? id, string type = "LD")
        {
            var data = context.AdvertisingStructures.Single(a => a.Id_show == id);
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
                        ViewBag.Id = data.Id;

                        ViewBag.Scan1Sides = LoadPic(data.Id.ToString(), "Scan1SidesWithFinancialManagement");
                        ViewBag.ScanPassport_1 = LoadPic(data.Id.ToString(), "ScanPassport_1Sides");
                        ViewBag.ScanPassport_2 = LoadPic(data.Id.ToString(), "ScanPassport_2Sides");
                        ViewBag.Scan1Side = LoadPic(data.Id.ToString(), "Scan1Side");
                        ViewBag.Scan2Side = LoadPic(data.Id.ToString(), "Scan2Side");
                
                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            return View(mc);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateLightDuctDesign()
        {
            ViewBag.Code = "LD";
            ViewBag.UniqueNumber = TableAdapterExtensions.StringSymvol();
            ViewBag.SeizesCount = 2;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateLightDuctDesign(AdvertisingStructure Structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> Sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase ScanPassport_1Sides, HttpPostedFileBase ScanPassport_2Sides,
            HttpPostedFileBase Scan1SidesWithFinancialManagement, List<HttpPostedFileBase> SeveralPhoto,
            HttpPostedFileBase Scan1Side, HttpPostedFileBase Scan2Side, int CountSize = 1)
        {

            Guid StructuresId = Guid.NewGuid();
            Structures.Id = StructuresId;
            //удаление временно номера из базы данных
            if (context.ListUniqueNumbers.Any(a => a.UniqueNumber == Structures.UniqueNumber))
            {
                context.ListUniqueNumbers.Remove(
                    context.ListUniqueNumbers.Single(x => x.UniqueNumber == Structures.UniqueNumber));

            }

            for (int j = 0; j < CountSize; j++)
            {


                Sides[j].AdvertisingStructures_Id = StructuresId;
                Sides[j].Name = (j + 1).ToString();
                Sides[j].Id = Guid.NewGuid();
            }

            context.Sides.AddRange(Sides);
            context.AdvertisingStructures.Add(Structures);
            List<Surface> ListSurface = new List<Surface>();
            foreach (var i in surfaces)
            {
                i.Side_Id = Sides.Single(a => a.Name == i.SideOfSurface).Id;
                ListSurface.Add(i);

            }
            context.Surfaces.AddRange(ListSurface);

            context.SaveChanges();

            SavePic(Structures.Id.ToString(), "Scan1SidesWithFinancialManagement", Scan1SidesWithFinancialManagement);

            SavePic(Structures.Id.ToString(), "ScanPassport_1Sides", ScanPassport_1Sides);

            SavePic(Structures.Id.ToString(), "ScanPassport_2Sides", ScanPassport_2Sides);
            
            SavePic(Structures.Id.ToString(), "Scan1Side", Scan1Side);

            SavePic(Structures.Id.ToString(), "Scan2Side", Scan2Side);
            
            return RedirectToAction("AdvertisingDesign");
        }

        [HttpGet]
        public ActionResult EditLightDuctDesign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
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
            mc.Code = "LD";
            return View(mc);
        }

        [HttpPost]
        public ActionResult EditLightDuctDesign(int id, AdvertisingStructure Structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> Sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase ScanPassport_1Sides, HttpPostedFileBase ScanPassport_2Sides,
            HttpPostedFileBase Scan1SidesWithFinancialManagement,
            HttpPostedFileBase Scan1Side, HttpPostedFileBase Scan2Side, int CountSize = 1)
        {

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            var TempId = mc.Id;

            foreach (var side in mc.Sides)
            {
                context.Surfaces.RemoveRange(side.Surfaces);
            }

            context.Sides.RemoveRange(mc.Sides);
            context.AdvertisingStructures.Remove(mc);
            context.SaveChanges();

            for (int j = 0; j < CountSize; j++)
            {
                try
                {
                    Sides[j].AdvertisingStructures_Id = mc.Id;
                }
                catch (System.IndexOutOfRangeException e)
                {
                    Sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
                }
                Sides[j].AdvertisingStructures_Id = mc.Id;
                Sides[j].Name = (j + 1).ToString();
                Sides[j].Id = Guid.NewGuid();
            }
            Structures.Id = TempId;

            context.AdvertisingStructures.Add(Structures);
            context.Sides.AddRange(Sides);

            List<Surface> ListSurface = new List<Surface>();
            foreach (var i in surfaces)
            {
                i.Side_Id = Sides.Single(a => a.Name == i.SideOfSurface).Id;
                ListSurface.Add(i);
            }
            context.Surfaces.AddRange(ListSurface);

            context.SaveChanges();

            //картики
            if (Scan1SidesWithFinancialManagement != null)
            {
                string src = "~/Images/Scan1SidesWithFinancialManagement/" + Structures.Id +
                             "FinancialManagement.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                Scan1SidesWithFinancialManagement.SaveAs(path);
            }

            if (ScanPassport_1Sides != null)
            {

                string src = "~/Images/ScanPassport_1Sides/" + Structures.Id + "passport.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                ScanPassport_1Sides.SaveAs(path);
            }
            if (ScanPassport_2Sides != null)
            {
                string src = "~/Images/ScanPassport_2Sides/" + Structures.Id + "ScanPassport_2Sides.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                ScanPassport_2Sides.SaveAs(path);
            }
            if (Scan1Side != null)
            {

                string src = "~/Images/Scan1Side/" + Structures.Id + "Scan1Side.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                Scan1Side.SaveAs(path);
            }
            if (Scan2Side != null)
            {
                string src = "~/Images/Scan2Side/" + Structures.Id + "Scan2Side.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                Scan2Side.SaveAs(path);
            }
            return RedirectToAction("AdvertisingDesign");

        }

        #endregion

        #region Неопознанные конструкции

        public ActionResult Illegal(int? id, string type = "UI")
        {
            var data = context.AdvertisingStructures.Single(a => a.Id_show == id);
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
                case "UI":
                    {
                        ViewBag.Type = "UI";
                        ViewBag.Id = data.Id;

                        ViewBag.photo1 = LoadPic(data.Id.ToString(), "photo1");

                        ViewBag.photo2 = LoadPic(data.Id.ToString(), "photo2");

                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            return View(mc);
        }

        [HttpGet]
        public ActionResult EditIllegalDesign(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
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
            mc.Code = "UI";
            return View(mc);
        }

        [HttpPost]
        public ActionResult EditIllegalDesign(int id, AdvertisingStructure Structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> Sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase photo1, HttpPostedFileBase photo2, int CountSize = 1)
        {

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            var TempId = mc.Id;

            foreach (var side in mc.Sides)
            {
                context.Surfaces.RemoveRange(side.Surfaces);
            }

            context.Sides.RemoveRange(mc.Sides);
            context.AdvertisingStructures.Remove(mc);
            context.SaveChanges();

            for (int j = 0; j < CountSize; j++)
            {
                try
                {
                    Sides[j].AdvertisingStructures_Id = mc.Id;
                }
                catch (System.IndexOutOfRangeException e)
                {
                    Sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
                }
                Sides[j].AdvertisingStructures_Id = mc.Id;
                Sides[j].Name = (j + 1).ToString();
                Sides[j].Id = Guid.NewGuid();
            }
            Structures.Id = TempId;
            Structures.Code = "UI";
            context.AdvertisingStructures.Add(Structures);
            context.Sides.AddRange(Sides);

            List<Surface> ListSurface = new List<Surface>();
            foreach (var i in surfaces)
            {
                i.Side_Id = Sides.Single(a => a.Name == i.SideOfSurface).Id;
                ListSurface.Add(i);
            }
            context.Surfaces.AddRange(ListSurface);

            context.SaveChanges();

            //картики

            if (photo1 != null)
            {

                string src = "~/Images/photo1/" + Structures.Id + "photo1.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                photo1.SaveAs(path);
            }
            if (photo2 != null)
            {
                string src = "~/Images/photo2/" + Structures.Id + "photo2.jpg";
                string path = Server.MapPath(src);
                FileInfo info1 = new FileInfo(path);
                if (info1.Exists)
                {
                    info1.Delete();
                }

                photo2.SaveAs(path);
            }
            return RedirectToAction("AdvertisingDesign");

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
        public ActionResult CreateIllegalDesign(AdvertisingStructure Structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> Sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase photo1, HttpPostedFileBase photo2,
            List<HttpPostedFileBase> SeveralPhoto,
            int CountSize = 0)
        {
            
            Guid StructuresId = Guid.NewGuid();
            Structures.Id = StructuresId;
            //удаление временно номера из базы данных
            if (context.ListUniqueNumbers.Any(a => a.UniqueNumber == Structures.UniqueNumber))
            {
                context.ListUniqueNumbers.Remove(
                    context.ListUniqueNumbers.Single(x => x.UniqueNumber == Structures.UniqueNumber));

            }

            if (Structures.Code == null)
            {
                Structures.Code = "UI";
            }

            for (int j = 0; j < CountSize; j++)
            {
                try
                {
                    Sides[j].AdvertisingStructures_Id = StructuresId;
                }
                catch (System.IndexOutOfRangeException e)
                {
                    Sides.Add(new Side() { DirectionSide_id = new Guid("27b8c509-8f09-4a0d-ae22-048c2611b7ea") });
                }
                Sides[j].AdvertisingStructures_Id = StructuresId;
                Sides[j].Name = (j + 1).ToString();
                Sides[j].Id = Guid.NewGuid();
            }

            context.Sides.AddRange(Sides);
            context.AdvertisingStructures.Add(Structures);
            List<Surface> ListSurface = new List<Surface>();
            foreach (var i in surfaces)
            {
                i.Side_Id = Sides.Single(a => a.Name == i.SideOfSurface).Id;
                ListSurface.Add(i);
            }
            context.Surfaces.AddRange(ListSurface);

            context.SaveChanges();

            SavePic(Structures.Id.ToString(), "photo1", photo1);

            SavePic(Structures.Id.ToString(), "photo2", photo2);

            return RedirectToAction("AdvertisingDesign");
        }

        #endregion

    }
}

