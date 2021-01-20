namespace WatchShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            ChiTietHoaDons = new List<ChiTietHoaDon>();
        }

        [Key]
        [DisplayName("Mã hoá đơn")]
        public int MaHD { get; set; }

        public int MaKH { get; set; }
        
        [StringLength(100)]
        [DisplayName("Họ và tên")]
        [Required(ErrorMessage = "Bạn phải nhập họ tên")]
        public string HoTen { get; set; }

        [DisplayName("Ngày lập hoá đơn")]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM/yyyy}")]
        public DateTime NgayLapHD { get; set; }

        [DisplayName("Tổng hoá đơn")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ")]
        public int? TongGiaTriHD { get; set; }

        [Required]
        [StringLength(200)]
        [DisplayName("Địa chỉ giao hàng")]
        public string DiaChiGiaoHang { get; set; }
        
        [Required]
        [StringLength(10)]
        [DisplayName("Số điện thoại")]
        public string SoDT { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Ngày giao dự kiến")]
        public DateTime NgayGiaoDuKien { get; set; }

        [Required]
        [StringLength(10)]
        public string MaHTTT { get; set; }

        [Required]
        [StringLength(10)]
        public string MaTTHD { get; set; }

        public int? MaShipper { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual HinhThucThanhToan HinhThucThanhToan { get; set; }

        public virtual KhachHang KhachHang { get; set; }

        public virtual TinhTrangHoaDon TinhTrangHoaDon { get; set; }
        public virtual Shipper Shipper { get; set; }
    }
}
