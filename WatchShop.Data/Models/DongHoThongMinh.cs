namespace WatchShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DongHoThongMinh")]
    public partial class DongHoThongMinh
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDH { get; set; }

        [StringLength(100)]
        [DisplayName("Công nghệ màn hình")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string CongNgheManHinh { get; set; }

        [DisplayName("Kích thước màn hình")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật", DataFormatString = "{0} inch")]
        public float? KichThuocManHinh { get; set; }

        [StringLength(100)]
        [DisplayName("Độ phân giải")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string DoPhanGiai { get; set; }

        [StringLength(100)]
        [DisplayName("Chất liệu mặt")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string ChatLieuMat { get; set; }

        [DisplayName("Đường kính mặt")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật", DataFormatString = "{0} mm")]
        public float? DuongKinhMat { get; set; }

        [StringLength(20)]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string CPU { get; set; }

        [StringLength(100)]
        [DisplayName("Bộ nhớ trong")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string BoNhoTrong { get; set; }

        [StringLength(100)]
        [DisplayName("Hệ điều hành")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string HeDieuHanh { get; set; }

        [StringLength(100)]
        [DisplayName("Kết nối được với hệ điều hành")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string KetNoiHDH { get; set; }

        [StringLength(100)]
        [DisplayName("Cổng sạc")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string CongSac { get; set; }

        [StringLength(100)]
        [DisplayName("Thời gian sử dụng pin")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string ThoiGianSDPin { get; set; }

        [StringLength(100)]
        [DisplayName("Thời gian sạc")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string ThoiGianSac { get; set; }

        [DisplayName("Dung lượng pin")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật", DataFormatString = "{0:N0} mAh")]
        public float? DungLuongPin { get; set; }

        [DisplayName("Theo dõi sức khoẻ")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string TheoDoiSucKhoe { get; set; }

        [DisplayName("Hiển thị thông báo")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string HienThiThongBao { get; set; }

        [DisplayName("Tiện ích khác")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string TienIchKhac { get; set; }

        [StringLength(100)]
        [DisplayName("Kết nối")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string KetNoi { get; set; }

        [StringLength(100)]
        [DisplayName("Chống nước")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string ChongNuoc { get; set; }

        [DisplayName("Độ dài dây")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật", DataFormatString = "{0} cm")]
        public float? DoDaiDay { get; set; }

        [DisplayName("Độ rộng dây")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật", DataFormatString = "{0} cm")]
        public float? DoRongDay { get; set; }

        [StringLength(100)]
        [DisplayName("Chất liệu dây")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string ChatLieuDay { get; set; }

        [DisplayName("Dây có thể tháo rời")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public bool? DayThaoRoi { get; set; }

        [StringLength(100)]
        [DisplayName("Ngôn ngữ")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string NgonNgu { get; set; }

        [StringLength(100)]
        [DisplayName("Kích thước")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string KichThuoc { get; set; }

        [DisplayName("Trọng lượng")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật", DataFormatString = "{0:N0} g")]
        public int? TrongLuong { get; set; }

        public virtual DongHo DongHo { get; set; }
    }
}
