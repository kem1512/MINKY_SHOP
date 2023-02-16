using MinkyShopProject.Data.Enums;

namespace MinkyShopProject.Data.Models
{
    public class SanPhamModel
    {
        public Guid Id { get; set; }

        public string Ten { get; set; } = null!;

        public string Sku { get; set; } = null!;

        public float GiaBan { get; set; }

        public int SoLuong { get; set; }

        public string ThuocTinh { get; set; } = null!;
    }

    public class SanPhamCreateModel
    {
        public Guid IdThuocTinh { get; set; }

        public string TenThuocTinh { get; set; } = null!;

        public GiaTriModel[] GiaTris { get; set; } = null!;
    }

    public class SanPhamUpdateModel
    {
        public string Ten { get; set; } = null!;

        public TrangThaiChung TrangThai { get; set; }
    }

    // Thuộc Tính Sản Phẩm

    public class ThuocTinhSanPhamCreateModel
    {
        public Guid Id { get; set; }

        public Guid IdSanPham { get; set; }

        public Guid IdThuocTinh { get; set; }
    }

    // Biến Thể

    public class BienTheCreateModel
    {
        public Guid Id { get; set; }

        public Guid IdSanPham { get; set; }

        public string Sku { get; set; } = null!;
    }

    // Biến Thể Chi Tiêt

    public class BienTheChiTietCreateModel
    {
        public Guid IdBienThe { get; set; }

        public Guid IdGiaTri { get; set; }

        public Guid IdThuocTinhSanPham { get; set; }
    }
}
