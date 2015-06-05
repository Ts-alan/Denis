using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace Sciencecom.Controllers
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity;
    using System.Net;
    using System.Threading;
    public class DataController : Controller
    {
        SciencecomEntities context = new SciencecomEntities();
        [Authorize]
        public ActionResult Owner(string name, string backTo)
        {
            var owner = context.Owners.First(m => m.Name == name);
            ViewBag.BackRef = backTo;
            return View(owner);
        }


        #region Металлические указатели
        [Authorize]
        public ActionResult Metal()
        {
            ViewBag.Data = null;
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Metal(string owner, string locality, string street1, string street2, string fromStreet, string day, string month, string year)
        {
            
            Owner o = new Owner();
            if (!string.IsNullOrEmpty(owner))
            {
                o = context.Owners.Where(m => m.Name == owner).Single();
            }
            MetalConstruction mc = new MetalConstruction()
            {
                Street1 = street1,
                Street2 = street2,
                FromStreet = fromStreet,
                IdOwner = o.Id,
                Support = 0,
                Locality = locality
            };
            ViewBag.Data = mc;
            ViewBag.Day = day;
            ViewBag.Month = month;
            ViewBag.Year = year;
            return View();
        }

        [Authorize]
        public ActionResult ShowMetalTablePartial(MetalConstruction metalConstruction, string locality, string day, string month, string year)
        {
            if (metalConstruction == null)
            {
                ViewBag.Results = null;
                return View(context.MetalConstructions.OrderByDescending(m => m.StartDate));
            }
            else
            {
                metalConstruction.Locality = locality;
                IEnumerable<MetalConstruction> result = SearchMetal(metalConstruction, day, month, year);
                //if (metalConstruction.StartDate.Date != Convert.ToDateTime("01.01.0001"))
                //{
                //    result = result.Where(m => m.StartDate == metalConstruction.StartDate);
                //}
                ViewBag.Results = result.Count();
                return View(result.OrderByDescending(m => m.StartDate));
            }
        }

        #region Создание
        [Authorize(Roles = "Admin, ChiefEditAll, ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult CreateMetal()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ChiefEditAll, ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult CreateMetal(MetalConstruction metalicConstruction, string shirota, string dolgota, string owner, string startDate, HttpPostedFileBase passport1, HttpPostedFileBase passport2, HttpPostedFileBase photo)
        {
            if ((metalicConstruction.Locality == "") || (dolgota == "") || (shirota == "")
                || (owner == "") || (startDate == ""))
            {
                ModelState.AddModelError("Error", "Имеются неверно заполненные поля. Все поля, за исключением поля \"Фото\", обязательны для заполнения.");
            }
            else
            {
                ModelState.Clear();
                metalicConstruction.Dolgota = float.Parse(dolgota.Replace(".", ","));
                metalicConstruction.Shirota = float.Parse(shirota.Replace(".", ","));
                metalicConstruction.StartDate = Convert.ToDateTime(startDate);
            }
            context.MetalConstructions.Add(metalicConstruction);
            var idOwner = context.Owners.Where(m => m.Name == owner).Single().Id;
            metalicConstruction.IdOwner = idOwner;
            
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser user = um.FindByName(User.Identity.GetUserName());
            var idWhoAdded = context.Owners.Where(m => m.Name == user.Company).Single().Id;
            metalicConstruction.IdWhoAdded = idWhoAdded;
            if (ModelState.IsValid)
            {
                context.SaveChanges();
                if (((passport1 != null) && (passport1.ContentLength > 0) && (passport1.ContentLength < 1050578)) &&
                ((passport2 != null) && (passport2.ContentLength > 0) && (passport2.ContentLength < 1050578))
                )
                {
                    string src = "~/Images/Metal/" + metalicConstruction.Id + "1.jpg";
                    string path = Server.MapPath(src);
                    passport1.SaveAs(path);

                    src = "~/Images/Metal/" + metalicConstruction.Id + "2.jpg";
                    path = Server.MapPath(src);
                    passport2.SaveAs(path);

                    if (photo != null) 
                    {
                        if ((photo.ContentLength > 0) && (photo.ContentLength < 1050578))
                        {
                            src = "~/Images/Metal/p" + metalicConstruction.Id + ".jpg";
                            path = Server.MapPath(src);
                            photo.SaveAs(path);
                        }
                        else
                        {
                            context.MetalConstructions.Remove(metalicConstruction);
                            context.SaveChanges();
                            ModelState.AddModelError("Photo", "Нельзя загрузить пустой файл или файл, размер которого превышает 1 МБ.");
                        }
                    }
                }
                else
                {
                    context.MetalConstructions.Remove(metalicConstruction);
                    context.SaveChanges();
                    ModelState.AddModelError("Photo", "Нельзя загрузить пустой файл или файл, размер которого превышает 1 МБ.");
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Metal", context.MetalConstructions);
            }
            else
                return View();
        }
        #endregion

        #region Редактирование
        [Authorize]
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult EditMetal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MetalConstruction mc = context.MetalConstructions.Find(id);
            if (mc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owners = context.Owners.Select(m => m.Name);
            return View(mc);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult EditMetal(MetalConstruction metalicConstruction, string owner, HttpPostedFileBase passport1, HttpPostedFileBase passport2, HttpPostedFileBase photo)
        {
            if ((metalicConstruction.Locality == "") || (owner == ""))
            {
                ModelState.AddModelError("Error", "Имеются неверно заполненные поля. Все поля, за исключением файлов, обязательны для заполнения");
            }
            else
            {
              
            }
            if (passport1 != null)
            {
                if ((passport1.ContentLength > 0) && (passport1.ContentLength < 1050578))
                {
                    string src = "~/Images/Metal/" + metalicConstruction.Id + "1.jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    passport1.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            if (passport2 != null)
            {
                if ((passport2.ContentLength > 0) && (passport2.ContentLength < 1050578))
                {
                    string src = "~/Images/Metal/" + metalicConstruction.Id + "2.jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    passport2.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            if (photo != null)
            {
                if ((photo.ContentLength > 0) && (photo.ContentLength < 1050578))
                {
                    string src = "~/Images/Metal/p" + metalicConstruction.Id + ".jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    photo.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            var mc = context.MetalConstructions.Find(metalicConstruction.Id);
            var idOwner = context.Owners.Where(m => m.Name == owner).Single().Id;
            mc.Id = metalicConstruction.Id;
            mc.IdOwner = idOwner;
            mc.Street1 = metalicConstruction.Street1;
            mc.Street2 = metalicConstruction.Street2;
            mc.FromStreet = metalicConstruction.FromStreet;
            mc.Support = metalicConstruction.Support;
            mc.StartDate = metalicConstruction.StartDate;
            mc.Dolgota = metalicConstruction.Dolgota;
            mc.Shirota = metalicConstruction.Shirota;
            ModelState["Owner"].Errors.Clear();
            if (ModelState.IsValid)
            {
                context.SaveChanges();
                return RedirectToAction("Metal", context.MetalConstructions);
            }
            else
            {
                return View(mc);
            }
        }
        #endregion

        #region Удаление
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult DeleteMetal(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MetalConstruction mc = context.MetalConstructions.Find(id);
            if (mc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owners = context.Owners.Select(m => m.Name);
            return View(mc);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult DeleteMetal(int id)
        {
            MetalConstruction mc = context.MetalConstructions.Find(id);
            context.MetalConstructions.Remove(mc);
            context.SaveChanges();

            string src = "~/Images/Metal/" + id + "1.jpg";
            string path = Server.MapPath(src);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            src = "~/Images/Metal/" + id + "2.jpg";
            path = Server.MapPath(src);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            src = "~/Images/Metal/p" + id + ".jpg";
            path = Server.MapPath(src);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            return RedirectToAction("Metal", context.MetalConstructions);
        }
        #endregion
        #endregion

        #region Световые указатели
        [Authorize]
        public ActionResult Light()
        {
            ViewBag.Data = null;
            return View(context.LightConstructions);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Light(string owner, string locality, string street1, string street2, string fromStreet, string startDay,
                                  string startMonth, string startYear, string finishDay, string finishMonth, string finishYear,
                                  string onStatement, string isSocial)
        {
            Owner o = new Owner();
            if (!string.IsNullOrEmpty(owner))
            {
                o = context.Owners.Where(m => m.Name == owner).Single();
            }
            LightConstruction lc = new LightConstruction()
            {
                Street1 = street1,
                Street2 = street2,
                IdOwner = o.Id,
                Support = 0,
                FromStreet = fromStreet,
                Locality = locality
            };
            ViewBag.OnStatement = onStatement;
            ViewBag.IsSocial = isSocial;
            ViewBag.Data = lc;
            ViewBag.StartDay = startDay;
            ViewBag.StartMonth = startMonth;
            ViewBag.StartYear = startYear;
            ViewBag.FinishDay = finishDay;
            ViewBag.FinishMonth = finishMonth;
            ViewBag.FinishYear = finishYear;
            return View();
        }

        [Authorize]
        public ActionResult ShowLightTablePartial(LightConstruction lightConstruction, string startDay,
                                 string startMonth, string startYear, string finishDay, string finishMonth, string finishYear,
                                 string onStatement, string isSocial)
        {
            if (lightConstruction == null)
            {
                ViewBag.Results = null;
                return View(context.LightConstructions.OrderBy(m => m.FinishDate));
            }
            else
            {
                IEnumerable<LightConstruction> result = SearchLight(lightConstruction, startDay, startMonth, startYear, finishDay, finishMonth, finishYear, onStatement, isSocial);
                ViewBag.Results = result.Count();
                return View(result.OrderBy(m => m.FinishDate));
            }
        }




        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult AddPassport(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LightConstruction lc = context.LightConstructions.Find(id);
            if (lc == null)
            {
                return HttpNotFound();
            }
            return View(lc);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult AddPassport(LightConstruction lightConstruction, string startDate, string finishDate,  HttpPostedFileBase ispolkom1, HttpPostedFileBase ispolkom2, HttpPostedFileBase photo)
        {

            if ((startDate == "") || (finishDate == ""))
            {
                ModelState.AddModelError("Error", "Имеются неверно заполненные поля. Все поля, за исключением поля \"Фото\", обязательны для заполнения");
                return View(context.LightConstructions.Find(lightConstruction.Id));
            }
            else
            {
                lightConstruction.StartDate = Convert.ToDateTime(startDate);
                lightConstruction.FinishDate = Convert.ToDateTime(finishDate);
            }
            var lc = context.LightConstructions.Find(lightConstruction.Id);
            lightConstruction.IdOwner = lc.IdOwner;
            lightConstruction.IdWhoAdded = lc.IdWhoAdded;
            lightConstruction.IsSocial = lc.IsSocial;
            lightConstruction.Locality = lc.Locality;
            lightConstruction.OnStatement = false;
            lightConstruction.Owner = lc.Owner;
            lightConstruction.OwnerWhoAdded = lc.OwnerWhoAdded;
            lightConstruction.Shirota = lc.Shirota;
            lightConstruction.Street1 = lc.Street1;
            lightConstruction.Street2 = lc.Street2;
            lightConstruction.Support = lc.Support;
            lightConstruction.DateOfSocial = lc.DateOfSocial;
            lightConstruction.FromStreet = lc.FromStreet;
            
            //lc.Id = lightConstruction.Id;
            //lc.StartDate = lightConstruction.StartDate;
            //lc.FinishDate = lightConstruction.FinishDate;
            //lc.OnStatement = false;
            context.LightConstructions.Add(lightConstruction);
            if (!ModelState.ContainsKey("Error"))
            {
                context.SaveChanges();
                string src;
                string path;
                    if (ispolkom1 != null)
                    {
                        if ((ispolkom1.ContentLength > 0) && (ispolkom1.ContentLength < 1050578))
                        {
                            src = "~/Images/Light/ispolkom" + lightConstruction.Id + "1.jpg";
                            path = Server.MapPath(src);
                            ispolkom1.SaveAs(path);
                        }
                        else
                        {
                            context.LightConstructions.Remove(lightConstruction);
                            ModelState.AddModelError("Photo", "Нельзя загрузить файл, размер которого превышает 1 МБ.");
                        }
                    }
                    if (ispolkom2 != null)
                    {
                        if ((ispolkom2.ContentLength > 0) && (ispolkom2.ContentLength < 1050578))
                        {
                            src = "~/Images/Light/ispolkom" + lightConstruction.Id + "2.jpg";
                            path = Server.MapPath(src);
                            // System.IO.File.Delete(path);
                            ispolkom2.SaveAs(path);
                        }
                        else
                        {
                            context.LightConstructions.Remove(lightConstruction);
                            ModelState.AddModelError("Photo", "Нельзя загрузить файл, размер которого превышает 1 МБ.");
                        }
                    }

                    if (photo != null)
                    {
                        if ((photo.ContentLength > 0) && (photo.ContentLength < 1050578))
                        {
                            src = "~/Images/Light/photo" + lightConstruction.Id + ".jpg";
                            path = Server.MapPath(src);
                            photo.SaveAs(path);
                        }
                        else
                        {
                            context.LightConstructions.Remove(lightConstruction);
                            ModelState.AddModelError("Photo", "Нельзя загрузить файл, размер которого превышает 1 МБ.");
                        }
                    } 
            }
            if ((!ModelState.ContainsKey("Error")) && (!ModelState.ContainsKey("Photo")))
            {
                string oldSrc = "~/Images/Light/passport" + lc.Id + "1.jpg";
                string oldPath = Server.MapPath(oldSrc);
                string newSrc = "~/Images/Light/passport" + lightConstruction.Id + "1.jpg";
                string newPath = Server.MapPath(newSrc);
                System.IO.File.Move(oldPath, newPath);

                oldSrc = "~/Images/Light/passport" + lc.Id + "2.jpg";
                oldPath = Server.MapPath(oldSrc);
                newSrc = "~/Images/Light/passport" + lightConstruction.Id + "2.jpg";
                newPath = Server.MapPath(newSrc);
                System.IO.File.Move(oldPath, newPath);

                context.LightConstructions.Remove(lc);
                context.SaveChanges();
                return RedirectToAction("Light", context.LightConstructions);
            }
            else
            {
                return View(lightConstruction);
            }       
        }

        #region Редактирование
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult EditLight(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LightConstruction lc = context.LightConstructions.Find(id);
            if (lc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owners = context.Owners.Select(m => m.Name);
            return View(lc);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult EditLightOnStatement(LightConstruction lightConstruction, string owner, string startDate, HttpPostedFileBase passport1, HttpPostedFileBase passport2, HttpPostedFileBase zajavlenie)
        {
            if ((lightConstruction.Locality == "") || (owner == "") || (startDate == ""))
            {
                ModelState.AddModelError("Error", "Имеются неверно заполненные поля. Все поля обязательны для заполнения");
            }
            else
            {
                lightConstruction.StartDate = Convert.ToDateTime(startDate);
            }
            if (passport1 != null)
            {
                if ((passport1.ContentLength > 0) && (passport1.ContentLength < 1050578))
                {
                    string src = "~/Images/Light/passport" + lightConstruction.Id + "1.jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    passport1.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            if (passport2 != null)
            {
                if ((passport2.ContentLength > 0) && (passport2.ContentLength < 1050578))
                {
                    string src = "~/Images/Light/passport" + lightConstruction.Id + "2.jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    passport2.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            if (zajavlenie != null)
            {
                if ((zajavlenie.ContentLength > 0) && (zajavlenie.ContentLength < 1050578))
                {
                    string src = "~/Images/Light/z" + lightConstruction.Id + ".jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    zajavlenie.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            var lc = context.LightConstructions.Find(lightConstruction.Id);
            var idOwner = context.Owners.Where(m => m.Name == owner).Single().Id;
            lc.Id = lightConstruction.Id;
            lc.IdOwner = idOwner;
            lc.Street1 = lightConstruction.Street1;
            lc.Street2 = lightConstruction.Street2;
            lc.FromStreet = lightConstruction.FromStreet;
            lc.Support = lightConstruction.Support;
            lc.StartDate = lightConstruction.StartDate;
            lc.IsSocial = lightConstruction.IsSocial;
            lc.OnStatement = true;
            if (lightConstruction.IsSocial)
            {
                lc.DateOfSocial = DateTime.Now.Date.ToShortDateString().ToString();
            }
            lc.Dolgota = lightConstruction.Dolgota;
            lc.Shirota = lightConstruction.Shirota;
            ModelState["Owner"].Errors.Clear();
            if (ModelState.IsValid)
            {
                context.SaveChanges();
                return RedirectToAction("Light", context.LightConstructions);
            }
            else
            {
                return View("EditLight", lc);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult EditLight(LightConstruction lightConstruction, string owner, string startDate, string finishDate, HttpPostedFileBase passport1, HttpPostedFileBase passport2, HttpPostedFileBase ispolkom1, HttpPostedFileBase ispolkom2, HttpPostedFileBase photo)
        {
            if ((lightConstruction.Locality == "") || (owner == "") || (startDate == "") || (finishDate == ""))
            {
                ModelState.AddModelError("Error", "Имеются неверно заполненные поля. Все поля обязательны для заполнения");
            }
            else
            {
                lightConstruction.StartDate = Convert.ToDateTime(startDate);
                lightConstruction.FinishDate = Convert.ToDateTime(finishDate);
            }
            if (passport1 != null)
            {
                if ((passport1.ContentLength > 0) && (passport1.ContentLength < 1050578))
                {
                    string src = "~/Images/Light/passport" + lightConstruction.Id + "1.jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    passport1.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            if (passport2 != null)
            {
                if ((passport2.ContentLength > 0) && (passport2.ContentLength < 1050578))
                {
                    string src = "~/Images/Light/passport" + lightConstruction.Id + "2.jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    passport2.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            if (ispolkom1 != null)
            {
                if ((ispolkom1.ContentLength > 0) && (ispolkom1.ContentLength < 1050578))
                {
                    string src = "~/Images/Light/ispolkom" + lightConstruction.Id + "1.jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    ispolkom1.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            if (ispolkom2 != null)
            {
                if ((ispolkom2.ContentLength > 0) && (ispolkom2.ContentLength < 1050578))
                {
                    string src = "~/Images/Light/ispolkom" + lightConstruction.Id + "2.jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    ispolkom2.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            if (photo != null)
            {
                if ((photo.ContentLength > 0) && (photo.ContentLength < 1050578))
                {
                    string src = "~/Images/Light/photo" + lightConstruction.Id + ".jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    photo.SaveAs(path);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 1 МБ");
                }
            }
            var lc = context.LightConstructions.Find(lightConstruction.Id);
            var idOwner = context.Owners.Where(m => m.Name == owner).Single().Id;
            lc.Id = lightConstruction.Id;
            lc.IdOwner = idOwner;
            lc.Street1 = lightConstruction.Street1;
            lc.Street2 = lightConstruction.Street2;
            lc.FromStreet = lightConstruction.FromStreet;
            lc.Support = lightConstruction.Support;
            lc.StartDate = lightConstruction.StartDate;
            lc.FinishDate = lightConstruction.FinishDate;
            lc.IsSocial = lightConstruction.IsSocial;
            if (lightConstruction.IsSocial)
            {
                lc.DateOfSocial = DateTime.Now.Date.ToShortDateString().ToString();
            }
            lc.OnStatement = false;
            lc.Dolgota = lightConstruction.Dolgota;
            lc.Shirota = lightConstruction.Shirota;
            ModelState["Owner"].Errors.Clear();
            if (ModelState.IsValid)
            {
                context.SaveChanges();
                return RedirectToAction("Light", context.LightConstructions);
            }
            else
            {
                return View(lc);
            }
        }
        #endregion

        #region Удаление
        [Authorize(Roles = "Admin, ChiefEditAll, ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult DeleteLight(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LightConstruction lc = context.LightConstructions.Find(id);
            if (lc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owners = context.Owners.Select(m => m.Name);
            return View(lc);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult DeleteLight(int id)
        {
            LightConstruction lc = context.LightConstructions.Find(id);
            if (lc.OnStatement)
            {
                string src = "~/Images/Light/passport" + id + "1.jpg";
                string path = Server.MapPath(src);
                System.IO.File.Delete(path);
                src = "~/Images/Light/passport" + id + "2.jpg";
                path = Server.MapPath(src);
                System.IO.File.Delete(path);
                src = "~/Images/Light/z" + id + ".jpg";
                path = Server.MapPath(src);
                System.IO.File.Delete(path);
            }
            else
            {
                string src = "~/Images/Light/passport" + id + "1.jpg";
                string path = Server.MapPath(src);
                System.IO.File.Delete(path);
                src = "~/Images/Light/passport" + id + "2.jpg";
                path = Server.MapPath(src);
                System.IO.File.Delete(path);
                src = "~/Images/Light/ispolkom" + id + "1.jpg";
                path = Server.MapPath(src);
                System.IO.File.Delete(path);
                src = "~/Images/Light/ispolkom" + id + "2.jpg";
                path = Server.MapPath(src);
                System.IO.File.Delete(path);
                src = "~/Images/Light/photo" + id + ".jpg";
                path = Server.MapPath(src);
                System.IO.File.Delete(path);
            }
            context.LightConstructions.Remove(lc);
            context.SaveChanges();
            return RedirectToAction("Light", context.LightConstructions);
        }
        #endregion

        #region Создание
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult CreateLight()
        {
            ViewBag.Owners = context.Owners.Select(m => m.Name);
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult CreateLight(LightConstruction lightConstruction, string dolgota, string shirota, string owner, string startDate, string finishDate, HttpPostedFileBase passport1, HttpPostedFileBase passport2, HttpPostedFileBase ispolkom1, HttpPostedFileBase ispolkom2, HttpPostedFileBase photo)
        {
            if ((lightConstruction.Locality == "") || (dolgota == "") || (shirota == "") || (owner == "") || (startDate == "") || (finishDate == ""))
            {
                ModelState.AddModelError("Error", "Имеются неверно заполненные поля. Все поля, за исключением поля \"Фото\", обязательны для заполнения");
            }
            else
            {
                ModelState.Clear();
                lightConstruction.Dolgota = float.Parse(dolgota.Replace(".", ","));
                lightConstruction.Shirota = float.Parse(shirota.Replace(".", ","));
                lightConstruction.StartDate = Convert.ToDateTime(startDate);
                lightConstruction.FinishDate = Convert.ToDateTime(finishDate);
            }
            context.LightConstructions.Add(lightConstruction);
            var idOwner = context.Owners.Where(m => m.Name == owner).Single().Id;
            lightConstruction.IdOwner = idOwner;
           
            lightConstruction.OnStatement = false;
            if (lightConstruction.IsSocial)
            {
                lightConstruction.DateOfSocial = DateTime.Now.Date.ToString().Replace(" 0:00:00", "");
            }
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser user = um.FindByName(User.Identity.GetUserName());
            var idWhoAdded = context.Owners.Where(m => m.Name == user.Company).Single().Id;
            lightConstruction.IdWhoAdded = idWhoAdded;
            if (ModelState.IsValid)
            {
                context.SaveChanges();
                if (((ispolkom1 != null) && (ispolkom1.ContentLength > 0) && (ispolkom1.ContentLength < 1050578))
                && ((ispolkom2 != null) && (ispolkom2.ContentLength > 0) && (ispolkom2.ContentLength < 1050578)))
                {
                    string src = "~/Images/Light/ispolkom" + lightConstruction.Id + "1.jpg";
                    string path = Server.MapPath(src);
                    //  System.IO.File.Delete(path);
                    ispolkom1.SaveAs(path);

                    src = "~/Images/Light/ispolkom" + lightConstruction.Id + "2.jpg";
                    path = Server.MapPath(src);
                    //   System.IO.File.Delete(path);
                    ispolkom2.SaveAs(path);

                    if (passport1 != null)
                    {
                        if ((passport1.ContentLength > 0) && (passport1.ContentLength < 1050578))
                        {
                            src = "~/Images/Light/passport" + lightConstruction.Id + "1.jpg";
                            path = Server.MapPath(src);
                            passport1.SaveAs(path);
                        }
                        else
                        {
                            context.LightConstructions.Remove(lightConstruction);
                            context.SaveChanges();
                            ModelState.AddModelError("Photo", "Нельзя загрузить файл, размер которого превышает 1 МБ.");
                        }
                    }
                    if (passport2 != null)
                    {
                        if ((passport2.ContentLength > 0) && (passport2.ContentLength < 1050578))
                        {
                            src = "~/Images/Light/passport" + lightConstruction.Id + "2.jpg";
                            path = Server.MapPath(src);
                            // System.IO.File.Delete(path);
                            passport2.SaveAs(path);
                        }
                        else
                        {
                            context.LightConstructions.Remove(lightConstruction);
                            context.SaveChanges();
                            ModelState.AddModelError("Photo", "Нельзя загрузить файл, размер которого превышает 1 МБ.");
                        }
                    }
                    if (photo != null)
                    {
                        if ((photo.ContentLength > 0) && (photo.ContentLength < 1050578))
                        {
                            src = "~/Images/Light/photo" + lightConstruction.Id + ".jpg";
                            path = Server.MapPath(src);
                            photo.SaveAs(path);
                        }
                        else
                        {
                            context.LightConstructions.Remove(lightConstruction);
                            context.SaveChanges();
                            ModelState.AddModelError("Photo", "Нельзя загрузить файл, размер которого превышает 1 МБ.");
                        }
                    }
                }
                else
                {
                    context.LightConstructions.Remove(lightConstruction);
                    context.SaveChanges();
                    ModelState.AddModelError("Photo", "Нельзя загрузить пустой файл или файл, размер которого превышает 1 МБ");
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Light", context.MetalConstructions);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult CreateLightOnStatement()
        {
            ViewBag.Owners = context.Owners.Select(m => m.Name);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult CreateLightOnStatement(LightConstruction lightConstruction, string dolgota, string shirota, string owner, string startDate, HttpPostedFileBase passport1, HttpPostedFileBase passport2, HttpPostedFileBase zajavlenie)
        {
            if ((lightConstruction.Locality == "") || (dolgota == "") || (shirota == "") || (owner == "") || (startDate == ""))
            {
                ModelState.AddModelError("Error", "Имеются неверно заполненные поля. Все поля, обязательны для заполнения");
            }
            else
            {
                ModelState.Clear();
                lightConstruction.Dolgota = float.Parse(dolgota.Replace(".", ","));
                lightConstruction.Shirota = float.Parse(shirota.Replace(".", ","));
                lightConstruction.StartDate = Convert.ToDateTime(startDate);
            }
            context.LightConstructions.Add(lightConstruction);
            var idOwner = context.Owners.Where(m => m.Name == owner).Single().Id;
            lightConstruction.IdOwner = idOwner;
            //lightConstruction.FinishDate = Convert.ToDateTime("01:01:0001");
            lightConstruction.OnStatement = true;
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser user = um.FindByName(User.Identity.GetUserName());
            var idWhoAdded = context.Owners.Where(m => m.Name == user.Company).Single().Id;
            lightConstruction.IdWhoAdded = idWhoAdded;
            if (lightConstruction.IsSocial)
            {
                lightConstruction.DateOfSocial = DateTime.Now.Date.ToString().Replace(" 0:00:00", "");
            }
            if (ModelState.IsValid)
            {
                context.SaveChanges();
                if (((passport1 != null) && (passport1.ContentLength > 0) && (passport1.ContentLength < 1050578)) &&
                 ((passport2 != null) && (passport2.ContentLength > 0) && (passport2.ContentLength < 1050578)) &&
                 ((zajavlenie != null) && (zajavlenie.ContentLength > 0) && (zajavlenie.ContentLength < 1050578)))
                {
                    string src = "~/Images/Light/passport" + lightConstruction.Id + "1.jpg";
                    string path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    passport1.SaveAs(path);


                    src = "~/Images/Light/passport" + lightConstruction.Id + "2.jpg";
                    path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    passport2.SaveAs(path);

                    src = "~/Images/Light/z" + lightConstruction.Id + ".jpg";
                    path = Server.MapPath(src);
                    System.IO.File.Delete(path);
                    zajavlenie.SaveAs(path);
                }
                else
                {
                    context.LightConstructions.Remove(lightConstruction);
                    context.SaveChanges();
                    ModelState.AddModelError("Photo", "Нельзя загрузить пустой файл или файл, размер которого превышает 1 МБ");
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Light", context.LightConstructions);
            }
            else
            {
                return View();
            }
        }
        #endregion
        #endregion

        #region Методы поиска
        public IEnumerable<LightConstruction> SearchLight(LightConstruction lightConstruction, string startDay, string startMonth, string startYear, string finishDay, string finishMonth, string finishYear, string onStatement, string isSocial)
        {
            context = new SciencecomEntities();
            IEnumerable<LightConstruction> result = context.LightConstructions;
            if (lightConstruction.IdOwner != 0)
            {
                result = result.Where(m => m.IdOwner == lightConstruction.IdOwner);
            }
            if (lightConstruction.Street1 != "")
            {
                result = result.Where(m => m.Street1.ToLower().Contains(lightConstruction.Street1.ToLower()));
            }
            if (lightConstruction.Street2 != "")
            {
                result = result.Where(m => m.Street2.ToLower().Contains(lightConstruction.Street2.ToLower()));
            }
            if (lightConstruction.FromStreet != "")
            {
                result = result.Where(m => m.FromStreet.ToLower().Contains(lightConstruction.FromStreet.ToLower()));
            }
            if (lightConstruction.Locality != "")
            {
                result = result.Where(m => m.Locality.ToLower().Contains(lightConstruction.Locality.ToLower()));
            }
            if (startDay != "")
            {
                result = result.Where(m => m.StartDate.Day == int.Parse(startDay));
            }
            if (startMonth != "")
            {
                result = result.Where(m => m.StartDate.Month == int.Parse(startMonth));
            }
            if (startYear != "")
            {
                result = result.Where(m => m.StartDate.Year == int.Parse(startYear));
            }
            if (finishDay != "")
            {
                result = result.Where(m => m.FinishDate.Day == int.Parse(finishDay));
            }
            if (finishMonth != "")
            {
                result = result.Where(m => m.FinishDate.Month == int.Parse(finishMonth));
            }
            if (finishYear != "")
            {
                result = result.Where(m => m.FinishDate.Year == int.Parse(finishYear));
            }
            if (onStatement != "Не важно")
            {
                if (onStatement == "Да")
                {
                    result = result.Where(m => m.OnStatement == true);
                }
                else
                {
                    result = result.Where(m => m.OnStatement == false);
                }
            }
            if (isSocial != "Не важно")
            {
                if (isSocial == "Да")
                {
                    result = result.Where(m => m.IsSocial == true);
                }
                else
                {
                    result = result.Where(m => m.IsSocial == false);
                }
            }
            return result;
        }

        public IEnumerable<MetalConstruction> SearchMetal(MetalConstruction metalConstruction, string day, string month, string year)
        {
            context = new SciencecomEntities();
            IEnumerable<MetalConstruction> result = context.MetalConstructions;
            if (metalConstruction.IdOwner != 0)
            {
                result = result.Where(m => m.IdOwner == metalConstruction.IdOwner);
            }
            if (metalConstruction.Street1 != "")
            {
                result = result.Where(m => m.Street1.ToLower().Contains(metalConstruction.Street1.ToLower()));
            }
            if (metalConstruction.Street2 != "")
            {
                result = result.Where(m => m.Street2.ToLower().Contains(metalConstruction.Street2.ToLower()));
            }
            if (metalConstruction.FromStreet != "")
            {
                result = result.Where(m => m.FromStreet.ToLower().Contains(metalConstruction.FromStreet.ToLower()));
            }
            if (metalConstruction.Locality != "")
            {
                result = result.Where(m => m.Locality.ToLower().Contains(metalConstruction.Locality.ToLower()));
            }
            if (day != "")
            {
                result = result.Where(m => m.StartDate.Day == int.Parse(day));
            }
            if (month != "")
            {
                result = result.Where(m => m.StartDate.Month == int.Parse(month));
            }
            if (year != "")
            {
                result = result.Where(m => m.StartDate.Year == int.Parse(year));
            }
            return result;
        }
       
        
        #endregion
        public IEnumerable<Billboards1> SearchBillbord(Billboards1 billboard, string startDay, string startMonth, string startYear, string finishDay, string finishMonth, string finishYear, string[] Story, string IsBillboardSocial, string owner)
        {
            bool isSocial;
            if (IsBillboardSocial == "Да")
            {
                isSocial = true;
            }
            else
            {
                isSocial = false;
            }
            context = new SciencecomEntities();
            IEnumerable<Billboards1> result = context.Billboards1.ToList();
            if (owner != "" && owner != null)
            {
                var id_Owner = context.Owners.Single(a => a.Name.ToLower() == owner.ToLower()).Id;
                result = result.Where(m => m.Owner.Id == id_Owner);
            }
            if (billboard.Street1 != "")
            {
                result = result.Where(m => m.Street1.ToLower().Contains(billboard.Street1.ToLower()));
            }
            if (billboard.Street2 != "")
            {
                result = result.Where(m => m.Street2.ToLower().Contains(billboard.Street2.ToLower()));
            }
            if (billboard.FromStreet != "")
            {
                result = result.Where(m => m.FromStreet.ToLower().Contains(billboard.FromStreet.ToLower()));
            }
            if (billboard.Locality != "")
            {
                result = result.Where(m => m.Locality.ToLower().Contains(billboard.Locality.ToLower()));
            }

            result = result.Where(m => m.OnAgreement == billboard.OnAgreement).ToList();
            int d = 0;
          
            if (startDay != "" && startMonth != "" && startYear != "")
            {
                DateTime startDate = new DateTime(Int32.Parse(startYear), Int32.Parse(startMonth), Int32.Parse(startDay));
                result = result.Where(a => a.StartDate.Date >= startDate.Date).ToList();
               }
            if (finishDay != "" && finishMonth != "" && finishYear != "")
            {
                DateTime endDate = new DateTime(Int32.Parse(finishYear), Int32.Parse(finishMonth), Int32.Parse(finishDay));
                result= result.Where(m => m.EndDate.Date <= endDate.Date).ToList();
               
            }
                 result = result.Where(a =>
                {
                    foreach (var side in a.Sides)
                    {
                        foreach (var surface in side.Surfaces)
                        {
                            if (isSocial == true)
                            {
                                if (surface.IsSocial == true)
                                {
                                    return true;
                                }
                            }
                            else
                            {
                                if (surface.IsSocial == true)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    if (isSocial == true)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }).ToList();
                 if (Story != null)
                 {
                     result = result.Where(a =>
                      {
                          foreach (var j in Story)
                          {
                              foreach (var side in a.Sides)
                              {
                                  foreach (var surface in side.Surfaces)
                                  {
                                      if (surface.Story == j)
                                      {
                                          return true;
                                      }
                                  }
                              }
                          }
                          return false;
                      });
                 }
          return result;
        }
        public ActionResult FindStreets(string term)
        {
            var streets = from m in context.Streets where m.Name.Contains(term) select m;
            var projection = from street in streets
                             select new
                             {
                                 id = street.Id,
                                 label = street.Name + " " + street.Type,
                                 value = street.Name + " " + street.Type
                             };
            return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult FindStory(string term)
        {
            var streets = from m in context.Surfaces where m.Story.Contains(term) select m;
            var projection = from street in streets
                             select new
                             {
                                 label = street.Story,
                                 value = street.Story
                             };
            return Json(projection.ToList(), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Documents(int? id, string type)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            switch (type)
            {
                case "m":
                    {
                        ViewBag.Type = "Metal";
                        ViewBag.Id = id;
                        string src = "~/Images/Metal/" +id + "1.jpg";
                        string path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.TSMO1 = true;
                        }
                        else
                        {
                            ViewBag.TSMO1 = false;
                        }
                        src = "~/Images/Metal/" + id + "2.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.TSMO2 = true;
                        }
                        else
                        {
                            ViewBag.TSMO2 = false;
                        }
                        src = "~/Images/Metal/p" + id + ".jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Photo = true;
                        }
                        else
                        {
                            ViewBag.Photo = false;
                        }
                    }
                    break;
                case "l":
                    {
                        ViewBag.Type = "Light";
                        ViewBag.Id = id;
                        string src = "~/Images/Light/passport" + id + "1.jpg";
                        string path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.TSMO1 = true;
                        }
                        else
                        {
                            ViewBag.TSMO1 = false;
                        }
                        src = "~/Images/Light/passport" + id + "2.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.TSMO2 = true;
                        }
                        else
                        {
                            ViewBag.TSMO2 = false;
                        }
                        src = "~/Images/Light/ispolkom" + id + "1.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.NR1 = true;
                        }
                        else
                        {
                            ViewBag.NR1 = false;
                        }
                        src = "~/Images/Light/ispolkom" + id + "2.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.NR2 = true;
                        }
                        else
                        {
                            ViewBag.NR2 = false;
                        }
                        src = "~/Images/Light/photo" + id + ".jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Photo = true;
                        }
                        else
                        {
                            ViewBag.Photo = false;
                        }
                    }
                    break;
                case "ls":
                    {
                        ViewBag.Type = "LightOnStatement";
                        ViewBag.Id = id;
                        string src = "~/Images/Light/passport" + id + ".jpg";
                        string path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.TSMO1 = true;
                        }
                        else
                        {
                            ViewBag.TSMO1 = false;
                        }
                        src = "~/Images/Light/photo" + id + ".jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.TSMO2 = true;
                        }
                        else
                        {
                            ViewBag.TSMO2 = false;
                        }
                        src = "~/Images/Light/z" + id + ".jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Zajavlenie = true;
                        }
                        else
                        {
                            ViewBag.Zajavlenie = false;
                        }
                    }
                    break;
                case "b":
                    {
                        ViewBag.Type = "Bilboard";
                        ViewBag.Id = id;
                        string src = "~/Images/Billboard/" + id + "passport.jpg";
                        string path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.passport = true;
                        }
                        else
                        {
                            ViewBag.passport = false;
                        }
                        src = "~/Images/Billboard/" + id + "photo.jpg";
                        path = Server.MapPath(src);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.photo = true;
                        }
                        else
                        {
                            ViewBag.photo = false;
                        }

                    }
                break;
                case "doc":
                {
                    ViewBag.Type = "Balyn";
                    ViewBag.Id = id;
                }
                break;
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }
        //билбордер
        [Authorize]
        [HttpGet]
        public ActionResult Bilboard()
        {

            ViewBag.Data = null;
            return View();

        }
        [Authorize]
        [HttpPost]
        public ActionResult Bilboard(string owner, string locality, string street1, string street2, string fromStreet, string day, string month, string year)
        {
            Owner proprietor = new Owner();
            if (!string.IsNullOrEmpty(owner))
            {
                proprietor = context.Owners.Where(m => m.Name == owner).Single();
            }
            Billboards1 mc = new Billboards1()
            {
                Street1 = street1,
                Street2 = street2,
                FromStreet = fromStreet,
                Owner_Id = proprietor.Id,
                Locality = locality
            };
            ViewBag.Data = mc;
            ViewBag.Day = day;
            ViewBag.Month = month;
            ViewBag.Year = year;
            return View();

        }

        
        [Authorize]
        [HttpPost]
        public ActionResult GreateBilboard(Sciencecom.Models.Billboards1 billboards, List<Sciencecom.Models.Surface> surfaces, string Owner, HttpPostedFileBase passport, HttpPostedFileBase photo, int CountSize = 1)
        {

            var idOwner = context.Owners.Single(m => m.Name == Owner).Id;
            billboards.Owner_Id = idOwner;
            Guid BillboardsId = Guid.NewGuid();
            billboards.Id = BillboardsId;
            string[] arraySize = new string[3] { "А", "B", "C" };
            List<Side> ListSide = new List<Side>();
            for (int j = 0; j < CountSize; j++)
            {
                ListSide.Add(new Side() { Billboard_Id = BillboardsId, Name = arraySize[j], Id = Guid.NewGuid() });
            }
            foreach (var i in surfaces)
            {
                i.Side_Id = ListSide.Single(a => a.Name == i.SideOfSurface).Id;
            }


            context.Surfaces.AddRange(surfaces);
            context.Sides.AddRange(ListSide);
            context.Billboards1.Add(billboards);

            context.SaveChanges();
            if (passport != null)
            {
                string src = "~/Images/Billboard/" + billboards.Id_show + "passport.jpg";
                string path = Server.MapPath(src);
                passport.SaveAs(path);
            }
            if (photo != null)
            {
                string src = "~/Images/Billboard/" + billboards.Id_show + "photo.jpg";
                string path = Server.MapPath(src);
                photo.SaveAs(path);
            }


            foreach (var surface in surfaces)
            {
              if (surface.SeveralPhoto!= null)
                {

                       string src = "~/Images/Billboard/surfaces/" + surface.Id + ".jpg";
                       string path = Server.MapPath(src);
                       surface.SeveralPhoto.SaveAs(path);
                }  
            }

            return RedirectToAction("Bilboard"); ;
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateBilboard()
        {

            return View();
        }

        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        [HttpGet]
        public ActionResult DeleteBilboard(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Billboards1 mc = context.Billboards1.Find(id);
            if (mc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Owners = context.Owners.Select(m => m.Name);
            return View(mc);
        }
        [HttpPost]
        [Authorize(Roles = "Admin, ChiefEditAll,ChiefEditOwn, SupplierEditAll, SupplierEditOwn")]
        public ActionResult DeleteBilboard(Guid id)
        {
            Billboards1 mc = context.Billboards1.Find(id);
            context.Billboards1.Remove(mc);
            IQueryable<Side> sideDelete = context.Sides.Where(a => a.Billboard_Id == id);
            context.Sides.RemoveRange(sideDelete);
            IQueryable<Surface> surfaceDelete = context.Surfaces;
            
            for (int i = 0; i < sideDelete.Count(); i++)
            {
                surfaceDelete.Where(a => a.Side_Id == sideDelete.ElementAt(i).Id);
            }
            context.Surfaces.RemoveRange(surfaceDelete);
            context.SaveChanges();

            //string src = "~/Images/Metal/" + id + "1.jpg";
            //string path = Server.MapPath(src);
            //if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            //src = "~/Images/Metal/" + id + "2.jpg";
            //path = Server.MapPath(src);
            //if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            //src = "~/Images/Metal/p" + id + ".jpg";
            //path = Server.MapPath(src);
            //if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            return RedirectToAction("Bilboard", context.MetalConstructions);
        }
        [Authorize]
        public ActionResult ShowBillboardTablePartial(Billboards1 billboard, string day, string month, string year, string owner)
        {
            if (billboard == null)
            {
                ViewBag.Results = null;
                return View(context.Billboards1.OrderByDescending(m => m.StartDate));
            }
            else
            {
                IEnumerable<Billboards1> result = SearchBillboard(billboard, day, month, year, owner);
                ViewBag.Results = result.Count();
                return View(result.OrderByDescending(m => m.StartDate));
            }
        }

        public IEnumerable<Billboards1> SearchBillboard(Sciencecom.Models.Billboards1 billboard, string day, string month, string year,string owner)
        {

            context = new SciencecomEntities();
            IEnumerable<Billboards1> result = context.Billboards1;
            if (owner != "" && owner != null)
            {
                var id_Owner = context.Owners.Single(a => a.Name.ToLower() == owner.ToLower()).Id;
                 result = result.Where(m => m.Owner.Id == id_Owner);
            }
            if (billboard.Street1 != "" && billboard.Street1 != null)
            {
                result = result.Where(m => m.Street1.ToLower().Contains(billboard.Street1.ToLower()));
            }
            if (billboard.Street2 != "" && billboard.Street2!=null)
            {
                result = result.Where(m => m.Street2.ToLower().Contains(billboard.Street2.ToLower()));
            }
            if (billboard.FromStreet != "" && billboard.FromStreet != null)
            {
                result = result.Where(m => m.FromStreet.ToLower().Contains(billboard.FromStreet.ToLower()));
            }
            if (billboard.Locality != "" && billboard.Locality != null)
            {
                result = result.Where(m => m.Locality.ToLower().Contains(billboard.Locality.ToLower()));
            }
            if (day != "")
            {
                result = result.Where(m => m.StartDate.Day == int.Parse(day));
            }
            if (month != "")
            {
                result = result.Where(m => m.StartDate.Month == int.Parse(month));
            }
            if (year != "")
            {
                result = result.Where(m => m.StartDate.Year == int.Parse(year));
            }

            return result;
        }

        public ActionResult PartialBilboard(int param , string side)
        {
            ViewBag.Incerment = param;
            ViewBag.Side = side;
            return View();
        }
        public ActionResult EditBillboard(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Billboards1 mc = context.Billboards1.Find(id);
            if (mc == null)
            {
                return HttpNotFound();
            }
            
            return View(mc);
        }
        [HttpPost]
        public ActionResult EditBillboard(Billboards1 BillboardConstruction, string Owner, HttpPostedFileBase passport1, HttpPostedFileBase passport2, HttpPostedFileBase photo)
        {

            var mc = context.Billboards1.Find(BillboardConstruction.Id);
            var idOwner = context.Owners.Where(m => m.Name == Owner).Single().Id;
            mc.Owner_Id = idOwner;
            mc.Street1 = BillboardConstruction.Street1;
            mc.Street2 = BillboardConstruction.Street2;
            mc.FromStreet = BillboardConstruction.FromStreet;
            mc.Breadth = BillboardConstruction.Breadth;
            mc.StartDate = BillboardConstruction.StartDate;
            mc.EndDate = BillboardConstruction.EndDate;
            mc.Height = BillboardConstruction.Height;
            mc.Comment = BillboardConstruction.Comment;
            mc.ContractNumber = BillboardConstruction.ContractNumber;
            mc.Locality = BillboardConstruction.Locality;
            mc.House1 = BillboardConstruction.House1;
            mc.OnAgreement = BillboardConstruction.OnAgreement;
            mc.PassportNumber = BillboardConstruction.PassportNumber;
            
            ModelState["Owner"].Errors.Clear();
            //if (ModelState.IsValid)
            //{
                context.SaveChanges();
                return RedirectToAction("Bilboard", context.MetalConstructions);
            //}
            //else
            //{
            //    return View(mc);
            //}
        }

    }

    
}