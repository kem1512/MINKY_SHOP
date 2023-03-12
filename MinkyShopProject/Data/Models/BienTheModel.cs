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

        public string Ten { get; set; } = null!;

        public Guid IdSanPham { get; set; }

        public int SoLuong { get; set; }

        public float GiaBan { get; set; }

        public string Sku { get; set; } = null!;

        public string Anh { get; set; } = null!;

        public DateTime NgayTao { get; set; }

        public int TrangThai { get; set; }
    }

    public class BienTheChiTietModel
    {
        public List<ThuocTinhModel>? ThuocTinhs { get; set; } = new List<ThuocTinhModel>();

        public SanPhamModel SanPham { get; set; } = new SanPhamModel();
    }

    public class BienTheCreateModel
    {
        public List<ThuocTinhModel> ThuocTinhs { get; set; } = null!;

        public SanPhamModel SanPham { get; set; } = null!;
    }
}
