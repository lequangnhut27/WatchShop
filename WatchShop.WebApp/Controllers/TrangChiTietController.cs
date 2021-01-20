using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WatchShop.Data.Models;
using WatchShop.Data.ViewModels;

namespace WatchShop.WebApp.Controllers
{
    public class TrangChiTietController : Controller
    {
        private WebBanDongHoContext db = new WebBanDongHoContext();
        // GET: TrangChiTiet
        public ActionResult Details(int? maDH)
        {
            if (maDH == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DongHo dongHo = db.DongHoes.Find(maDH);
            ViewBag.DongHoCungLoai = db.DongHoes.Where(item => item.DongHoThoiTrang.MaDoiTuong == dongHo.DongHoThoiTrang.MaDoiTuong);
            if (dongHo == null)
            {
                return HttpNotFound();
            }
            if (dongHo.DongHoThoiTrang != null)
                return View("DetailsDongHoThoiTrang", dongHo);
            else
                return View("DetailsDongHoThongMinh", dongHo);
        }

        public ActionResult SearchDongHo(string strSearch)
        {
            List<DongHo> dongHos = db.DongHoes.Where(item => item.TenDH.Contains(strSearch) || item.MoTa.Contains(strSearch) || item.ThuongHieu.TenTH.Contains(strSearch)).ToList();
            ViewBag.TitleFilter = "Kết quả tìm kiếm cho " + "\"" + strSearch + "\"";
            return View("Search", dongHos);
        }

        public ActionResult FilterDongHoGiamGia(InfoFilter infoFilter)
        {
            ViewBag.TitleFilter = "Đồng hồ giảm giá";
            foreach (ThuongHieu th in db.ThuongHieux)
                infoFilter.SelectedThuongHieus.Add(new SelectedThuongHieu() { ThuongHieu = th, IsSelected = false });
            infoFilter.isDongHoGiamGia = true;
            return View("Filter", infoFilter);
        }

        public ActionResult FilterDongHoThoiTrang(InfoFilter infoFilter, string doiTuong)
        {
            ViewBag.TitleFilter = "Đồng hồ thời trang";
            List<ThuongHieu> thuongHieus = new List<ThuongHieu>();
            db.DongHoThoiTrangs.ToList().ForEach(item => { if (!thuongHieus.Contains(item.DongHo.ThuongHieu)) thuongHieus.Add(item.DongHo.ThuongHieu); });
            foreach (ThuongHieu th in thuongHieus)
                infoFilter.SelectedThuongHieus.Add(new SelectedThuongHieu() { ThuongHieu = th, IsSelected = false });
            infoFilter.isDongHoThoiTrang = true;
            if (doiTuong == "Nam")
                infoFilter.isNam = true;
            else if (doiTuong == "Nữ")
                infoFilter.isNu = true;
            else if (doiTuong == "Trẻ em")
                infoFilter.isTreEm = true;
            return View("Filter", infoFilter);
        }

        public ActionResult FilterDongHoThongMinh(InfoFilter infoFilter)
        {
            ViewBag.TitleFilter = "Đồng hồ thông minh";
            List<ThuongHieu> thuongHieus = new List<ThuongHieu>();
            db.DongHoThongMinhs.ToList().ForEach(item => { if (!thuongHieus.Contains(item.DongHo.ThuongHieu)) thuongHieus.Add(item.DongHo.ThuongHieu); });
            foreach (ThuongHieu th in thuongHieus)
                infoFilter.SelectedThuongHieus.Add(new SelectedThuongHieu() { ThuongHieu = th, IsSelected = false });
            infoFilter.isDongHoThongMinh = true;
            return View("Filter", infoFilter);
        }

        public ActionResult Filter(InfoFilter infoFilter)
        {
            return View(infoFilter);
        }

        public ActionResult ContentFilter(InfoFilter infoFilter)
        {
            List<DongHo> dongHos = new List<DongHo>();
            if (infoFilter.isDongHoGiamGia)
                dongHos = db.DongHoes.Where(item => item.MaGG != null).ToList();
            else if (infoFilter.isDongHoThoiTrang)
                dongHos = db.DongHoes.Where(item => item.MaDH == item.DongHoThoiTrang.MaDH).ToList();
            else if (infoFilter.isDongHoThongMinh)
                dongHos = db.DongHoes.Where(item => item.MaDH == item.DongHoThongMinh.MaDH).ToList();
            int count = 0;
            infoFilter.SelectedThuongHieus.ForEach(sth => { if (sth.IsSelected) count++; });
            if (count > 0)
                for (int i = dongHos.Count-1; i >= 0; i--)
                {
                    if (infoFilter.SelectedThuongHieus.Find(sTH => sTH.ThuongHieu.MaTH == dongHos[i].MaTH && sTH.IsSelected == true) == null)
                        dongHos.RemoveAt(i);
                }
            if (!(infoFilter.isNam == false && infoFilter.isNu == false && infoFilter.isTreEm == false))
            {
                for (int i = dongHos.Count - 1; i >= 0; i--)
                {
                    bool condition = (infoFilter.isNam && dongHos[i].DongHoThoiTrang.MaDoiTuong == "MALE") ||
                                     (infoFilter.isNu && dongHos[i].DongHoThoiTrang.MaDoiTuong == "FEMALE") ||
                                     (infoFilter.isTreEm && dongHos[i].DongHoThoiTrang.MaDoiTuong == "CHILD");
                    if (!condition)
                        dongHos.RemoveAt(i);
                }
            }
            if (!(infoFilter.MinPrice == 0 && infoFilter.MaxPrice == 0))
            {
                for (int i = dongHos.Count - 1; i >= 0; i--)
                {
                    if (dongHos[i].GiaBan < infoFilter.MinPrice || dongHos[i].GiaBan > infoFilter.MaxPrice )
                        dongHos.RemoveAt(i);
                }
            }
            return PartialView(dongHos);
        }
    }
}