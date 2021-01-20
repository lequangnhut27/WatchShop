using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WatchShop.Data.Models;

namespace WatchShop.WebApp.Areas.Admin.Controllers
{
    public class HoaDonsController : Controller
    {
        private WebBanDongHoContext db = new WebBanDongHoContext();

        // GET: Admin/HoaDons
        public ActionResult Index()
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            var hoaDons = db.HoaDons.Include(h => h.HinhThucThanhToan).Include(h => h.KhachHang).Include(h => h.TinhTrangHoaDon).Include(h => h.ChiTietHoaDons);
            return View(hoaDons.ToList());
        }

        public ActionResult XacNhanHoaDon(int? id)
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
            hoaDon.MaTTHD = "XULY";
            hoaDon.MaShipper = ChonShipper();
            if (ModelState.IsValid)
            {
                db.Entry(hoaDon).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "HoaDons");
        }

        public int ChonShipper()
        {
            int min = db.Shippers.Min(sp => sp.HoaDons.Where(hd => hd.MaTTHD == "XULY").Count());
            Shipper shipper = db.Shippers.Where(sp => sp.HoaDons.Where(hd => hd.MaTTHD == "XULY").Count() == min).FirstOrDefault();
            return shipper.MaShipper;
        }

        public ActionResult Details(int? id)
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
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Create
        public ActionResult Create()
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        public ActionResult ContentCreatePartial(HoaDon hoaDon, int addMaDH = 0, int soLuong = 0, int delMaDH = 0)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            // Xử lý thêm đồng hồ vào view
            if (addMaDH != 0 && soLuong != 0)
            {
                DongHo dh = db.DongHoes.Find(addMaDH);
                ChiTietHoaDon cthd = new ChiTietHoaDon() { MaHD = hoaDon.MaHD, MaDH = addMaDH, SoLuong = soLuong, DongHo = dh };
                if (hoaDon.ChiTietHoaDons.Find(item => item.MaDH == cthd.MaDH) == null)
                    hoaDon.ChiTietHoaDons.Add(cthd);
                else
                    hoaDon.ChiTietHoaDons.Find(item => item.MaDH == cthd.MaDH).SoLuong += cthd.SoLuong;
            }
            // Xử lý xoá đồng hồ khỏi view
            if (delMaDH != 0)
            {
                ChiTietHoaDon delCthd = hoaDon.ChiTietHoaDons.Find(item => item.MaHD == hoaDon.MaHD && item.MaDH == delMaDH);
                hoaDon.ChiTietHoaDons.Remove(delCthd);
            }
            int? tongGTHD = 0;
            foreach (ChiTietHoaDon item in hoaDon.ChiTietHoaDons)
            {
                tongGTHD += item.DongHo.GiaBan * item.SoLuong;
            }
            hoaDon.TongGiaTriHD = tongGTHD;
            ViewBag.HTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "MoTa", hoaDon.MaHTTT);
            ViewBag.KhachHang = new SelectList(db.KhachHangs, "MaKH", "HoTen", hoaDon.MaKH);
            ViewBag.DongHoes = db.DongHoes;
            ModelState.Clear();
            return PartialView(hoaDon);
        }

        // POST: Admin/HoaDons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,DiaChiGiaoHang,NgayGiaoDuKien,MaHTTT,TongGiaTriHD,ChiTietHoaDons")] HoaDon hoaDon)
        {
            hoaDon.MaTTHD = "CHUA";
            List<ChiTietHoaDon> listCthd = hoaDon.ChiTietHoaDons.ToList();
            hoaDon.ChiTietHoaDons = null;
            hoaDon.NgayLapHD = DateTime.Now;
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                db.HoaDons.Add(hoaDon);
                db.SaveChanges();
                foreach (ChiTietHoaDon item in listCthd)
                    db.ChiTietHoaDons.Add(new ChiTietHoaDon() { MaHD = hoaDon.MaHD, MaDH = item.MaDH, SoLuong = item.SoLuong });
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "MoTa", hoaDon.MaHTTT);
            ViewBag.KhachHang = new SelectList(db.KhachHangs, "MaKH", "HoTen", hoaDon.MaKH);
            ViewBag.DongHoes = db.DongHoes;
            return View(hoaDon);
        }

        // GET: Admin/HoaDons/Delete/5
        public ActionResult Cancel(int? id)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HoaDon hoaDon = db.HoaDons.Find(id);
            if (hoaDon == null)
            {
                return HttpNotFound();
            }
            return View(hoaDon);
        }

        // POST: Admin/HoaDons/Delete/5
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelConfirmed(int id)
        {
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
