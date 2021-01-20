using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchShop.Data.Models;

namespace WatchShop.WebApp.Areas.Admin.Controllers
{
    public class ShippersController : Controller
    {
        private WebBanDongHoContext db = new WebBanDongHoContext();
        // GET: Admin/Shippers
        public ActionResult Index()
        {
            if (Session["MaShipper"] == null)
                return RedirectToAction("LoginShipper", "Login");
            int maShipper = (int)Session["MaShipper"];
            var hoadons = from hd in db.HoaDons
                          where hd.MaShipper == maShipper
                          select hd;
            return View(hoadons);
        }

        public ActionResult XacNhanDaGiao(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            hoaDon.MaTTHD = "DA";
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Shippers");
        }

        public ActionResult XacNhanGiaoHangThatBai(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            hoaDon.MaTTHD = "HUY";
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Shippers");
        }
    }
}