using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchShop.Data.Models;

namespace WatchShop.WebApp.Areas.Admin.Controllers
{
    public class KhachHangsController : Controller
    {
        private WebBanDongHoContext db = new WebBanDongHoContext();

        // GET: Admin/KhachHangs
        public ActionResult Index()
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            return View(db.KhachHangs.Where(kh => kh.Enable == true).ToList());
        }

        // GET: Admin/KhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHangs/Create
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
        public ActionResult Create([Bind(Include = "MaKH,TenDangNhap,MatKhau,HoTen,GioiTinh,Email,SoDT,AnhURL")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                var avatar = Request.Files["Avatar"];
                if (avatar != null && avatar.ContentLength > 0)
                {
                    string extension = Path.GetExtension(avatar.FileName);
                    string path = Server.MapPath("~/Content/img/AvatarKhachHang/" + khachHang.MaKH.ToString() + extension);
                    avatar.SaveAs(path);
                    khachHang.AnhURL = "~/Content/img/AvatarKhachHang/" + khachHang.MaKH.ToString() + extension;
                }
                else
                    khachHang.AnhURL = "~/Content/img/AvatarKhachHang/DefaultAvatar.png";
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khachHang);
        }

        // GET: Admin/KhachHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TenDangNhap,MatKhau,HoTen,GioiTinh,Email,SoDT,AnhURL")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                var avatar = Request.Files["Avatar"];
                if (avatar != null && avatar.ContentLength > 0)
                {
                    // Xoá avatar cũ
                    string path = Server.MapPath(khachHang.AnhURL);
                    if (System.IO.File.Exists(path) && !path.Contains("DefaultAvatar.png"))
                        System.IO.File.Delete(path);
                    // Thêm avatar mới
                    string extension = Path.GetExtension(avatar.FileName);
                    path = Server.MapPath("~/Content/img/AvatarKhachHang/" + khachHang.MaKH.ToString() + extension);
                    avatar.SaveAs(path);
                    khachHang.AnhURL = "~/Content/img/AvatarKhachHang/" + khachHang.MaKH.ToString() + extension;
                }
                else
                {
                    // Xoá avatar cũ
                    string path = Server.MapPath(khachHang.AnhURL);
                    if (System.IO.File.Exists(path) && !path.Contains("DefaultAvatar.png"))
                        System.IO.File.Delete(path);
                    khachHang.AnhURL = "~/Content/img/AvatarKhachHang/DefaultAvatar.png";
                }
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khachHang);
        }

        // GET: Admin/KhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: Admin/KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            //string path = Server.MapPath(khachHang.AnhURL);
            //db.KhachHangs.Remove(khachHang);
            khachHang.Enable = false;
            db.Entry(khachHang).State = EntityState.Modified;
            db.SaveChanges();
            //if (System.IO.File.Exists(path) && !path.Contains("DefaultAvatar.png"))
            //    System.IO.File.Delete(path);
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