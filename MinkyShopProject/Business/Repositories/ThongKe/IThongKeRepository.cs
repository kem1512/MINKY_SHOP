using MinkyShopProject.Common;

namespace MinkyShopProject.Business.Repositories.ThongKe
{
    public interface IThongKeRepository
    {
        public Task<Response> ThongKeSanPhamBanNhieuNhatTheoThangNam();
        public Task<Response> ThongKeTongTienNgayTienThangNam(string loaiThongKe);
        public Task<Response> ThongKeNhanVienBanDuocNhieuHoaDonvaTienNhat();
        public Task<Response> SanPhamBanNhieuNhat();
    }
}
