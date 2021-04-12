using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WatchShop.Application.Services;
using WatchShop.Data.Models;
using WatchShop.Data.ViewModels;

namespace WatchShop.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private WebBanDongHoContext db = new WebBanDongHoContext();
        private readonly IDongHoService _dongHoService;

        public HomeController(IDongHoService dongHoService)
        {
            _dongHoService = dongHoService;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public ActionResult Login(KhachHang model)
        {
            var khachHang = db.KhachHangs.Where(kh => kh.TenDangNhap == model.TenDangNhap && kh.MatKhau == model.MatKhau).FirstOrDefault();
            if (khachHang != null)
            {
                Session["MaKH"] = khachHang.MaKH;
                Session["HoTenKH"] = khachHang.HoTen;
                Session["AnhURLKH"] = khachHang.AnhURL;
                Session["GioHang"] = new GioHang()
                {
                    MaKH = khachHang.MaKH,
                    //HoTen = khachHang.HoTen,
                    ListItems = new List<ItemGioHang>(),
                    //SoDT = khachHang.SoDT
                };
                return RedirectToAction("Index", "Home");
            }
            TempData["LoiDangNhap"] = "alert('Sai tên đăng nhập hoặc mật khẩu')";
            return View(khachHang);
        }

        public void LoginExternal(string ReturnUrl = "/", string type = "")
        {
            //if (!Request.IsAuthenticated)
            //{
                if (type == "Google")
                {
                    HttpContext.GetOwinContext().Authentication.Challenge(new AuthenticationProperties { RedirectUri = "Home/GoogleLoginCallback" }, "Google");
                }
            //}
        }

        [AllowAnonymous]
        public ActionResult GoogleLoginCallback()
        {
            var claimsPrincipal = HttpContext.User.Identity as ClaimsIdentity;

            string email = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            KhachHang khachHang = db.KhachHangs.Where(x => x.Email == email).FirstOrDefault();
            if (khachHang == null)
            {
                string name = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                khachHang = new KhachHang()
                {
                    HoTen = name,
                    Email = email,
                    Enable = true,
                    AnhURL = "~/Content/img/AvatarKhachHang/DefaultAvatar.png"
                };
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                khachHang = db.KhachHangs.Where(x => x.Email == email).FirstOrDefault();
            }

            Session["MaKH"] = khachHang.MaKH;
            Session["HoTenKH"] = khachHang.HoTen;
            Session["AnhURLKH"] = khachHang.AnhURL;
            Session["GioHang"] = new GioHang()
            {
                MaKH = khachHang.MaKH,
                //HoTen = khachHang.HoTen,
                ListItems = new List<ItemGioHang>(),
                //SoDT = khachHang.SoDT
            };
            return RedirectToAction("Index", "Home");

        }

        public ActionResult Logout()
        {
            Session["MaKH"] = null;
            Session["HoTenKH"] = null;
            Session["AnhURLKH"] = null;
            Session["GioHang"] = null;
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        //public JsonResult IsTenDangNhapExist(string tenDangNhap)
        //{
        //    bool isExist = db.KhachHangs.ToList().Find(kh => kh.TenDangNhap == tenDangNhap) != null;
        //    return Json(!isExist, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult Register(RegisterKhachHangView registerKhachHangView)
        {
            KhachHang khachHang = new KhachHang()
            {
                TenDangNhap = registerKhachHangView.TenDangNhap,
                HoTen = registerKhachHangView.HoTen,
                GioiTinh = registerKhachHangView.GioiTinh,
                MatKhau = registerKhachHangView.MatKhau,
                SoDT = registerKhachHangView.SoDT,
                Email = registerKhachHangView.Email,
                AnhURL = registerKhachHangView.AnhURL,
            };

            if (ModelState.IsValid)
            {
                Session["KhachHang"] = khachHang;
                Session["Avatar"] = Request.Files["Avatar"];
                string confirmKey = new Random().Next(100000, 1000000).ToString();
                Session[khachHang.Email] = confirmKey;
                SendEmail(khachHang.Email, confirmKey);
                return RedirectToAction("ConfirmAccount", "Home");
            }
            return View(khachHang);
        }

        public ActionResult ConfirmAccount()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConfirmAccount(string confirmKey)
        {
            KhachHang khachHang = (KhachHang)Session["KhachHang"];
            HttpPostedFileBase avatar = (HttpPostedFileBase)Session["Avatar"];
            if (confirmKey == (string)Session[khachHang.Email])
            {
                Session[khachHang.Email] = null;
                Session["KhachHang"] = null;
                Session["Avatar"] = null;
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
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
                TempData["DangKyMess"] = "alert('Tạo tài khoản thành công')";
                return RedirectToAction("Login", "Home");
            }
            TempData["DangKyMess"] = "alert('Sai mã xác thực')";
            return RedirectToAction("ConfirmAccount", "Home");
        }

        public void SendEmail(string address, string confirmKey)
        {
            string email = "gamefreetgdd1@gmail.com";
            string password = "binhduong99";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(address));
            msg.Subject = "Mã xác thực Shop NN";
            msg.Body = confirmKey;
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }


        public ActionResult UpdateThongTinKhachHang()
        {
            if (Session["MaKH"] == null)
                return RedirectToAction("Login", "Home");
            KhachHang khachHang = db.KhachHangs.Find(int.Parse(Session["MaKH"].ToString()));
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }



        [HttpPost]
        public ActionResult UpdateThongTinKhachHang(KhachHang khachHang)
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
                Session["MaKH"] = khachHang.MaKH;
                Session["HoTenKH"] = khachHang.HoTen;
                Session["AnhURLKH"] = khachHang.AnhURL;
                TempData["CapNhatMess"] = "alert('Cập nhật thành công')";
                return View(khachHang);
            }
            return View(khachHang);
        }

        public PartialViewResult DongHoGiamGiaPartial(List<DongHo> dongHos)
        {
            //dongHos = db.DongHoes.ToList();
            dongHos = _dongHoService.GetDongHoGiamGia();
            return PartialView(dongHos);
        }

        public PartialViewResult DongHoThoiTrangPartial(List<DongHo> dongHos)
        {
            //dongHos = db.DongHoes.Where(dh => dh.MaDH == dh.DongHoThoiTrang.MaDH).ToList();
            dongHos = _dongHoService.GetDongHoThoiTrang();
            return PartialView(dongHos);
        }

        public PartialViewResult DongHoThongMinhPartial(List<DongHo> dongHos)
        {
            //dongHos = db.DongHoes.Where(dh => dh.MaDH == dh.DongHoThongMinh.MaDH).ToList();
            dongHos = _dongHoService.GetDongHoThongMinh();
            return PartialView(dongHos);
        }

        public PartialViewResult CardPartial(DongHo dongHo)
        {
            return PartialView(dongHo);
        }

        public ActionResult XemDonHang()
        {
            if (Session["MaKH"] == null)
                return RedirectToAction("Login", "Home");
            KhachHang khachHang = db.KhachHangs.Find(int.Parse(Session["MaKH"].ToString()));
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang.HoaDons);
        }

        public ActionResult HuyDonHang(int? id)
        {
            if (Session["MaKH"] == null)
                return RedirectToAction("Login", "Home");
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
                TempData["ThongBao"] = "alert('Huỷ đơn hàng thành công!')";
            }
            return RedirectToAction("XemDonHang");
        }
    }
}