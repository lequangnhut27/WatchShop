namespace WatchShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using WatchShop.Data.Infrastructure;

    [Table("AccountManager")]
    public partial class AccountManager
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountManager()
        {
            Roles = new HashSet<Role>();
        }

        [Key]
        public int ManagerId { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập tên tài khoản")]
        [StringLength(20)]
        [Index(IsUnique = true)]
        [DisplayName("Tên tài khoản")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập tên tài khoản")]
        [StringLength(20)]
        [DisplayName("Mật khẩu")]
        public string Pass { get; set; }

        [StringLength(100)]
        [DisplayName("Họ và tên")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string HoTen { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Ngày sinh")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", NullDisplayText = "Chưa cập nhật")]
        public DateTime? NgaySinh { get; set; }

        [StringLength(10)]
        [DisplayName("Giới tính")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string GioiTinh { get; set; }

        [StringLength(100)]
        [EmailAddress]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string Email { get; set; }

        [StringLength(10)]
        [DisplayName("Số điện thoại")]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string SoDT { get; set; }

        [StringLength(100)]
        [DisplayName("Ảnh đại diện")]
        public string AnhURL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Role> Roles { get; set; }
    }
}
