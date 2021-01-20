using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WatchShop.Data.ViewModels
{
    public class GioHang
    {
        [DisplayName("Mã khách hàng")]
        public int MaKH { get; set; }

        //[DisplayName("Họ và tên")]
        //public string HoTen { get; set; }

        //[DisplayName("Số điện thoại")]
        //public string SoDT { get; set; }

        public List<ItemGioHang> ListItems { get; set; }

        [DisplayName("Tổng hoá đơn")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ")]
        public int? TongGTHD
        {
            get
            {
                int? tong = 0;
                ListItems.ForEach(item => { tong += item.Gia * item.SoLuong; });
                return tong;
            }
        }

    }
}