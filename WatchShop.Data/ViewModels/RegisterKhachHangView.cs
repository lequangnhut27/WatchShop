using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WatchShop.Data.ViewModels
{
    public class RegisterKhachHangView
    {        
        [Required(ErrorMessage = "Bạn phải nhập tên đăng nhập")]
        [StringLength(20,ErrorMessage = "Không được vượt quá 20 ký tự")]

        [DisplayName("Tên đăng nhập")]
        //[Remote("IsTenDangNhapExist", "Home",HttpMethod = "Post" ,ErrorMessage = "Tên đăng nhập đã tồn tại")]
        public string TenDangNhap { get; set; }

        [Required(ErrorMessage = "Bạn phải nhập mật khẩu")]
        [StringLength(20)]
        [DisplayName("Mật khẩu")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[0-9])(?=.*[a-z]).{8,}$",
            ErrorMessage = "Mật khẩu ít nhất 8 ký tự, phải bao gồm ký tự hoa, ký tự thường và số")]
        public string MatKhau { get; set; }


        [Required(ErrorMessage = "Bạn phải nhập lại mật khẩu")]
        [StringLength(20)]
        [DisplayName("Nhập lại mật khẩu")]
        [System.ComponentModel.DataAnnotations.Compare("MatKhau", ErrorMessage = "Nhập lại mật khẩu không chính xác")]
        public string ConfirmMatKhau { get; set; }
        [Required]
        [StringLength(100)]
        [DisplayName("Họ và tên")]
        public string HoTen { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Giới tính")]
        public string GioiTinh { get; set; }

        [StringLength(100)]
        [EmailAddress]
        [DisplayFormat(NullDisplayText = "Chưa cập nhật")]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        [DisplayName("Số điện thoại")]
        public string SoDT { get; set; }

        [StringLength(100)]
        [DisplayName("Ảnh đại diện")]
        public string AnhURL { get; set; }
    }
}