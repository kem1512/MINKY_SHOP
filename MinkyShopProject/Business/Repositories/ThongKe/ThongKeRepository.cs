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
    }
}
