using MinkyShopProject.Common;

namespace MinkyShopProject.Data.Models
{
    public class KhachHangModel
    {
        public Guid Id { get; set; }

        public Guid? IdViDiem { get; set; }

        public string Ma { get; set; } = null!;

        public string Ten { get; set; } = null!;

        public string Anh { get; set; } = null!;

        public bool GioiTinh { get; set; }

        public DateTime NgaySinh { get; set; }

        public string DiaChi { get; set; } = null!;

        public string Sdt { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string MatKhau { get; set; } = null!;

        public int SoLanMua { get; set; }
    }
    public class KhachHangThemVaSuaModel
    {

        public Guid? IdViDiem { get; set; }

        public string Ma { get; set; } = null!;

        public string Ten { get; set; } = null!;

        public string Anh { get; set; } = null!;

        public bool GioiTinh { get; set; }

        public DateTime NgaySinh { get; set; }

        public string DiaChi { get; set; } = null!;

        public string Sdt { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string MatKhau { get; set; } = null!;

        public int SoLanMua { get; set; }
    }
    public class KhachHangQueryModel : PaginationRequest
    {
        public Guid? khachhangId { get; set; }
    }
}
