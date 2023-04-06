namespace MinkyShopProject.Data.Models
{
    public class SoLuongThangNam
    {
        public int nam { get; set; }
        public int thang { get; set; }
        public int soluong { get; set; }
    }
    public class TongTienNgayTienThangNam
    {
        public float TongTien { get; set; }
        public int ngay { get; set; }
        public int thang { get; set; }
        public int nam { get; set; }

    }
    public class ThongKeTongTienResult
    {
        public float TongTienTatCa { get; set; }
        public List<TongTienNgayTienThangNam> ThongKeTheoNgayTienThangNam { get; set; }
    }
    public class NhanVienBanHangNhieuNHat
    {
        public Guid Id { get; set; }
        public string Ten { get; set; }
        public int SoHoaDon { get; set; }
        public float TongTien { get; set; }
        public int SoSanPhamBan { get; set; }
    }
    public class SpBanNhieuNHat
    {
        public string Ten { get; set; }
        public int SoLuong { get; set; }

    }
}
