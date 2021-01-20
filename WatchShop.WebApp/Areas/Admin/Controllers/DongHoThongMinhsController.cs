using PagedList;
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
    public class DongHoThongMinhsController : Controller
    {
        private WebBanDongHoContext db = new WebBanDongHoContext();

        // GET: Admin/DongHoThongMinhs
        public ActionResult Index()
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }
        public PartialViewResult ContentPartial(int? size, int? page)
        {
            List<SelectListItem> listSize = new List<SelectListItem>();
            listSize.Add(new SelectListItem { Text = "4", Value = "4" });
            listSize.Add(new SelectListItem { Text = "10", Value = "10" });
            listSize.Add(new SelectListItem { Text = "15", Value = "15" });
            listSize.Add(new SelectListItem { Text = "20", Value = "20" });
            foreach (var s in listSize)
            {
                if (s.Value == size.ToString())
                    s.Selected = true;
            }
            ViewBag.Size = listSize;

            var dongHoes = (from dh in db.DongHoes.Include(d => d.ChiTietGiamGia).Include(d => d.DongHoThongMinh).Include(d => d.ThuongHieu)
                            where dh.MaDH == dh.DongHoThongMinh.MaDH && dh.Enable == true
                            select dh).OrderBy(dh => dh.MaDH);
            int pageNum = page ?? 1;
            int pageSize = size ?? Convert.ToInt32(listSize[0].Value); // kích thước trang mặc định
            ViewBag.CurrentSize = pageSize;
            return PartialView("ContentPartial", dongHoes.ToPagedList(pageNum, pageSize));
        }

        // GET: Admin/DongHoThongMinhs/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongHo dongHo = db.DongHoes.Find(id);
            if (dongHo == null)
            {
                return HttpNotFound();
            }
            return View(dongHo);
        }

        // GET: Admin/DongHoThongMinhs/Create
        public ActionResult Create()
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            ViewBag.MaGG = new SelectList(db.ChiTietGiamGias, "MaGG", "MoTa");
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH");
            return View();
        }

        // POST: Admin/DongHoThongMinhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,TenDH,MaTH,GiaGoc,MaGG,GiaBan,SoLuong,MoTa,AnhChinhURL,Anh1URL,Anh2URL,Anh3URL,Anh4URL,DongHoThongMinh")] DongHo dongHo)
        {
            if (ModelState.IsValid)
            {
                dongHo.AnhChinhURL = dongHo.Anh1URL = dongHo.Anh2URL = dongHo.Anh3URL = dongHo.Anh4URL = "~/Content/img/AnhDongHo/UpdatingImage.png";
                db.DongHoes.Add(dongHo);
                db.SaveChanges();
                AddImage(dongHo);
                db.Entry(dongHo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaGG = new SelectList(db.ChiTietGiamGias, "MaGG", "MoTa", dongHo.MaGG);
            ViewBag.MaTH = new SelectList(db.ThuongHieux, "MaTH", "TenTH", dongHo.MaTH);
            return View(dongHo);
        }

        // GET: Admin/DongHoThongMinhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongHo dongHo = db.DongHoes.Find(id);
            if (dongHo == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChiTietGiamGias = new SelectList(db.ChiTietGiamGias, "MaGG", "MoTa", dongHo.MaGG);
            ViewBag.ThuongHieus = new SelectList(db.ThuongHieux, "MaTH", "TenTH", dongHo.MaTH);
            return View(dongHo);
        }

        // POST: Admin/DongHoThongMinhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [ValidateInput(false)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,TenDH,MaTH,GiaGoc,MaGG,GiaBan,SoLuong,MoTa,AnhChinhURL,Anh1URL,Anh2URL,Anh3URL,Anh4URL,DongHoThongMinh")] DongHo dongHo)
        {
            if (ModelState.IsValid)
            {
                AddImage(dongHo);
                db.Entry(dongHo).State = EntityState.Modified;
                db.Entry(dongHo.DongHoThongMinh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ChiTietGiamGias = new SelectList(db.ChiTietGiamGias, "MaGG", "MoTa", dongHo.MaGG);
            ViewBag.ThuongHieus = new SelectList(db.ThuongHieux, "MaTH", "TenTH", dongHo.MaTH);
            return View(dongHo);
        }

        // GET: Admin/DongHoThongMinhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongHo dongHo = db.DongHoes.Find(id);
            if (dongHo == null)
            {
                return HttpNotFound();
            }
            return View(dongHo);
        }

        // POST: Admin/DongHoThongMinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DongHo dongHo = db.DongHoes.Include(dh => dh.DongHoThongMinh).Where(dh => dh.MaDH == id).FirstOrDefault();
            //string pathDirectory = Server.MapPath("~/Content/img/AnhDongHo/" + dongHo.MaDH.ToString());
            //if (Directory.Exists(pathDirectory))
            //    Directory.Delete(pathDirectory, true);
            //db.DongHoes.Remove(dongHo);
            dongHo.Enable = false;
            db.Entry(dongHo).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        private void AddImage(DongHo dongHo)
        {
            string pathDirectory = Server.MapPath("~/Content/img/AnhDongHo/" + dongHo.MaDH.ToString());
            Directory.CreateDirectory(pathDirectory);
            // Thêm hình ảnh
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var image = Request.Files[i];
                if (image != null && image.ContentLength > 0)
                {
                    string extension = Path.GetExtension(image.FileName);
                    image.SaveAs(pathDirectory + "/" + i.ToString() + extension);
                    switch (i)
                    {
                        case 0: dongHo.AnhChinhURL = "~/Content/img/AnhDongHo/" + dongHo.MaDH.ToString() + "/" + i.ToString() + extension; break;
                        case 1: dongHo.Anh1URL = "~/Content/img/AnhDongHo/" + dongHo.MaDH.ToString() + "/" + i.ToString() + extension; break;
                        case 2: dongHo.Anh2URL = "~/Content/img/AnhDongHo/" + dongHo.MaDH.ToString() + "/" + i.ToString() + extension; break;
                        case 3: dongHo.Anh3URL = "~/Content/img/AnhDongHo/" + dongHo.MaDH.ToString() + "/" + i.ToString() + extension; break;
                        case 4: dongHo.Anh4URL = "~/Content/img/AnhDongHo/" + dongHo.MaDH.ToString() + "/" + i.ToString() + extension; break;
                    }
                }
            }
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
