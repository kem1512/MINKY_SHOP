using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public static class NhanVienModel
    {
        public class NhanVienCreateModel
        {
            [Required(ErrorMessage = "Mã nhân viên không được để trống")]
            public string Ma { get; set; } = null!;

            [Required(ErrorMessage = "Tên nhân viên không được để trống")]
            public string Ten { get; set; } = null!;

            [Required(ErrorMessage = "Ảnh nhân viên không được để trống")]
            public string Anh { get; set; } = null!;

            public bool GioiTinh { get; set; }

            public DateTime? NgaySinh { get; set; }

            [Required(ErrorMessage = "Địa chỉ nhân viên không được để trống")]
            public string DiaChi { get; set; } = null!;

            [Required(ErrorMessage = "Mã nhân viên không được để trống")]
            public string Sdt { get; set; } = null!;

            [Required(ErrorMessage = "Email nhân viên không được để trống")]
            public string Email { get; set; } = null!;

            [Required(ErrorMessage = "Mật khẩu nhân viên không được để trống")]
            public string MatKhau { get; set; } = null!;

            public int VaiTro { get; set; }

            public int TrangThai { get; set; }
        }
    }
}
