using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WatchShop.Data.Models
{
    public class Shipper
    {

        public Shipper()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        [DisplayName("Shipper")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaShipper { get; set; }

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
        [DisplayName("Họ tên")]
        [DisplayFormat(NullDisplayText = "Không có")]
        public string HoTen { get; set; }

        public virtual ICollection<HoaDon> HoaDons { get; set; }

    }
}