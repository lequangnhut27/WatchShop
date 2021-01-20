namespace WatchShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DongHoThoiTrang")]
    public partial class DongHoThoiTrang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDH { get; set; }

        [StringLength(100)]
        [DisplayName("Loại máy")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string LoaiMay { get; set; }

        [DisplayName("Đường kính mặt")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật", DataFormatString = "{0} mm")]
        public float? DuongKinhMat { get; set; }

        [StringLength(100)]
        [DisplayName("Chất liệu mặt kính")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string ChatLieuMatKinh { get; set; }

        [StringLength(100)]
        [DisplayName("Chất liệu khung viền")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string ChatLieuKhungVien { get; set; }

        [DisplayName("Độ dày mặt")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật", DataFormatString = "Dày {0} mm")]
        public float? DoDayMat { get; set; }

        [StringLength(100)]
        [DisplayName("Chất liệu dây")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string ChatLieuDay { get; set; }

        [DisplayName("Độ rộng dây")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật", DataFormatString = "{0} mm")]
        public float? DoRongDay { get; set; }

        [DisplayName("Thay được dây")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public bool? ThayDuocDay { get; set; }

        [StringLength(100)]
        [DisplayName("Tiện ích")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string TienIch { get; set; }

        [StringLength(100)]
        [DisplayName("Chống nước")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string ChongNuoc { get; set; }

        [StringLength(100)]
        [DisplayName("Nguồn năng lượng")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string NguonNangLuong { get; set; }

        [StringLength(10)]
        [DisplayName("Mã đối tượng sử dụng")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string MaDoiTuong { get; set; }

        [StringLength(100)]
        [DisplayName("Nơi sản xuất")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string NoiSanXuat { get; set; }

        public virtual DoiTuong DoiTuong { get; set; }

        public virtual DongHo DongHo { get; set; }
    }
}
