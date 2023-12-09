using ShoppingZone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShoppingZone.Controllers
{
    public class AccountsController : Controller
    {
        private ShoppingZoneEntities db = new ShoppingZoneEntities();
        // GET: Accounts
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(tbl_UserRegister ur)
        {
            if(db.tbl_UserRegister.Any(x => x.Email == ur.Email))
            {
                ViewBag.Msg = "Email already registered";
            }
            
            else
            {
                db.tbl_UserRegister.Add(ur);
                db.SaveChanges();
                Response.Write("<script>alert('Registered Successfully.')</script>");
            }
            return View();
        }
        public ActionResult Login()
        {
            if(User.Identity.IsAuthenticated)
            {

                return RedirectToAction("Index", "USers");
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserLogin ul)
        {
            var validlog = db.tbl_UserRegister.SingleOrDefault(x => x.Email == ul.Email && x.Password == ul.Password);
            if (validlog != null)
            {
               
                Session["Userid"] = validlog.UserId;
                FormsAuthentication.SetAuthCookie(validlog.Email, false);

                //Session["username"]= ur.Name.ToString();
                return RedirectToAction("Index","Users");
            }
            else if (ul.Email == "admin@gmail.com" && ul.Password == "admin@123")
            {
                Session["AdminId"] = 108;
                FormsAuthentication.SetAuthCookie(validlog.Email, false);
                return RedirectToAction("Index", "Products");
            }
            else
            {
                ViewBag.LoginErrMsg = "Invalid Credentials.";
            }
            return View();
        }
        public ActionResult Signout()
        {
            //Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Accounts");
        }
    }
}