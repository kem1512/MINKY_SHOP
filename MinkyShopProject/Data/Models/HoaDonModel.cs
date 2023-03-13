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

        public Guid IdNhanVien { get; set; }

        public Guid IdKhachHang { get; set; }

        public string Ma { get; set; } = null!;

        public DateTime NgayTao { get; set; }

        public DateTime NgayThanhToan { get; set; }

        public DateTime NgayGiaoHang { get; set; }

        public DateTime NgayNhan { get; set; }

        public int LoaiDonHang { get; set; }

        public string TenNguoiNhan { get; set; } = null!;

        public string DiaChi { get; set; } = null!;

        public string Sdt { get; set; } = null!;

        public float TienShip { get; set; }

        public float TongTien { get; set; }

        public TrangThaiHoaDon TrangThai { get; set; }

        public List<HoaDonChiTietModel> HoaDonChiTiets { get; set; } = null!;
    }

    public class HoaDonChiTietModel
    {
        public Guid Id { get; set; }

        public Guid IdBienThe { get; set; }

        public Guid IdHoaDon { get; set; }

        public int SoLuong { get; set; }

        public float DonGia { get; set; }
    }

    public class HoaDonQueryModel : PaginationRequest
    {

    }
}
