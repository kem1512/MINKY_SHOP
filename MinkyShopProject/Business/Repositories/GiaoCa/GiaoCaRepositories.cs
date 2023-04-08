using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code = System.Net.HttpStatusCode;

namespace MinkyShopProject.Business.Repositories.GiaoCa
{
    public class GiaoCaRepositories : IGiaoCaRepositories
    {

        private readonly MinkyShopDbContext _context;

        public GiaoCaRepositories(MinkyShopDbContext context)
        {
            _context = context;
        }

        public async Task<Response> KhaiBaoDauCa(GiaoCaModels.GiaoCaCreateModel model)
        {
            try
            {
                var ca = new Entities.GiaoCa()
                {
                    Id = Guid.NewGuid(),
                    IdNhanVienTrongCa = model.IdNhanVienTrongCa,
                    ThoiGianNhanCa = model.ThoiGianNhanCa,
                    TienBanDau = model.TienBanDau,
                    TrangThai = model.TrangThai,
                    GhiChuPhatSinh = ""
                };

                await _context.AddAsync(ca);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.Created, "Bạn đã bắt đầu ca làm việc");
                }

                return new ResponseError(Code.BadRequest, "Bắt đầu ca làm việc thất bại");
            }
            catch (Exception exception)
            {
                return new ResponseError(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> GetCaHienThai(Guid Id, DateTime ThoiGian)
        {
              var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.IdNhanVienTrongCa == Id && c.ThoiGianNhanCa.Day == ThoiGian.Day && c.ThoiGianNhanCa.Month == ThoiGian.Month && c.ThoiGianNhanCa.Year == ThoiGian.Year && c.ThoiGianNhanCa.Hour <= ThoiGian.Hour && c.TrangThai == 0);
              return new Response<Entities.GiaoCa>(Code.OK,Ca);
        }

        public async Task<Response> KetCa(Guid Id,GiaoCaModels.GiaoCaEditModel model)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c=>c.Id == Id);
                Ca.ThoiGianGiaoCa = model.ThoiGianGiaoCa;
                Ca.IdNhanVienCaTiepTheo = model.IdNhanVienCaTiepTheo;
                Ca.TongTienTrongCa = model.TongTienTrongCa;
                Ca.TongTienMat = model.TongTienMat;
                Ca.TongTienKhac = model.TongTienKhac;
                Ca.TienPhatSinh = model.TienPhatSinh;
                Ca.GhiChuPhatSinh = model.GhiChuPhatSinh;
                Ca.TongTienMatCaTruoc = model.TongTienMatCaTruoc;
                Ca.ThoiGianReset = model.ThoiGianReset;
                Ca.TrangThai = 1;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.OK, "Hoàn thành báo cáo");
                }
                else
                {
                    return new ResponseError(Code.BadRequest, "Kết ca thất bại,vui lòng thử lại");
                }
            }
            catch (Exception ex)
            {

                return new ResponseError(Code.InternalServerError, ex.ToString());
            }
        }

        public async Task<Response> GetHoaDonCa(Guid IdNhanVien,DateTime ThoiGian)
        {
            var hoadons = await _context.HoaDon.Where(c => c.NgayTao.Day == ThoiGian.Day && c.NgayTao.Month == ThoiGian.Month && c.NgayTao.Year == ThoiGian.Year && c.NgayTao.Hour <= ThoiGian.Hour && c.IdNhanVien == IdNhanVien).ToListAsync();
            return new Response<List<Entities.HoaDon>>(Code.OK, hoadons);
        }

        public async Task<Response> GetTienMatHoaDon(Guid IdHoaDon)
        {
            var hinhthucthanhtoan = await _context.HinhThucThanhToan.Where(c => c.IdHoaDon == IdHoaDon && c.KieuThanhToan == 0).ToListAsync();
            return new Response<List<HinhThucThanhToan>>(Code.OK, hinhthucthanhtoan);
        }
    }
}
