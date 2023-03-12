using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;

namespace MinkyShopProject.Data.Models
{
    public class SanPhamModel
    {
        public Guid Id { get; set; }

        public Guid? IdNhomSanPham { get; set; }

        public string Ten { get; set; } = null!;

        public string? Ma { get; set; } = null!;

        public string? Anh { get; set; } = null!;

        public DateTime NgayTao { get; set; }

        public int TrangThai { get; set; }

        public NhomSanPhamModel? NhomSanPham { get; set; } = null!;

        public List<BienTheModel>? BienThes { get; set; } = null!;
    }

    public class SanPhamQueryModel : PaginationRequest
    {

    }
}
