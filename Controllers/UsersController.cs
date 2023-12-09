using ShoppingZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingZone.Controllers
{
    
    public class UsersController : Controller
    {
        private ShoppingZoneEntities db = new ShoppingZoneEntities();
        List<Cart> li = new List<Cart>();
        public ActionResult Index()
        {
            if (Session["cart"] != null)
            {
                int x = 0;
                List<Cart> li2 = Session["cart"] as List<Cart>;
                foreach (var item in li2)
                {
                    x += item.bill;
                }
                Session["total"] = x;
                Session["item_count"] = li2.Count();
            }
            var query = db.tbl_Products.ToList();
            return View(query);
        }
        public ActionResult AddtoCart(int id)
        {
            var query = db.tbl_Products.Where(x => x.ProdId == id).SingleOrDefault();
            return View(query);
        }
        [HttpPost]
        public ActionResult AddtoCart(int id, int qty)
        {
            tbl_Products p = db.tbl_Products.Where(x => x.ProdId == id).SingleOrDefault();
            Cart c = new Cart();
            c.proId = id;
            c.proname = p.ProdName;
            c.price = Convert.ToInt32(p.Price);
            c.qty = Convert.ToInt32(qty);
            c.bill = c.price * c.qty;
            if (Session["cart"] == null)
            {
                li.Add(c);
                Session["cart"] = li;
            }
            else
            {
                List<Cart> li2 = Session["cart"] as List<Cart>;
                int flag = 0;
                foreach(var item in li2)
                {
                    if(item.proId == c.proId)
                    {
                        item.qty += c.proId;
                        item.bill += c.bill;
                        flag = 1;
                    }
                }
                if(flag == 0)
                {
                    li2.Add(c);
                }
                Session["cart"] = li2;
            }

            return RedirectToAction("Index");
        }
        public ActionResult remove(int? id)
        {
            if (Session["cart"] == null)
            {
                Session.Remove("total");
                Session.Remove("cart");
            }
            else
            {
                List<Cart> li2 = Session["cart"] as List<Cart>;
                Cart c = li2.Where(x => x.proId == id).SingleOrDefault();
                li2.Remove(c);
                int s = 0;
                foreach(var item in li2)
                {
                    s += item.bill;
                }
                Session["total"] = s;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Search(string search)
        {
            var products = from s in db.tbl_Products
                           select s;
            if(!String.IsNullOrEmpty(search) )
            {
                products = products.Where(s => s.ProdName.Contains(search) || s.ProdDesc.Contains(search));
            }
            return View(products.ToList());
        }
        public ActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(string contact, string address)
        {
            if (ModelState.IsValid)
            {
                List<Cart> li2 = Session["cart"] as List<Cart>;
                tbl_Invoice iv = new tbl_Invoice();
                iv.UserId = Convert.ToInt32(Session["Userid"].ToString());
                iv.InvoiceDate = System.DateTime.Now;
                iv.Bill = (int)Session["total"];
                iv.Payment = "cash";
                db.tbl_Invoice.Add(iv); ;
                db.SaveChanges();

                foreach(var item in li2)
                {
                    tbl_order od = new tbl_order();
                    od.ProdID = item.proId;
                    od.OrderDate = System.DateTime.Now;
                    od.Contact = contact;
                    od.Address = address;
                    od.InvoiceId = iv.InvoiceId;
                    od.Qty = item.qty;
                    od.Price = item.price;
                    od.Total = item.bill;

                    db.tbl_order.Add(od);db.SaveChanges();
                }
                Session.Remove("total");
                Session.Remove("cart");
                return RedirectToAction("OrderPlaced");
            }
            return View();
        }
        [HttpGet]
        public ActionResult OrderPlaced()
        {
            var query = db.tbl_order.ToList();
            return View(query);
        }
        [HttpGet]
        public ActionResult Orders()
        {
            var query = db.tbl_order.ToList().OrderBy(o => o.OrderDate);
            return View(query);
        }
        
    }
}