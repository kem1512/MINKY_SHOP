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
        public async Task<Response> SanPhamBanNhieuNhat()
        {
            var ListSanPham = _context.HoaDon
                .Include(po => po.HoaDonChiTiets)
                .ToList()
                .SelectMany(po => po.HoaDonChiTiets)
                .GroupBy(s => _context.BienThe.Include(bt => bt.SanPham).FirstOrDefault(bt => bt.Id == s.IdBienThe).SanPham.Ten)
                .ToDictionary(g => g.Key, g => g.Sum(s => s.SoLuong));

            var result = ListSanPham
                .Select(p => new SpBanNhieuNHat()
                {
                    Ten = p.Key,
                    SoLuong = p.Value
                })
                .OrderByDescending(p => p.SoLuong)
                .Take(10)
                .ToList();
            return new ResponseObject<List<SpBanNhieuNHat>>(result);
        }
        public async Task<Response> ThongKeTongTienNgayTienThangNam(string loaiThongKe)
        {
            DateTime now = DateTime.Now.Date;
            DateTime start;
            DateTime end = now.AddDays(1).AddMilliseconds(-1);
            switch (loaiThongKe)
            {
                case "homnay":
                    start = now;
                    break;
                case "homqua":
                    start = now.AddDays(-1);
                    end = now.AddMilliseconds(-1);
                    break;
                case "7ngaytruoc":
                    start = now.AddDays(-6);
                    break;
                case "thangtruoc":
                    start = new DateTime(now.Year, now.Month, 1).AddMonths(-1);
                    end = new DateTime(now.Year, now.Month, 1).AddMilliseconds(-1);
                    break;
                case "thangnay":
                    start = new DateTime(now.Year, now.Month, 1);
                    break;
                default:
                    return new ResponseError(HttpStatusCode.BadRequest, "Lựa chọn không hợp lệ.");
            }

            try
            {
                var result = await _context.HoaDon
                    .Where(x => x.NgayTao >= start && x.NgayTao <= end)
                    .GroupBy(x => new { x.NgayTao.Day, x.NgayTao.Month, x.NgayTao.Year })
                    .Select(g => new TongTienNgayTienThangNam()
                    {
                        TongTien = g.Sum(x => x.TongTien),
                        ngay = g.Key.Day,
                        thang = g.Key.Month,
                        nam = g.Key.Year,
                    })
                    .ToListAsync();

                var tongTienTatCa = result.Sum(x => x.TongTien);

                var thongKeTongTienResult = new ThongKeTongTienResult()
                {
                    TongTienTatCa = tongTienTatCa,
                    ThongKeTheoNgayTienThangNam = result
                };

                return new ResponseObject<ThongKeTongTienResult>(thongKeTongTienResult);

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
