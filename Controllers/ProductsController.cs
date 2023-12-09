using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingZone.Models;

namespace ShoppingZone.Controllers
{
    public class ProductsController : Controller
    {
        private ShoppingZoneEntities db = new ShoppingZoneEntities();

        // GET: Products
        public ActionResult Index()
        {
            //var tbl_Products = db.tbl_Products.Include(t => t.tbl_Categories);
            return View(db.tbl_Products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Products tbl_Products = db.tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Products);
        }

        public ActionResult Create()
        {
            List<tbl_Categories> categories = db.tbl_Categories.ToList();
            ViewBag.catList = new SelectList(categories, "CatId", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Create(tbl_Products p, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                List<tbl_Categories> categories = db.tbl_Categories.ToList();
                ViewBag.catList = new SelectList(categories, "CatId", "Name");
                tbl_Products product = new tbl_Products();
                product.ProdId = p.ProdId;
                product.ProdName = p.ProdName;
                product.ProdDesc = p.ProdDesc;
                product.Price = p.Price;
                product.Image = Image.FileName.ToString();
                product.CatId = p.CatId;

                var folder = Server.MapPath("~/Uploads/");
                Image.SaveAs(Path.Combine(folder, Image.FileName.ToString()));
                db.tbl_Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                ViewBag.ProductErrMsg = "Product not added";
            }
            return View();
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Products tbl_Products = db.tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CatId = new SelectList(db.tbl_Categories, "CatId", "Name", tbl_Products.CatId);
            return View(tbl_Products);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdId,ProdName,ProdDesc,Price,Image,CatId")] tbl_Products tbl_Products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbl_Products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CatId = new SelectList(db.tbl_Categories, "CatId", "Name", tbl_Products.CatId);
            return View(tbl_Products);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tbl_Products tbl_Products = db.tbl_Products.Find(id);
            if (tbl_Products == null)
            {
                return HttpNotFound();
            }
            return View(tbl_Products);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tbl_Products tbl_Products = db.tbl_Products.Find(id);
            db.tbl_Products.Remove(tbl_Products);
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
