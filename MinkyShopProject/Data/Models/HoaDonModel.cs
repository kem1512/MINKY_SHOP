using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public class HoaDonModel
    {
        public Guid Id { get; set; }

        public Guid IdNhanVien { get; set; } = Guid.Parse("E0921328-1982-4107-8272-DE00F8505341");

        public Guid? IdKhachHang { get; set; }

        public string? Ma { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public DateTime? NgayThanhToan { get; set; }

        public DateTime? NgayGiaoHang { get; set; }

        public DateTime? NgayNhan { get; set; }

        public int LoaiDonHang { get; set; }

        public KhachHangModel? KhachHang { get; set; } = new KhachHangModel();

        public string? TenNguoiNhan { get; set; }

        public string? DiaChi { get; set; }

        public string? Sdt { get; set; }

        public float TienShip { get; set; }

        public float TongTien { get; set; }

        public List<HinhThucThanhToanModel> HinhThucThanhToans { get; set; } = new List<HinhThucThanhToanModel>() { new HinhThucThanhToanModel() };

        public int TrangThai { get; set; }

        public List<HoaDonChiTietModel> HoaDonChiTiets { get; set; } = new List<HoaDonChiTietModel>();
    }

    public class HinhThucThanhToanModel
    {
        public Guid Id { get; set; }

        public Guid IdHoaDon { get; set; }

        public DateTime NgayTao { get; set; }

        public int KieuThanhToan { get; set; }

        public float TongTienThanhToan { get; set; }

        public string? GhiChu { get; set; }
    }

    public class HoaDonChiTietModel
    {
        public Guid Id { get; set; }

        public Guid IdBienThe { get; set; }

        public Guid IdHoaDon { get; set; }

        public int SoLuong { get; set; }

        public float DonGia { get; set; }

        public BienTheModel? BienThe { get; set; }
    }

    public class HoaDonQueryModel : PaginationRequest
    {

    }
}
