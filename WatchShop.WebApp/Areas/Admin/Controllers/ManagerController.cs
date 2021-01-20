using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchShop.Data.Models;

namespace WatchShop.WebApp.Areas.Admin.Controllers
{
    public class ManagerController : Controller
    {
        private WebBanDongHoContext db = new WebBanDongHoContext();
        // GET: Admin/Manager
        public ActionResult Index()
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login","Login");
            return View(db.HoaDons);
        }

    }
}