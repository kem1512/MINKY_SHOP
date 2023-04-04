using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
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

        public async Task<Response> GetCaHienThai(Guid Id, DateTime Time)
        {
            var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.IdNhanVienTrongCa == Id && c.ThoiGianNhanCa.Day == Time.Day && c.ThoiGianNhanCa.Month == Time.Month && c.ThoiGianNhanCa.Year == Time.Year && c.TrangThai == 0);
            return new Response<Entities.GiaoCa>(Code.OK, Ca);
        }

        public async Task<Response> KetCa(Guid Id, DateTime Time, GiaoCaModels.GiaoCaEditModel model)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.IdNhanVienTrongCa == Id && c.ThoiGianNhanCa.Day == Time.Day && c.ThoiGianNhanCa.Month == Time.Month && c.ThoiGianNhanCa.Year == Time.Year && c.TrangThai == 0);

                var idCa = Ca.Id;

                var CaUpdate = new Entities.GiaoCa()
                {
                    Id = idCa,
                    ThoiGianGiaoCa = model.ThoiGianGiaoCa,
                    IdNhanVienCaTiepTheo = model.IdNhanVienCaTiepTheo,
                    TongTienTrongCa = model.TongTienTrongCa,
                    TongTienMat = model.TongTienMat,
                    TongTienKhac = model.TongTienKhac,
                    TienPhatSinh = model.TienPhatSinh,
                    GhiChuPhatSinh = model.GhiChuPhatSinh,
                    TongTienMatCaTruoc = model.TongTienMatCaTruoc,
                    ThoiGianReset = model.ThoiGianReset,
                    TrangThai = 1,
                };

                _context.Update(CaUpdate);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.OK, "Kết ca thành công");
                }
                else
                {
                    return new ResponseError(Code.BadRequest, "Kết ca thất bại");
                }
            }
            catch (Exception ex)
            {

                return new ResponseError(Code.InternalServerError, ex.ToString());
            }
        }

        public async Task<Response<int>> GetHoaDonCa(Guid IdNhanVien, DateTime Time)
        {
            var hoadon = await _context.HoaDon.Where(c => c.NgayTao.Day == Time.Day && c.NgayTao.Month == Time.Month && c.NgayTao.Year == Time.Year && c.IdNhanVien == IdNhanVien).ToListAsync();
            if (hoadon.Count() == 0)
            {
                return new Response<int>(Code.BadRequest, 0);
            }
            else
            {
                return new Response<int>(Code.BadRequest, hoadon.Count());
            }
        }
    }
}
