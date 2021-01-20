namespace WatchShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DongHo")]
    public partial class DongHo
    {


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DongHo()
        {
            ChiTietHoaDons = new HashSet<ChiTietHoaDon>();
        }

        [Key]
        [DisplayName("Mã đồng hồ")]
        public int MaDH { get; set; }

        [StringLength(100)]
        [DisplayName("Tên đồng hồ")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string TenDH { get; set; }

        [StringLength(10)]
        [DisplayName("Mã thương hiệu")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string MaTH { get; set; }

        [DisplayName("Giá")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ")]
        [Required(ErrorMessage = "Yêu cầu nhập giá tiền")]
        public int GiaGoc { get; set; }

        [StringLength(10)]
        [DisplayName("Mã giảm giá")]
        [DisplayFormat(NullDisplayText = "Không giảm")]
        public string MaGG { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayName("Giá đã giảm")]
        [DisplayFormat(DataFormatString = "{0:N0} VNĐ")]
        public int? GiaBan { get; set; }

        [DisplayName("Mô tả")]
        public string MoTa { get; set; }

        [DisplayName("Số lượng tồn kho")]
        public int SoLuong { get; set; }

        [StringLength(100)]
        [DisplayName("Ảnh")]
        public string AnhChinhURL { get; set; }

        [StringLength(100)]
        public string Anh1URL { get; set; }

        [StringLength(100)]
        public string Anh2URL { get; set; }

        [StringLength(100)]
        public string Anh3URL { get; set; }

        [StringLength(100)]
        public string Anh4URL { get; set; }
        
        public bool? Enable { get; set; }

        public virtual ChiTietGiamGia ChiTietGiamGia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; }

        public virtual ThuongHieu ThuongHieu { get; set; }

        public virtual DongHoThoiTrang DongHoThoiTrang { get; set; }

        public virtual DongHoThongMinh DongHoThongMinh { get; set; }
    }
}