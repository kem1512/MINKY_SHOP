﻿using MinkyShopProject.Common;

namespace MinkyShopProject.Business.Repositories.ThongKe
{
    public interface IThongKeRepository
    {
        public Task<Response> ThongKeSanPhamBanNhieuNhatTheoThangNam();
        public Task<Response> ThongKeTongTienNgayTienThangNam(string loaiThongKe);
        public Task<Response> ThongKeNhanVienBanDuocNhieuHoaDonvaTienNhat();
        public Task<Response> ThongKeNhanVienkhongBanDuocHang();
        public Task<Response> SanPhamBanNhieuNhatSoLuong();
        public Task<Response> SanPhamBanNhieuNhatTien();
        public Task<Response> ThongKeKhackHangMuaNhieuNhat();
        public Task<Response> ThongKeKhachHangHuyHoaDonNhieuNhat();
    }
}