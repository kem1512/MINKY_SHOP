using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;

namespace MinkyShopProject.Data.Models
{
    public class SanPhamModel
    {
        public Guid Id { get; set; }

        public Guid? IdNhomSanPham { get; set; }

        public string? Ten { get; set; }

        public string? Ma { get; set; }

        public string? Anh { get; set; }

        public DateTime NgayTao { get; set; }

        public int TrangThai { get; set; }

        public NhomSanPhamModel NhomSanPham { get; set; } = new NhomSanPhamModel();

        public List<BienTheModel> BienThes { get; set; } = new List<BienTheModel>();
    }

    public class SanPhamQueryModel : PaginationRequest
    {

    }
}
