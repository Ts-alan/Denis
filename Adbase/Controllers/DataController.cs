using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sciencecom.Controllers
{
    using Models;
    using System.Net;
    public class DataController : Controller
    {
        private SciencecomEntities context = new SciencecomEntities();

        [Authorize]
        public ActionResult Owner(string name, string backTo)
        {
            var owner = context.Owners.First(m => m.Name == name);
            ViewBag.BackRef = backTo;
            return View(owner);
        }


        //#region Металлические указатели

        [Authorize]
        public ActionResult Metal()
        {
            ViewBag.Data = null;
            return View();
        }

       
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult CreateLight()
        {
            ViewBag.Owners = context.Owners.Select(m => m.Name);
            return View();
        }
        

        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult CreateLightOnStatement()
        {
            ViewBag.Owners = context.Owners.Select(m => m.Name);
            return View();
        }

        //[HttpPost]
        //[Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        //public ActionResult CreateLightOnStatement(LightConstruction lightConstruction, string dolgota, string shirota, string owner, string startDate, HttpPostedFileBase passport1, HttpPostedFileBase passport2, HttpPostedFileBase zajavlenie)
        //{
        //    if ((lightConstruction.Locality == "") || (dolgota == "") || (shirota == "") || (owner == "") || (startDate == ""))
        //    {
        //        ModelState.AddModelError("Error", "Имеются неверно заполненные поля. Все поля, обязательны для заполнения");
        //    }
        //    else
        //    {
        //        ModelState.Clear();
        //        lightConstruction.Dolgota = float.Parse(dolgota.Replace(".", ","));
        //        lightConstruction.Shirota = float.Parse(shirota.Replace(".", ","));
        //        lightConstruction.StartDate = Convert.ToDateTime(startDate);
        //    }
        //    context.LightConstructions.Add(lightConstruction);
        //    var idOwner = context.Owners.Where(m => m.Name == owner).Single().Id;
        //    lightConstruction.IdOwner = idOwner;
        //    //lightConstruction.FinishDate = Convert.ToDateTime("01:01:0001");
        //    lightConstruction.OnStatement = true;
        //    var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
        //    ApplicationUser user = um.FindByName(User.Identity.GetUserName());
        //    var idWhoAdded = context.Owners.Where(m => m.Name == user.Company).Single().Id;
        //    lightConstruction.IdWhoAdded = idWhoAdded;
        //    if (lightConstruction.IsSocial)
        //    {
        //        lightConstruction.DateOfSocial = DateTime.Now.Date.ToString().Replace(" 0:00:00", "");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        context.SaveChanges();
        //        if (((passport1 != null) && (passport1.ContentLength > 0) && (passport1.ContentLength < 1050578)) &&
        //         ((passport2 != null) && (passport2.ContentLength > 0) && (passport2.ContentLength < 1050578)) &&
        //         ((zajavlenie != null) && (zajavlenie.ContentLength > 0) && (zajavlenie.ContentLength < 1050578)))
        //        {
        //            string src = "~/Images/Light/passport" + lightConstruction.Id + "1.jpg";
        //            string path = Server.MapPath(src);
        //            System.IO.File.Delete(path);
        //            passport1.SaveAs(path);


        //            src = "~/Images/Light/passport" + lightConstruction.Id + "2.jpg";
        //            path = Server.MapPath(src);
        //            System.IO.File.Delete(path);
        //            passport2.SaveAs(path);

        //            src = "~/Images/Light/z" + lightConstruction.Id + ".jpg";
        //            path = Server.MapPath(src);
        //            System.IO.File.Delete(path);
        //            zajavlenie.SaveAs(path);
        //        }
        //        else
        //        {
        //            context.LightConstructions.Remove(lightConstruction);
        //            context.SaveChanges();
        //            ModelState.AddModelError("Photo", "Нельзя загрузить пустой файл или файл, размер которого превышает 1 МБ");
        //        }
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        return RedirectToAction("Light", context.LightConstructions);
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}
        //#endregion
        //#endregion

        //#region Методы поиска
        //public IEnumerable<LightConstruction> SearchLight(LightConstruction lightConstruction, string startDay, string startMonth, string startYear, string finishDay, string finishMonth, string finishYear, string onStatement, string isSocial)
        //{
        //    context = new SciencecomEntities();
        //    IEnumerable<LightConstruction> result = context.LightConstructions;
        //    if (lightConstruction.IdOwner != 0)
        //    {
        //        result = result.Where(m => m.IdOwner == lightConstruction.IdOwner);
        //    }
        //    if (lightConstruction.Street1 != "")
        //    {
        //        result = result.Where(m => m.Street1.ToLower().Contains(lightConstruction.Street1.ToLower()));
        //    }
        //    if (lightConstruction.Street2 != "")
        //    {
        //        result = result.Where(m => m.Street2.ToLower().Contains(lightConstruction.Street2.ToLower()));
        //    }
        //    if (lightConstruction.FromStreet != "")
        //    {
        //        result = result.Where(m => m.FromStreet.ToLower().Contains(lightConstruction.FromStreet.ToLower()));
        //    }
        //    if (lightConstruction.Locality != "")
        //    {
        //        result = result.Where(m => m.Locality.ToLower().Contains(lightConstruction.Locality.ToLower()));
        //    }
        //    if (startDay != "")
        //    {
        //        result = result.Where(m => m.StartDate.Day == int.Parse(startDay));
        //    }
        //    if (startMonth != "")
        //    {
        //        result = result.Where(m => m.StartDate.Month == int.Parse(startMonth));
        //    }
        //    if (startYear != "")
        //    {
        //        result = result.Where(m => m.StartDate.Year == int.Parse(startYear));
        //    }
        //    if (finishDay != "")
        //    {
        //        result = result.Where(m => m.FinishDate.Day == int.Parse(finishDay));
        //    }
        //    if (finishMonth != "")
        //    {
        //        result = result.Where(m => m.FinishDate.Month == int.Parse(finishMonth));
        //    }
        //    if (finishYear != "")
        //    {
        //        result = result.Where(m => m.FinishDate.Year == int.Parse(finishYear));
        //    }
        //    if (onStatement != "Не важно")
        //    {
        //        if (onStatement == "Да")
        //        {
        //            result = result.Where(m => m.OnStatement == true);
        //        }
        //        else
        //        {
        //            result = result.Where(m => m.OnStatement == false);
        //        }
        //    }
        //    if (isSocial != "Не важно")
        //    {
        //        if (isSocial == "Да")
        //        {
        //            result = result.Where(m => m.IsSocial == true);
        //        }
        //        else
        //        {
        //            result = result.Where(m => m.IsSocial == false);
        //        }
        //    }
        //    return result;
        //}

        //public IEnumerable<MetalConstruction> SearchMetal(MetalConstruction metalConstruction, string day, string month, string year)
        //{
        //    context = new SciencecomEntities();
        //    IEnumerable<MetalConstruction> result = context.MetalConstructions;
        //    if (metalConstruction.IdOwner != 0)
        //    {
        //        result = result.Where(m => m.IdOwner == metalConstruction.IdOwner);
        //    }
        //    if (metalConstruction.Street1 != "")
        //    {
        //        result = result.Where(m => m.Street1.ToLower().Contains(metalConstruction.Street1.ToLower()));
        //    }
        //    if (metalConstruction.Street2 != "")
        //    {
        //        result = result.Where(m => m.Street2.ToLower().Contains(metalConstruction.Street2.ToLower()));
        //    }
        //    if (metalConstruction.FromStreet != "")
        //    {
        //        result = result.Where(m => m.FromStreet.ToLower().Contains(metalConstruction.FromStreet.ToLower()));
        //    }
        //    if (metalConstruction.Locality != "")
        //    {
        //        result = result.Where(m => m.Locality.ToLower().Contains(metalConstruction.Locality.ToLower()));
        //    }
        //    if (day != "")
        //    {
        //        result = result.Where(m => m.StartDate.Day == int.Parse(day));
        //    }
        //    if (month != "")
        //    {
        //        result = result.Where(m => m.StartDate.Month == int.Parse(month));
        //    }
        //    if (year != "")
        //    {
        //        result = result.Where(m => m.StartDate.Year == int.Parse(year));
        //    }
        //    return result;
        //}


        //#endregion
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

        public ActionResult FindStreets(string term)
        {
            var streets = from m in context.Streets where m.Street1.Contains(term) select m;
            var projection = from street in streets
                             select new
                             {
                                 id = street.id,
                                 label = street.Street1 + " " + street.Type,
                                 value = street.Street1 + " " + street.Type
                             };
            return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
        }

        //public ActionResult FindStory(string term)
        //{
        //    var streets = from m in context.Surfaces where m.Story.Contains(term) select m;
        //    var projection = from street in streets
        //        select new
        //        {
        //            label = street.Story,
        //            value = street.Story
        //        };
        //    return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
        //}

        ////обслуживающий класс для Documents
        //private class Lookup
        //{
        //    public string Key;
        //    public string Value;
        //}

        //[Authorize]
        //[HttpGet]
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
                        string src = "~/Images/Scan1SidesWithFinancialManagement/" + data.Id +
                             "FinancialManagement.jpg";
                        string path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Scan1Sides = true;
                        }
                        else
                        {
                            ViewBag.Scan1Sides = false;
                        }
                        src = "~/Images/ScanPassport_1Sides/" + data.Id + "passport.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.ScanPassport_1 = true;
                        }
                        else
                        {
                            ViewBag.ScanPassport_1 = false;
                        }
                        src = "~/Images/ScanPassport_2Sides/" + data.Id + "ScanPassport_2Sides.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.ScanPassport_2 = true;
                        }
                        else
                        {
                            ViewBag.ScanPassport_2 = false;
                        }
                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            return View(mc);
        }

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
                        string src = "~/Images/Scan1SidesWithFinancialManagement/" + data.Id +
                             "FinancialManagement.jpg";
                        string path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Scan1Sides = true;
                        }
                        else
                        {
                            ViewBag.Scan1Sides = false;
                        }
                        src = "~/Images/ScanPassport_1Sides/" + data.Id + "passport.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.ScanPassport_1 = true;
                        }
                        else
                        {
                            ViewBag.ScanPassport_1 = false;
                        }
                        src = "~/Images/ScanPassport_2Sides/" + data.Id + "ScanPassport_2Sides.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.ScanPassport_2 = true;
                        }
                        else
                        {
                            ViewBag.ScanPassport_2 = false;
                        }
                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            return View(mc);
        }

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
                        string src = "~/Images/Scan1SidesWithFinancialManagement/" + data.Id +
                             "FinancialManagement.jpg";
                        string path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Scan1Sides = true;
                        }
                        else
                        {
                            ViewBag.Scan1Sides = false;
                        }
                        src = "~/Images/ScanPassport_1Sides/" + data.Id + "passport.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.ScanPassport_1 = true;
                        }
                        else
                        {
                            ViewBag.ScanPassport_1 = false;
                        }
                        src = "~/Images/ScanPassport_2Sides/" + data.Id + "ScanPassport_2Sides.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.ScanPassport_2 = true;
                        }
                        else
                        {
                            ViewBag.ScanPassport_2 = false;
                        }
                        src = "~/Images/Scan1Side/" + data.Id + "Scan1Side.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Scan1Side = true;
                        }
                        else
                        {
                            ViewBag.Scan1Side = false;
                        }
                        src = "~/Images/Scan1Side/" + data.Id + "Scan1Side.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Scan2Side = true;
                        }
                        else
                        {
                            ViewBag.Scan2Side = false;
                        }
                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            return View(mc);
        }

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
                        string src = "~/Images/photo1/" + data.Id +
                             "photo1.jpg";
                        string path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.photo1 = true;
                        }
                        else
                        {
                            ViewBag.photo1 = false;
                        }
                        src = "~/Images/photo2/" + data.Id +
                             "photo2.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.photo2 = true;
                        }
                        else
                        {
                            ViewBag.photo2 = false;
                        }

                    }
                    break;

                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            return View(mc);
        }

        //Щит
        [Authorize]
        [HttpGet]
        public ActionResult AdvertisingDesign()
        {
            var data = context.AdvertisingStructures;

            return View(data);

        }

        //[Authorize]
        //[HttpPost]
        //public ActionResult AdvertisingDesign(string owner, string locality, string street1, string street2,
        //    string fromStreet, string day, string month, string year)
        //{
        //    Owner proprietor = new Owner();
        //    if (!string.IsNullOrEmpty(owner))
        //    {
        //        proprietor = context.Owners.Where(m => m.Name == owner).Single();
        //    }
        //    Advertisins1 mc = new Advertisins1()
        //    {
        //        Street1 = street1,
        //        Street2 = street2,
        //        FromStreet = fromStreet,
        //        Owner_Id = proprietor.Id,
        //        Locality = locality
        //    };
        //    ViewBag.Data = mc;
        //    ViewBag.Day = day;
        //    ViewBag.Month = month;
        //    ViewBag.Year = year;
        //    return View();

        //}


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
                context.ListUniqueNumbers.Remove(
                    context.ListUniqueNumbers.Single(x => x.UniqueNumber == Structures.UniqueNumber));

            }

            if (CountSize > 0)
            {
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
            }
            else
            {
                context.AdvertisingStructures.Add(Structures);
                context.SaveChanges();
            }

            

            if (Scan1SidesWithFinancialManagement != null)
            {
                string src = "~/Images/Scan1SidesWithFinancialManagement/" + Structures.Id +
                             "FinancialManagement.jpg";
                string path = Server.MapPath(src);
                ScanPassport_1Sides.SaveAs(path);
            }

            if (ScanPassport_1Sides != null)
            {
                string src = "~/Images/ScanPassport_1Sides/" + Structures.Id + "passport.jpg";
                string path = Server.MapPath(src);
                ScanPassport_1Sides.SaveAs(path);
            }
            if (ScanPassport_2Sides != null)
            {
                string src = "~/Images/ScanPassport_2Sides/" + Structures.Id + "ScanPassport_2Sides.jpg";
                string path = Server.MapPath(src);
                ScanPassport_2Sides.SaveAs(path);
            }

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
            if (Scan1SidesWithFinancialManagement != null)
            {
                string src = "~/Images/Scan1SidesWithFinancialManagement/" + Structures.Id +
                             "FinancialManagement.jpg";
                string path = Server.MapPath(src);
                ScanPassport_1Sides.SaveAs(path);
            }

            if (ScanPassport_1Sides != null)
            {
                string src = "~/Images/ScanPassport_1Sides/" + Structures.Id + "passport.jpg";
                string path = Server.MapPath(src);
                ScanPassport_1Sides.SaveAs(path);
            }
            if (ScanPassport_2Sides != null)
            {
                string src = "~/Images/ScanPassport_2Sides/" + Structures.Id + "ScanPassport_2Sides.jpg";
                string path = Server.MapPath(src);
                ScanPassport_2Sides.SaveAs(path);
            }

            return RedirectToAction("AdvertisingDesign");


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
            if (Scan1SidesWithFinancialManagement != null)
            {
                string src = "~/Images/Scan1SidesWithFinancialManagement/" + Structures.Id +
                             "FinancialManagement.jpg";
                string path = Server.MapPath(src);
                ScanPassport_1Sides.SaveAs(path);
            }

            if (ScanPassport_1Sides != null)
            {
                string src = "~/Images/ScanPassport_1Sides/" + Structures.Id + "passport.jpg";
                string path = Server.MapPath(src);
                ScanPassport_1Sides.SaveAs(path);
            }
            if (ScanPassport_2Sides != null)
            {
                string src = "~/Images/ScanPassport_2Sides/" + Structures.Id + "ScanPassport_2Sides.jpg";
                string path = Server.MapPath(src);
                ScanPassport_2Sides.SaveAs(path);
            }
            if (Scan1Side != null)
            {
                string src = "~/Images/Scan1Side /" + Structures.Id + "Scan1Side.jpg";
                string path = Server.MapPath(src);
                Scan1Side.SaveAs(path);
            }
            if (Scan2Side != null)
            {
                string src = "~/Images/Scan2Side/" + Structures.Id + "Scan2Side.jpg";
                string path = Server.MapPath(src);
                Scan2Side.SaveAs(path);
            }

            return RedirectToAction("AdvertisingDesign");
        }


        [Authorize]
        [HttpGet]
        public ActionResult CreateIllegalDesign()
        {
            ViewBag.Code = "UI";
            ViewBag.UniqueNumber = TableAdapterExtensions.StringSymvol();
            ViewBag.SeizesCount = 1;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateIllegalDesign(AdvertisingStructure Structures,
            [ModelBinder(typeof(CustomModelBinderForSide))] List<Side> Sides,
            [ModelBinder(typeof(CustomModelBinderForSurface))] List<Surface> surfaces,
            HttpPostedFileBase photo1, HttpPostedFileBase photo2,
            List<HttpPostedFileBase> SeveralPhoto,
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
           
            if (photo1 != null)
            {
                string src = "~/Images/photo1/" + Structures.Id + "photo1.jpg";
                string path = Server.MapPath(src);
                photo1.SaveAs(path);
            }
            if (photo2 != null)
            {
                string src = "~/Images/photo2/" + Structures.Id + "photo2.jpg";
                string path = Server.MapPath(src);
                photo2.SaveAs(path);
            }

            return RedirectToAction("AdvertisingDesign");
        }


        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult DeleteAdvertisingDesign(int? id,string switchtoMap)
        {
            AdvertisingStructure mc = context.AdvertisingStructures.Single(a => a.Id_show == id);
            context.ListUniqueNumbers.Add(new ListUniqueNumber() { UniqueNumber = mc.UniqueNumber, Code_id = mc.Code, TimeOpen = DateTime.Now });
            foreach (var side in mc.Sides)
            {
                context.Surfaces.RemoveRange(side.Surfaces);
            }

            context.Sides.RemoveRange(mc.Sides);
            context.AdvertisingStructures.Remove(mc);
            context.SaveChanges();

            //удаление картинок
            string src = "/Images/Scan1SidesWithFinancialManagement/" + mc.Id +
                         "FinancialManagement.jpg";
            string path = Server.MapPath(src);
            FileInfo info1 = new FileInfo(path);
            if (info1.Exists)
            {
                info1.Delete();
            }

            src = "/Images/ScanPassport_1Sides/" + mc.Id + "passport.jpg";
            path = Server.MapPath(src);
            info1 = new FileInfo(path);
            if (info1.Exists)
            {
                info1.Delete();
            }

            src = "/Images/ScanPassport_2Sides/" + mc.Id + "ScanPassport_2Sides.jpg";
            path = Server.MapPath(src);
            info1 = new FileInfo(path);
            if (info1.Exists)
            {
                info1.Delete();
            }

            if (switchtoMap == "true")
            {
                return RedirectToAction("Index","Map"); 
            }
            return RedirectToAction("AdvertisingDesign");
        }
        

        //[Authorize]
        //public ActionResult ShowAdvertisinTablePartial(Advertisins1 Advertisin, string day, string month, string year, string owner)
        //{
        //    if (Advertisin == null)
        //    {
        //        ViewBag.Results = null;
        //        return View(context.Advertisins1.OrderByDescending(m => m.StartDate));
        //    }
        //    else
        //    {
        //        IEnumerable<Advertisins1> result = SearchAdvertisin(Advertisin, day, month, year, owner);
        //        ViewBag.Results = result.Count();
        //        return View(result.OrderByDescending(m => m.StartDate));
        //    }
        //}

        //public IEnumerable<Advertisins1> SearchAdvertisin(Sciencecom.Models.Advertisins1 Advertisin, string day, string month, string year,string owner)
        //{

        //    context = new SciencecomEntities();
        //    IEnumerable<Advertisins1> result = context.Advertisins1;
        //    if (owner != "" && owner != null)
        //    {
        //        var id_Owner = context.Owners.Single(a => a.Name.ToLower() == owner.ToLower()).Id;
        //         result = result.Where(m => m.Owner.Id == id_Owner);
        //    }
        //    if (Advertisin.Street1 != "" && Advertisin.Street1 != null)
        //    {
        //        result = result.Where(m => m.Street1.ToLower().Contains(Advertisin.Street1.ToLower()));
        //    }
        //    if (Advertisin.Street2 != "" && Advertisin.Street2!=null)
        //    {
        //        result = result.Where(m => m.Street2.ToLower().Contains(Advertisin.Street2.ToLower()));
        //    }
        //    if (Advertisin.FromStreet != "" && Advertisin.FromStreet != null)
        //    {
        //        result = result.Where(m => m.FromStreet.ToLower().Contains(Advertisin.FromStreet.ToLower()));
        //    }
        //    if (Advertisin.Locality != "" && Advertisin.Locality != null)
        //    {
        //        result = result.Where(m => m.Locality.ToLower().Contains(Advertisin.Locality.ToLower()));
        //    }
        //    if (day != "")
        //    {
        //        result = result.Where(m => m.StartDate.Day == int.Parse(day));
        //    }
        //    if (month != "")
        //    {
        //        result = result.Where(m => m.StartDate.Month == int.Parse(month));
        //    }
        //    if (year != "")
        //    {
        //        result = result.Where(m => m.StartDate.Year == int.Parse(year));
        //    }

        //    return result;
        //}

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
                    catch (System.IndexOutOfRangeException e)
                    {
                        Sides.Add(new Side());
                    }
                    
                    Sides[j].AdvertisingStructures_Id = mc.Id;
                    Sides[j].Name = (j + 1).ToString();
                    Sides[j].Id = Guid.NewGuid();
                }
                Structures.Id = TempId;

                context.AdvertisingStructures.Add(Structures);
                context.Sides.AddRange(Sides);
                //context.AdvertisingStructures.Add(Structures);
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
                    Sides.Add(new Side());
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
                    Sides.Add(new Side());
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

    }
}

