using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WatchShop.Data.ViewModels
{
    public class ItemGioHang
    {
        [DisplayName("Mã đồng hồ")]
        public int MaDH { get; set; }

        [DisplayName("Tên đồng hồ")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string TenDH { get; set; }

        [DisplayName("Giá đã giảm")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ")]
        public int? Gia { get; set; }

        [DisplayName("Ảnh")]
        public string AnhURL { get; set; }

        [DisplayName("Số lượng")]
        public int SoLuong { get; set; }
    }
}