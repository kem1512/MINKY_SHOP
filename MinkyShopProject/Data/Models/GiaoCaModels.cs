using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public static class GiaoCaModels
    {
        public class GiaoCaCreateModel
        {
            public DateTime ThoiGianNhanCa { get; set; }

            public Guid IdNhanVienTrongCa { get; set; }

            public float TienBanDau { get; set; }

            public float? TongTienMat { get; set; }

            public int TrangThai { get; set; }
        }

        public class GiaoCaEditModel
        {
            public DateTime ThoiGianGiaoCa { get; set; }

            public Guid? IdNhanVienCaTiepTheo { get; set; }

            public float? TongTienTrongCa { get; set; }

            public float? TongTienMat { get; set; }

            public float? TongTienKhac { get; set; }

            public float? TienPhatSinh { get; set; }

            public string? GhiChuPhatSinh { get; set; } = null!;

            public float? TongTienMatCaTruoc { get; set; }

            public float? TongTienMatCuoiCa { get; set; }

            public DateTime? ThoiGianReset { get; set; }
        }

        public class GiaoCaViewModel
        {
            public Guid Id { get; set; }
            public string TenNhanVienTrongCa { get; set; } = null!;
            public string TenNhanVienCaTiepTheo { get; set; } = null!;
            public float? TongTienMat { get; set; }
            public int TrangThai { get; set; }
        }

        public class ResetTienModel
        {
            public float TongTienMatRut { get; set; }

            public DateTime ThoiGianReset { get; set; }

            public string GhiChuRutTien { get; set; } = null!;
        }
    }
}
