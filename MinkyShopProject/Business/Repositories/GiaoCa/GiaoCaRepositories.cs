using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Code = System.Net.HttpStatusCode;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;

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
                    TongTienMat = model.TongTienMat,
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

        public async Task<Response> GetCaHienTai(Guid Id, DateTime ThoiGian)
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
                Ca.TongTienMatCuoiCa = model.TongTienMatCuoiCa;
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
            var hoadons = await _context.HoaDon.Where(c => c.NgayTao.Day == ThoiGian.Day && c.NgayTao.Month == ThoiGian.Month && c.NgayTao.Year == ThoiGian.Year && c.IdNhanVien == IdNhanVien && c.TrangThai == 0).ToListAsync();
            return new Response<List<Entities.HoaDon>>(Code.OK, hoadons);
        }

        public async Task<Response> GetTienMatHoaDon(Guid IdHoaDon)
        {
            var hinhthucthanhtoan = await _context.HinhThucThanhToan.Where(c => c.IdHoaDon == IdHoaDon && c.KieuThanhToan == 0).ToListAsync();
            return new Response<List<HinhThucThanhToan>>(Code.OK, hinhthucthanhtoan);
        }

        public async Task<Response> GetTienChuyenKhoanHoaDon(Guid IdHoaDon)
        {
            var hinhthucthanhtoan = await _context.HinhThucThanhToan.Where(c => c.IdHoaDon == IdHoaDon && c.KieuThanhToan == 1).ToListAsync();
            return new Response<List<HinhThucThanhToan>>(Code.OK, hinhthucthanhtoan);
        }

        public async Task<Response> UpdateTiemMat(Guid IdCa, float TongTien)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == IdCa);
                Ca.TongTienMat = Ca.TongTienMat += TongTien;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.OK, "");
                }
                else
                {
                    return new Response(Code.BadRequest, "Error");
                }
            }
            catch (Exception exception)
            {
                return new Response(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> GetCaHienTaiChoBanGiao()
        {
            var Cas = await _context.giaoCas.Where(a => a.ThoiGianNhanCa.Day == DateTime.Now.Day && a.ThoiGianNhanCa.Month == DateTime.Now.Month && a.ThoiGianNhanCa.Year == DateTime.Now.Year).ToListAsync();


            return new Response<List<Entities.GiaoCa>>(Code.OK, Cas);
        }

        public async Task<Response> GetCaDuocChon(Guid Id)
        {
            var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == Id);
            return new Response<Entities.GiaoCa>(Code.OK, Ca);
        }

        public async Task<Response> RutTien(Guid Id, GiaoCaModels.ResetTienModel model)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == Id);
                Ca.ThoiGianReset = model.ThoiGianReset;
                Ca.TongTienMatRut = Ca.TongTienMatRut + model.TongTienMatRut;
                Ca.GhiChuRutTien = Ca.GhiChuRutTien + model.GhiChuRutTien;
                Ca.TongTienMat = Ca.TongTienMat - model.TongTienMatRut;
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return new Response(Code.OK, "Rút Tiền Thành Công");
                }
                else
                {
                    return new Response(Code.BadRequest, "Rút Tiền Thất Bại");
                }
            }
            catch (Exception exception)
            {
                return new Response(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> UpdateNhanVien(Guid Id, GiaoCaModels.GiaoCaEditModel model)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == Id);
                Ca.IdNhanVienCaTiepTheo = model.IdNhanVienCaTiepTheo;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.OK, "Sửa Thành Công");
                }
                else
                {
                    return new Response(Code.BadRequest, "Sửa Thất Bại");
                }
            }
            catch (Exception exception)
            {
                return new Response(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> GetCaTruoc(Guid IdNhanVien)
        {
            var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.IdNhanVienCaTiepTheo == IdNhanVien && c.ThoiGianNhanCa.Day == DateTime.Now.Day && c.ThoiGianNhanCa.Month == DateTime.Now.Month && c.ThoiGianNhanCa.Year == DateTime.Now.Year && c.ThoiGianNhanCa.Hour <= DateTime.Now.Hour && c.TrangThai == 1);
            return new Response<Entities.GiaoCa>(Code.OK, Ca);
        }

        public async Task<Response> NhanCa(Guid Id)
        {
            try
            {
                var Ca = await _context.giaoCas.FirstOrDefaultAsync(c => c.Id == Id);
                Ca.TrangThai = 3;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.OK, "Sửa Thành Công");
                }
                else
                {
                    return new Response(Code.BadRequest, "Sửa Thất Bại");
                }
            }
            catch (Exception exception)
            {
                return new Response(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> SendMail()
        {
            var Body = "<div class='card'>";
            Body += "<div>";
            Body += "<div>";
            Body += "<div>";
            Body += "<label class='form-control-label'>Nhân viên vàn giao: </label>";
            Body += "</div>";
            Body += "<div>";
            Body += "<label class='form-control-label'>Nhân viên chận ca: </label>";
            Body += "</div>";
            Body += "<div>";
            Body += "<label class='form-control-label'>Thời gian nhận ca: </label>";
            Body += "</div>";
            Body += "<div>";
            Body += "<label class='form-control-label'>Thời gian giao ca: </label>";
            Body += "</div>";
            Body += "<div>";
            Body += "<label class='form-control-label'>Tiền đầu ca: </label>";
            Body += "</div>";
            Body += "<div>";
            Body += "<label class='form-control-label'>Tổng tiền mặt trong ca: </label>";
            Body += "</div>";
            Body += "<div>";
            Body += "<label class='form-control-label'>Tổng tiền chuyển khoản trong ca: </label>";
            Body += "</div>";
            Body += "<div>";
            Body += "<label class='form-control-label'>Tiền Phát sinh : </label>";
            Body += "</div>";
            Body += "<div>";
            Body += "<label class='form-control-label'>Ghi chú: </label>";
            Body += "</div>";
            Body += "<div>";
            Body += "<label class='form-control-label'>Tổng tiền mặt cuối ca: </label>";
            Body += "</div>";
            Body += "</div>";
            Body += "</div>";
            Body += "</div>";

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("txuantruong400@gmail.com"));
            email.To.Add(MailboxAddress.Parse("truongapro342002@gmail.com"));
            email.Subject = "Báo Cáo Kết Ca";
            email.Body = new TextPart(TextFormat.Html) { Text = Body };


            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("txuantruong400@gmail.com", "truong290122");
            smtp.Send(email);
            smtp.Disconnect(true);

            return new Response(Code.OK, "Gửi Thành Công");
 
        }
    }
}
