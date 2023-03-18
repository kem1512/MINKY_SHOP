using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class BienThe
    {
        public Guid Id { get; set; }

        public string? Ten { get; set; }

        public Guid IdSanPham { get; set; }

        public int SoLuong { get; set; }

        public float GiaBan { get; set; }

        public string? Sku { get; set; }

        public string? Anh { get; set; }

        public DateTime NgayTao { get; set; }

        public int TrangThai { get; set; }

        public SanPham? SanPham { get; set; }

        public List<DanhGia> DanhGias { get; set; } = null!;

        public List<GioHangChiTiet>? GioHangChiTiets { get; set; }
        public List<BienTheChiTiet>? BienTheChiTiets { get; set; }

        public List<HoaDonChiTiet>? HoaDonChiTiets { get; set; }
    }
}
