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
    public class AccountManagersController : Controller
    {
        private WebBanDongHoContext db = new WebBanDongHoContext();

        // GET: Admin/AccountManagers
        public ActionResult Index()
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            return View(db.AccountManagers.ToList());
        }

        // GET: Admin/AccountManagers/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountManager accountManager = db.AccountManagers.Find(id);
            if (accountManager == null)
            {
                return HttpNotFound();
            }
            return View(accountManager);
        }

        // GET: Admin/AccountManagers/Create
        public ActionResult Create()
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            return View();
        }

        // POST: Admin/AccountManagers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ManagerId,Username,Pass,HoTen,NgaySinh,GioiTinh,Email,SoDT,AnhURL")] AccountManager accountManager)
        {
            if (ModelState.IsValid)
            {
                db.AccountManagers.Add(accountManager);
                db.SaveChanges();
                var avatar = Request.Files["Avatar"];
                if (avatar != null && avatar.ContentLength > 0)
                {
                    string extension = Path.GetExtension(avatar.FileName);
                    string path = Server.MapPath("~/Areas/Admin/Content/img/AvatarAccountManager/" + accountManager.ManagerId.ToString() + extension);
                    avatar.SaveAs(path);
                    accountManager.AnhURL = "~/Areas/Admin/Content/img/AvatarAccountManager/" + accountManager.ManagerId.ToString() + extension;
                }
                else
                    accountManager.AnhURL = "~/Areas/Admin/Content/img/AvatarAccountManager/DefaultAvatar.png";
                db.Entry(accountManager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountManager);
        }

        // GET: Admin/AccountManagers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountManager accountManager = db.AccountManagers.Find(id);
            if (accountManager == null)
            {
                return HttpNotFound();
            }
            return View(accountManager);
        }

        // POST: Admin/AccountManagers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ManagerId,Username,Pass,HoTen,NgaySinh,GioiTinh,Email,SoDT,AnhURL")] AccountManager accountManager)
        {
            if (ModelState.IsValid)
            {
                var avatar = Request.Files["Avatar"];
                if (avatar != null && avatar.ContentLength > 0)
                {
                    // Xoá avatar cũ
                    string path = Server.MapPath(accountManager.AnhURL);
                    if (System.IO.File.Exists(path) && !path.Contains("DefaultAvatar.png"))
                        System.IO.File.Delete(path);
                    // Thêm avatar mới
                    string extension = Path.GetExtension(avatar.FileName);
                    path = Server.MapPath("~/Areas/Admin/Content/img/AvatarAccountManager/" + accountManager.ManagerId.ToString() + extension);
                    avatar.SaveAs(path);
                    accountManager.AnhURL = "~/Areas/Admin/Content/img/AvatarAccountManager/" + accountManager.ManagerId.ToString() + extension;
                }
                else
                {
                    // Xoá avatar cũ
                    string path = Server.MapPath(accountManager.AnhURL);
                    if (System.IO.File.Exists(path) && !path.Contains("DefaultAvatar.png"))
                        System.IO.File.Delete(path);
                    accountManager.AnhURL = "~/Areas/Admin/Content/img/AvatarAccountManager/DefaultAvatar.png";
                }
                db.Entry(accountManager).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountManager);
        }

        // GET: Admin/AccountManagers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ManagerId"] == null)
                return RedirectToAction("Login", "Login");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountManager accountManager = db.AccountManagers.Find(id);
            if (accountManager == null)
            {
                return HttpNotFound();
            }
            return View(accountManager);
        }

        // POST: Admin/AccountManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountManager accountManager = db.AccountManagers.Find(id);
            string path = Server.MapPath(accountManager.AnhURL);
            db.AccountManagers.Remove(accountManager);
            db.SaveChanges();
            if (System.IO.File.Exists(path) && !path.Contains("DefaultAvatar.png"))
                System.IO.File.Delete(path);
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
