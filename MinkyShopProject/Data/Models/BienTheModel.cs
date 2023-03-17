using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public class BienTheModel
    {
        public Guid Id { get; set; }

        public string? Ten { get; set; }

        public Guid IdSanPham { get; set; }

        public int SoLuong { get; set; }

        public float GiaBan { get; set; }

        public string? Sku { get; set; }

        public string? Anh { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public int TrangThai { get; set; }

        public SanPhamModel? SanPham { get; set; }
    }

    public class BienTheChiTietModel
    {
        public List<ThuocTinhModel>? ThuocTinhs { get; set; }

        public SanPhamModel? SanPham { get; set; }
    }

    public class BienTheCreateModel
    {
        public List<ThuocTinhModel>? ThuocTinhs { get; set; }

        public SanPhamModel? SanPham { get; set; }
    }
}
