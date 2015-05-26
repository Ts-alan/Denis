using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Sciencecom.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sciencecom.Controllers
{
    public class IllegalController : Controller
    {
        private SciencecomEntities db = new SciencecomEntities();


        [Authorize]
        public ViewResult Index()
        {
           
            return View(db.IllegalConstructions.ToList().OrderBy(m=>m.Status).OrderBy(m=>m.DetectionDate));
        } 

        [Authorize(Roles = "Admin, ChiefEditAll, ChiefEditOwn, SupplierEditAll, SupplierEditOwn, IllegalEdit")]
        public ActionResult Create()
        {
            return View();
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, ChiefEditAll, ChiefEditOwn, SupplierEditAll, SupplierEditOwn, IllegalEdit")]
        public ActionResult Create([Bind(Include = "Id,Street1,Street2,FromStreet,DetectionDate,AdditionDate,Locality,NotingDate,SolvingDate,Status,Note,Shirota,Dolgota,file1, file2")] IllegalConstruction illegalconstruction, [Bind(Include = "file1")] HttpPostedFileBase file1, [Bind(Include = "file2")] HttpPostedFileBase file2)
        {
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser user = um.FindByName(User.Identity.GetUserName());
            illegalconstruction.WhoAdded = user.UserName;
            illegalconstruction.WhoLastEdited = user.UserName;
            string pathFile1 = null, pathFile2 = null;
            if (ModelState.IsValid)
            {
                illegalconstruction.AdditionDate = DateTime.Now;
                illegalconstruction.Status = 0;
                db.IllegalConstructions.Add(illegalconstruction);
                db.SaveChanges();
                if (file1 != null)
                {
                    if ((file1.ContentLength > 0) && (file1.ContentLength < 2097152))
                    {
                        string src = "~/Images/Illegal/" + illegalconstruction.Id + "file1.jpg";
                        pathFile1 = Server.MapPath(src);
                    }
                    else
                    {
                        ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 2 МБ");
                    }
                }
                if (file2 != null)
                {
                    if ((file2.ContentLength > 0) && (file2.ContentLength < 2097152))
                    {
                        string src = "~/Images/Illegal/" + illegalconstruction.Id + "file2.jpg";
                        pathFile2 = Server.MapPath(src);
                    }
                    else
                    {
                        ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 2 МБ");
                    }
                }
            }
            if (ModelState.IsValid)
            {
                if (pathFile1 != null)
                {
                    System.IO.File.Delete(pathFile1);
                    file1.SaveAs(pathFile1);
                }
                if (pathFile2 != null)
                {
                    System.IO.File.Delete(pathFile2);
                    file2.SaveAs(pathFile2);
                }
                return RedirectToAction("Index");
            }
            else
            {
                db.IllegalConstructions.Remove(db.IllegalConstructions.Find(illegalconstruction.Id));
                db.SaveChanges();
            }
            return View(illegalconstruction);
        }
     
        [Authorize(Roles = "Admin, IllegalEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IllegalConstruction illegalconstruction = db.IllegalConstructions.Find(id);
            if (illegalconstruction == null)
            {
                return HttpNotFound();
            }
            return View(illegalconstruction);
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, IllegalEdit")]
        public ActionResult Edit([Bind(Include = "Id,Street1,Street2,FromStreet,DetectionDate,AdditionDate,Locality,NotingDate,SolvingDate,Status,Note,Shirota,Dolgota,WhoAdded,WhoLastEdited,WhoTakeNote")] IllegalConstruction illegalconstruction, [Bind(Include = "file1")] HttpPostedFileBase file1, [Bind(Include = "file2")] HttpPostedFileBase file2)
        {
            string pathFile1 = null, pathFile2 = null;
            if (file1 != null)
            {
                if ((file1.ContentLength > 0) && (file1.ContentLength < 2097152))
                {
                    string src = "~/Images/Illegal/" + illegalconstruction.Id + "file1.jpg";
                    pathFile1 = Server.MapPath(src);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 2 МБ");
                }
            }
            if (file2 != null)
            {
                if ((file2.ContentLength > 0) && (file2.ContentLength < 2097152))
                {
                    string src = "~/Images/Illegal/" + illegalconstruction.Id + "file2.jpg";
                    pathFile2 = Server.MapPath(src);
                }
                else
                {
                    ModelState.AddModelError("Photo", "Нельзя загрузить файл объёмом больше 2 МБ");
                }
            }
            if (ModelState.IsValid)
            {
                var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                ApplicationUser user = um.FindByName(User.Identity.GetUserName());
                illegalconstruction.WhoLastEdited = user.UserName;
                db.Entry(illegalconstruction).State = EntityState.Modified;
                db.SaveChanges();
                if (pathFile1 != null)
                {
                    System.IO.File.Delete(pathFile1);
                    file1.SaveAs(pathFile1);
                }
                if (pathFile2 != null)
                {
                    System.IO.File.Delete(pathFile2);
                    file2.SaveAs(pathFile2);
                }
                return RedirectToAction("Index");
            }
            return View(illegalconstruction);
        }

        [Authorize(Roles = "Admin, IllegalEdit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IllegalConstruction illegalconstruction = db.IllegalConstructions.Find(id);
            if (illegalconstruction == null)
            {
                return HttpNotFound();
            }
            return View(illegalconstruction);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, IllegalEdit")]
        public ActionResult DeleteConfirmed(int id)
        {
            IllegalConstruction illegalconstruction = db.IllegalConstructions.Find(id);
            db.IllegalConstructions.Remove(illegalconstruction);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin, IllegalEdit")]
        public ActionResult ChangeStatus(int id, string status)
        {
            var construction = db.IllegalConstructions.Find(id);
            int idStatus = -1;
            switch (status)
            {
                case "Новый": idStatus = 0; break;
                case "Рассматривается": idStatus = 1; break;
                case "Нелегальный": idStatus = 2; break;
                case "Легальный": idStatus = 3; break;
                case "Демонтирован": idStatus = 4; break;
            }
            if ((idStatus != 0) && (idStatus != 1) && (idStatus != 2) && (idStatus != 3) && (idStatus != 4) || (construction == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser user = um.FindByName(User.Identity.GetUserName());
            if (construction.Status == 0)
            { 
                construction.NotingDate = DateTime.Now;
                construction.WhoTakeNote = user.UserName;
            }
            if ((idStatus != 1) && (idStatus != 0))
            {
                construction.SolvingDate = DateTime.Now;
            }
            else
            {
                construction.SolvingDate = null;
            }

            construction.Status = idStatus;

            construction.WhoLastEdited = user.UserName;
            try
            {
               db.SaveChanges();
            }
            catch
            {
                return new HttpStatusCodeResult(500);
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
