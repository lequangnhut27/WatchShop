namespace WatchShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhachHang")]
    public partial class KhachHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        [DisplayName("Mã khách hàng")]
        public int MaKH { get; set; }

        //[Required(ErrorMessage = "Bạn phải nhập tên đăng nhập")]
        [StringLength(20)]
        [Index(IsUnique = true)]
        [DisplayName("Tên đăng nhập")]
        public string TenDangNhap { get; set; }

        //[Required(ErrorMessage = "Bạn phải nhập tên mật khẩu")]
        [StringLength(20)]
        [DisplayName("Mật khẩu")]
        public string MatKhau { get; set; }

        [StringLength(100)]
        [DisplayName("Họ và tên")]
        //[Required(ErrorMessage = "Bạn phải nhập họ tên")]
        public string HoTen { get; set; }

        //[Required]
        [StringLength(10)]
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }

        [StringLength(100)]
        [EmailAddress]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string Email { get; set; }

        //[Required]
        [StringLength(10)]
        [DisplayName("Số điện thoại")]
        public string SoDT { get; set; }

        [StringLength(100)]
        [DisplayName("Ảnh đại diện")]
        public string AnhURL { get; set; }

        public bool? Enable { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
