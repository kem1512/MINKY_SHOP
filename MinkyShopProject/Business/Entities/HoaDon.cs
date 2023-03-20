using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class HoaDon
    {
        public Guid Id { get; set; }

        public Guid IdNhanVien { get; set; }

        public Guid IdKhachHang { get; set; }

        public string Ma { get; set; } = null!;

        public DateTime NgayTao { get; set; }

        public DateTime? NgayThanhToan { get; set; }

        public DateTime? NgayGiaoHang { get; set; }

        public DateTime? NgayNhan { get; set; }

        public int LoaiDonHang { get; set; }

        public string? TenNguoiNhan { get; set; }

        public string? DiaChi { get; set; }

        public string? Sdt { get; set; }

        public float TienShip { get; set; }

        public float TongTien { get; set; }

        public int TrangThai { get; set; }

        public NhanVien? NhanVien { get; set; }

        public DanhGia? DanhGia { get; set; }

        public KhachHang? KhachHang { get; set; }

        public List<HoaDonChiTiet>? HoaDonChiTiets { get; set; }

        public List<VoucherLog>? VoucherLogs { get; set; }

        public List<HinhThucThanhToan>? HinhThucThanhToans { get; set; }
    }
}
