using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingZone.Models;

namespace ShoppingZone.Controllers
{
    public class CategoriesController : Controller
    {
        private ShoppingZoneEntities db = new ShoppingZoneEntities();

        // GET: Categories
        public ActionResult Index()
        {
            return View(db.tbl_Categories.ToList());
        }

        // GET: Categories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Categories tbl_Categories = db.tbl_Categories.Find(id);
            if (tbl_Categories == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Categories);
        }

        // GET: Categories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tbl_Categories c)
        {
            if (ModelState.IsValid)
            {
                tbl_Categories categories = new tbl_Categories();
                categories.Name = c.Name;
                db.tbl_Categories.Add(categories);
                db.SaveChanges();
                return RedirectToAction("Index");   
            }
            return View();
        }

        // GET: Categories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Categories tbl_Categories = db.tbl_Categories.Find(id);
            if (tbl_Categories == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Categories);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CatId,Name")] tbl_Categories tbl_Categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbl_Categories);
        }

        // GET: Categories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Categories tbl_Categories = db.tbl_Categories.Find(id);
            if (tbl_Categories == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Categories);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Categories tbl_Categories = db.tbl_Categories.Find(id);
            db.tbl_Categories.Remove(tbl_Categories);
            db.SaveChanges();
            return RedirectToAction("Index");
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
