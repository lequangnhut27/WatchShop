using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WatchShop.WebApp.Helper;
using WatchShop.Data.Models;
using WatchShop.Data.ViewModels;

namespace WatchShop.WebApp.Controllers
{
    public class GioHangController : Controller
    {
        private WebBanDongHoContext db = new WebBanDongHoContext();
        private PayPal.Api.Payment payment;
        // GET: GioHang
        public ActionResult GioHang()
        {
            if (Session["GioHang"] == null || Session["MaKH"] == null)
                return RedirectToAction("Login", "Home");
            return View();
        }

        public ActionResult ContentGioHangPartial(int maDH = 0, int soLuong = 0, int delMaDH = 0)
        {
            if (Session["GioHang"] == null || Session["MaKH"] == null)
                return RedirectToAction("Login", "Home");
            GioHang gioHang = Session["GioHang"] as GioHang;
            // Xử lý cập nhật số lượng đồng hồ
            if (gioHang.ListItems.Find(i => i.MaDH == maDH) != null)
                gioHang.ListItems.Find(i => i.MaDH == maDH).SoLuong = soLuong;
            // Xử lý xoá đồng hồ khỏi giỏ hàng
            ItemGioHang delItem = gioHang.ListItems.Find(i => i.MaDH == delMaDH);
            if (delItem != null)
                gioHang.ListItems.Remove(delItem);
            return PartialView();
        }

        public string AddGioHang(int addMaDH = 0, int soLuong = 0)
        {
            if (addMaDH != 0)
            {
                DongHo dongHo = db.DongHoes.Find(addMaDH);
                ItemGioHang item = new ItemGioHang()
                {
                    MaDH = dongHo.MaDH,
                    TenDH = dongHo.TenDH,
                    Gia = dongHo.GiaBan,
                    AnhURL = dongHo.AnhChinhURL,
                    SoLuong = soLuong
                };
                GioHang gioHang = Session["GioHang"] as GioHang;
                if (gioHang.ListItems.Find(i => i.MaDH == item.MaDH) == null)
                    gioHang.ListItems.Add(item);
                else
                    gioHang.ListItems.Find(i => i.MaDH == item.MaDH).SoLuong += soLuong;
            }
            else
            {
                if (Session["GioHang"] == null)
                    return "0";
            }
            int tongSoLuong = 0;
            foreach (var item in ((GioHang)Session["GioHang"]).ListItems)
            {
                tongSoLuong += item.SoLuong;
            }
            return tongSoLuong.ToString(); ;
        }

        public ActionResult ThanhToan()
        {
            if (Session["GioHang"] == null || Session["MaKH"] == null)
                return RedirectToAction("Login", "Home");
            GioHang gioHang = Session["GioHang"] as GioHang;
            List<ChiTietHoaDon> listCthd = new List<ChiTietHoaDon>();
            gioHang.ListItems.ForEach(item => { listCthd.Add(new ChiTietHoaDon() { MaDH = item.MaDH, SoLuong = item.SoLuong, Gia = db.DongHoes.Where(x => x.MaDH == item.MaDH).FirstOrDefault().GiaBan }); });
            KhachHang khachHang = db.KhachHangs.Find(gioHang.MaKH);
            HoaDon hoaDon = new HoaDon()
            {
                MaKH = gioHang.MaKH,
                HoTen = khachHang.HoTen,
                SoDT = khachHang.SoDT,
                NgayGiaoDuKien = DateTime.Now.AddDays(7),
                TongGiaTriHD = gioHang.TongGTHD,
                ChiTietHoaDons = listCthd
            };
            ViewBag.HTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "MoTa", hoaDon.MaHTTT);
            return View(hoaDon);
        }

