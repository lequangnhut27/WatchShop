using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchShop.Data.Models;

namespace WatchShop.WebApp.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private WebBanDongHoContext db = new WebBanDongHoContext();
        // GET: Admin/Login
        public ActionResult Login()
        {
            if (Session["ManagerId"] != null)
                return RedirectToAction("Index","Manager");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountManager model)
        {
            var account = db.AccountManagers.Where(a => a.Username.Equals(model.Username) && a.Pass.Equals(model.Pass)).FirstOrDefault();
            if (account == null)
            {
                TempData["LoiDangNhap"] = "alert('Sai tên đăng nhập hoặc mật khẩu')";
                return View(model);
            }
            Session["ManagerId"] = account.ManagerId;
            Session["HoTen"] = account.HoTen;
            Session["AnhURL"] = account.AnhURL;
            return RedirectToAction("Index","Manager");
        }

        public ActionResult Logout()
        {
            Session["ManagerId"] = null;
            return RedirectToAction("Login", "Login");
        }

        public ActionResult LoginShipper()
        {
            if (Session["MaShipper"] != null)
                return RedirectToAction("Index", "Shippers");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginShipper(Shipper model)
        {
            var account = db.Shippers.Where(a => a.Username.Equals(model.Username) && a.Pass.Equals(model.Pass)).FirstOrDefault();
            if (account == null)
            {
                TempData["LoiDangNhap"] = "alert('Sai tên đăng nhập hoặc mật khẩu')";
                return View(model);
            }
            Session["MaShipper"] = account.MaShipper;
            Session["HoTen"] = account.HoTen;
            return RedirectToAction("Index", "Shippers");
        }
        public ActionResult LogoutShipper()
        {
            Session["MaShipper"] = null;
            return RedirectToAction("LoginShipper", "Login");
        }

    }
}