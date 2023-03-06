using MinkyShopProject.Data.Enums;

namespace MinkyShopProject.Data.Models
{
    public class SanPhamModel
    {
        public Guid Id { get; set; }

        public Guid? IdTheLoai { get; set; }

        public string Ten { get; set; } = null!;

        public TrangThaiSanPham TrangThai { get; set; }

        public DateTime NgayTao { get; set; }

        public List<SanPhamModel>? TheLoais { get; set; } = new List<SanPhamModel>() { };
    }

    // Biến Thể

    public class BienTheModel
    {
        public Guid Id { get; set; }

        public string Ten { get; set; } = null!;

        public string Sku { get; set; } = null!;

        public float GiaBan { get; set; }

        public string Anh { get; set; } = null!;

        public int SoLuong { get; set; }

        public string GiaTri { get; set; } = null!;
    }

    public class BienTheChiTietModel
    {
        public List<BienTheModel> BienTheModels { get; set; } = null!;

        public List<ThuocTinhModel> ThuocTinhModels { get; set; } = null!;
    }

    public class BienTheCreateModel
    {
        public List<ThuocTinhModel> ThuocTinhs { get; set; } = null!;

        public SanPhamModel SanPham { get; set; } = null!;
    }
}