        [HttpPost]
        public ActionResult ThanhToan(HoaDon hoaDon)
        {
            hoaDon.MaTTHD = "CHUA";
            List<ChiTietHoaDon> listCthd = hoaDon.ChiTietHoaDons.ToList();
            hoaDon.ChiTietHoaDons = null;
            hoaDon.NgayLapHD = DateTime.Now;
            hoaDon.NgayGiaoDuKien = hoaDon.NgayLapHD.AddDays(7);
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                if (hoaDon.MaHTTT == "BANK")
                {
                    TempData["hoaDon"] = hoaDon;
                    TempData["listCthd"] = listCthd;
                    return RedirectToAction("PaymentWithPaypal");
                }
                else
                {
                    db.HoaDons.Add(hoaDon);
                    db.SaveChanges();
                    foreach (ChiTietHoaDon item in listCthd)
                        db.ChiTietHoaDons.Add(new ChiTietHoaDon() { MaHD = hoaDon.MaHD, MaDH = item.MaDH, SoLuong = item.SoLuong });
                    db.SaveChanges();
                    ((GioHang)Session["GioHang"]).ListItems.Clear();
                    TempData["ThanhToanMess"] = "alert('Đặt hàng thành công')";
                    return RedirectToAction("Index", "Home");
                }
            }
            ViewBag.HTTT = new SelectList(db.HinhThucThanhToans, "MaHTTT", "MoTa", hoaDon.MaHTTT);
            return View(hoaDon);
        }

        public ActionResult PaymentWithPaypal()
        {
            HoaDon hoaDon = TempData["hoaDon"] as HoaDon;
            List<ChiTietHoaDon> listCthd = TempData["listCthd"] as List<ChiTietHoaDon>;
            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist
                    //it is returned by the create function call of the payment class

                    // Creating a payment
                    // baseURL is the url on which paypal sendsback the data.
                    // So we have provided URL of this controller only
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/GioHang/PaymentWithPayPal?";

                    //guid we are generating for storing the paymentID received in session
                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url
                    //on which payer is redirected for paypal account payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid, listCthd);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        TempData["ThanhToanMess"] = "alert('Đặt hàng thất bại')";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ThanhToanMess"] = "alert('Đặt hàng thất bại')";
                return RedirectToAction("Index", "Home");
            }
            db.HoaDons.Add(hoaDon);
            db.SaveChanges();
            foreach (ChiTietHoaDon item in listCthd)
                db.ChiTietHoaDons.Add(new ChiTietHoaDon() { MaHD = hoaDon.MaHD, MaDH = item.MaDH, SoLuong = item.SoLuong });
            db.SaveChanges();
            ((GioHang)Session["GioHang"]).ListItems.Clear();
            TempData["ThanhToanMess"] = "alert('Đặt hàng thành công')";
            return RedirectToAction("Index", "Home");
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl, List<ChiTietHoaDon> listCthd)
        {

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            int? tong = 0;
            foreach (var sp in listCthd)
            {
                itemList.items.Add(new Item()
                {
                    name = db.DongHoes.Find(sp.MaDH).TenDH,
                    currency = "USD",
                    price = (db.DongHoes.Find(sp.MaDH).GiaBan / 23000).ToString(),
                    quantity = sp.SoLuong.ToString(),
                    sku = "sku"
                });
                tong += (db.DongHoes.Find(sp.MaDH).GiaBan / 23000) * sp.SoLuong;
            }

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "0",
                shipping = "1",
                subtotal = tong.ToString()
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = (tong + 1).ToString(), // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = new Random().Next(100000).ToString(),
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);
        }

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private float TinhGiaTriDonHang(int gtdh, string maGiamGia)
        {
            ChiTietGiamGia ctgg = db.ChiTietGiamGias
                .Where(x => x.MaGG == maGiamGia)
                .FirstOrDefault();
            if (ctgg != null)
            {
                if (ctgg.MaLoaiGG == "PERCENT")
                    return gtdh * ctgg.SoLieuGiamGia / 100;
                else if (ctgg.MaLoaiGG == "DIRECT")
                    return gtdh - ctgg.SoLieuGiamGia;
            }
            return gtdh;
        }

        private bool KiemTraKhoHang(int maDongHo, int soLuong)
        {
            int slKho = db.DongHoes
                .Where(x => x.MaDH == maDongHo)
                .FirstOrDefault()
                .SoLuong;
            if (soLuong < slKho)
                return true;
            return false;
        }

        private bool KiemTraDangNhap(string username, string pass)
        {
            KhachHang khachHang = db.KhachHangs
                .Where(x => x.TenDangNhap == username && x.MatKhau == pass)
                .FirstOrDefault();
            if (khachHang != null)
                return true;
            else
                return false;
        }
    }
}