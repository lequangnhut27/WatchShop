using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WatchShop.Data.Models;

namespace WatchShop.WebApp.Areas.Admin.Controllers
{
    public class ThuongHieuController : Controller
    {
        WebBanDongHoContext db = new WebBanDongHoContext();
        // GET: Admin/ThuongHieu
        public ActionResult Index()
        {
            if (Session["ManagerId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }    
            return View(db.ThuongHieux);
        }

        public ActionResult Create()
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        // POST: Admin/KhachHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTH,TenTH,QuocGia,AnhURL")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                thuongHieu.AnhURL = "~/Content/img/AnhThuongHieu/Xiaomi.png";
                db.ThuongHieux.Add(thuongHieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(thuongHieu);
        }
    }
}