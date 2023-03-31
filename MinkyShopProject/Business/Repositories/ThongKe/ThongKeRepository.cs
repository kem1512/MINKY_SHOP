using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using Serilog;
using System.Net;

namespace MinkyShopProject.Business.Repositories.ThongKe
{
    public class ThongKeRepository : IThongKeRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;
        public ThongKeRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> ThongKeSanPhamBanNhieuNhatTheoThangNam()
        {
            try
            {
                var result = await _context.HoaDon
                    .GroupBy(x => new { x.NgayTao.Year, x.NgayTao.Month })
                    .Select(g => new SoLuongThangNam
                    {
                        nam = g.Key.Year,
                        thang = g.Key.Month,
                        soluong = g.Count()
                    })
                    .ToListAsync();
                return new ResponseList<SoLuongThangNam>(result);
            }
            catch (Exception e)
            {
                Log.Error("Thống kê số lượng sản phẩm bán theo tháng không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }
        public async Task<Response> ThongKeTongTienNgayTienThangNam()
        {
            try
            {
                var result = await _context.HoaDon
                    .GroupBy(x => new { x.NgayTao.Day, x.NgayTao.Year, x.NgayTao.Month })
                    .OrderByDescending(g => g.Key.Year)
                    .ThenByDescending(g => g.Key.Month)
                    .ThenByDescending(g => g.Key.Day)
                    .Select(g => new TongTienNgayTienThangNam()
                    {
                        TongTien = g.Sum(x => x.TongTien),
                        Ngay = g.Key.Day,
                        thang = g.Key.Month,
                        nam = g.Key.Year
                    })
                    .ToListAsync();
                return new ResponseList<TongTienNgayTienThangNam>(result);
            }
            catch (Exception e)
            {
                Log.Error("Thống kê tổng tiền theo ngày tháng năm không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }
        public async Task<Response> ThongKeNhanVienBanDuocNhieuHoaDonvaTienNhat()
        {
            try
            {
                var result = await _context.NhanVien
                    .Include(x => x.HoaDons)
                    .Select(x => new NhanVienBanHangNhieuNHat()
                    {
                        Id = x.Id,
                        Ten = x.Ten,
                        SoHoaDon = x.HoaDons.Count(),
                        TongTien = x.HoaDons.Sum(hd => hd.TongTien),
                    })
                    .OrderByDescending(x => x.TongTien)
                    .ToListAsync();
                return new ResponseList<NhanVienBanHangNhieuNHat>(result);
            }
            catch (Exception e)
            {
                Log.Error("Thống kê số lượng nhân viên bán được nhiều hàng nhất không thành công");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }
    }
}
