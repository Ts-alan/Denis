using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sciencecom.Models;
using Sciencecom.Models.Users;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sciencecom.Controllers
{
    [Authorize(Roles = "Admin, ChiefEditAll, SupplierEditAll")]
    public class OwnersController : Controller
    {
        // GET: /Owners/
        public ActionResult Index()
        {
            SciencecomEntities db = new SciencecomEntities();
            ViewBag.Error = "";
            return View(db.Owners.ToList());
        }

        // GET: /Owners/Details/5
        public ActionResult Details(int? id)
        {
            SciencecomEntities db = new SciencecomEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.Owners.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        // GET: /Owners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Owners/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Name,Address,Telephone")] Owner owner)
        {
            SciencecomEntities db = new SciencecomEntities();
            if (ModelState.IsValid)
            {
                db.Owners.Add(owner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(owner);
        }

        // GET: /Owners/Edit/5
        public ActionResult Edit(int? id)
        {
            SciencecomEntities db = new SciencecomEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.Owners.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            return View(owner);
        }

        // POST: /Owners/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Owner owner, string oldName)
        {
            SciencecomEntities db = new SciencecomEntities();
            if (ModelState.IsValid)
            {
                db.Entry(owner).State = EntityState.Modified;
                db.SaveChanges();
                Entities users = new Entities();
                foreach (var item in users.AspNetUsers)
                {
                    if (item.Company == oldName)
                        item.Company = owner.Name;
                }
                users.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(owner);
        }

        // GET: /Owners/Delete/5
        public ActionResult Delete(int? id)
        {
            SciencecomEntities db = new SciencecomEntities();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Owner owner = db.Owners.Find(id);
            if (owner == null)
            {
                return HttpNotFound();
            }
            
            return View(owner);
        }

        // POST: /Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SciencecomEntities db = new SciencecomEntities();
            try
            {
                Owner owner = db.Owners.Find(id);
                db.Owners.Remove(owner);
                db.SaveChanges();
                Entities users = new Entities();
                foreach (var item in users.AspNetUsers)
                {
                    if (item.Company == owner.Name)
                    {
                        users.AspNetUsers.Remove(item);
                    }
                }
                users.SaveChanges();
                ViewBag.Error = "";
            }
            catch (Exception)
            {
                ViewBag.Error = "Не удалось удалить компанию. Вероятно, существуют принадлежащие ей конструкции";
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            SciencecomEntities db = new SciencecomEntities();
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
