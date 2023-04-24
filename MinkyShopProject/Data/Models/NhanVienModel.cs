using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public static class NhanVienModel
    {
        public class NhanVienCreateModel
        {
            public string Ma { get; set; } = null!;

            public string Ten { get; set; } = null!;

            public string Anh { get; set; } = null!;

            public bool GioiTinh { get; set; }

            public DateTime? NgaySinh { get; set; }

            public string DiaChi { get; set; } = null!;

            public string Sdt { get; set; } = null!;

            public string Email { get; set; } = null!;
            public string MatKhau { get; set; } = null!;

            public int VaiTro { get; set; }

            public int TrangThai { get; set; }
        }

        public class NhanVienFormModel
        {
            [Required(ErrorMessage = "Mã nhân viên không được để trống")]
            public string Ma { get; set; } = null!;

            [Required(ErrorMessage = "Tên nhân viên không được để trống")]
            //[RegularExpression("^[0-9]", ErrorMessage = "Tên phải là chữ")]
            public string Ten { get; set; } = null!;

            public string Anh { get; set; } = null!;

            public bool GioiTinh { get; set; }

            public DateTime? NgaySinh { get; set; }

            [Required(ErrorMessage = "Địa chỉ nhân viên không được để trống")]
            [MinLength(10, ErrorMessage = "Địa chỉ phải lớn hơn 10 ký tự")]
            public string DiaChi { get; set; } = null!;

            [Required(ErrorMessage = "Số điện thoại không được để trống")]
            [RegularExpression("^[0-9]", ErrorMessage = "Số điện thoại phải là số")]
            public string Sdt { get; set; } = null!;

            [Required(ErrorMessage = "Email nhân viên không được để trống")]
            [RegularExpression("^([\\w-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([\\w-]+\\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)$", ErrorMessage = "Email không đúng định dạng")]
            public string Email { get; set; } = null!;

            [Required(ErrorMessage = "Mật khẩu nhân viên không được để trống")]
            [MinLength(6, ErrorMessage = "Mật khẩu phải lớn hơn 6 kí tự")]
            [MaxLength(30, ErrorMessage = "Mật khẩu phải nhỏ hơn 30 ký tự")]
            public string MatKhau { get; set; } = null!;

            public int VaiTro { get; set; }

            public int TrangThai { get; set; }
        }
    }
}
