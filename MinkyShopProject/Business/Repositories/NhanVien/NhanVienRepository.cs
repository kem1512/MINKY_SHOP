using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Pagination;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.Pagination;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code = System.Net.HttpStatusCode;
using PaginationRequest = MinkyShopProject.Business.Pagination.PaginationRequest;

namespace MinkyShopProject.Business.Repositories.NhanVien
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly MinkyShopDbContext _context;

        public NhanVienRepository(MinkyShopDbContext context)
        {
            _context = context;
        }

        public async Task<Response> ChangeStatus(Guid Id,int status)
        {
            try
            {
                var newStatus = status == 0 ? 1 : 0;
                var nhanvien = await _context.NhanVien.FirstOrDefaultAsync(c => c.Id == Id);
                nhanvien.TrangThai = newStatus;
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response(Code.OK, "Sửa thành công");
                }

                return new ResponseError(Code.BadRequest, "Sửa thất bại");
            }
            catch (Exception exeption)
            {
                return new ResponseError(Code.InternalServerError, exeption.ToString());
            }
        }

        public async Task<Response> Delete(Guid Id)
        {
            try
            {
                var nhanvienhoadon = await _context.HoaDon.FirstOrDefaultAsync(c => c.IdNhanVien == Id);
                var nhanvien = await _context.NhanVien.FirstOrDefaultAsync(c => c.Id == Id);


                if (nhanvienhoadon != null)
                {
                    return new ResponseError(Code.BadRequest, "Nhân viên này tồn tại hóa đơn không thể xóa");
                }

                if (nhanvien.TrangThai == 1)
                {
                    return new ResponseError(Code.BadRequest, "Không thể xóa nhân viên còn hoạt động");
                }

                _context.NhanVien.Remove(nhanvien);
                var result  = _context.SaveChanges();
                if (result > 0)
                {
                    return new Response(Code.OK,"Xóa thành công");
                }

                return new ResponseError(Code.BadRequest, "Xóa không thành công");
            }
            catch (Exception exeption)
            {
                return new ResponseError(Code.InternalServerError,exeption.ToString());
            }
        }

        public async Task<Response> Get(PaginationRequest paginationRequest)
        {
            try
            {
                var NhanViens = new List<Entities.NhanVien>();
                var pageResult = Convert.ToSingle(paginationRequest.PerPage);
                double pageCount;

                if (paginationRequest.Status == null && paginationRequest.Role == null && paginationRequest.Keyword == null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Count() / pageResult);

                    NhanViens = await _context.NhanVien
                        .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                        .Take((int)pageResult)
                        .ToListAsync();
                }
                else if (paginationRequest.Status != null && paginationRequest.Role == null && paginationRequest.Keyword == null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.TrangThai == paginationRequest.Status).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                        .Where(c=>c.TrangThai == paginationRequest.Status)
                        .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                        .Take((int)pageResult)
                        .ToListAsync();
                }
                else if (paginationRequest.Status == null && paginationRequest.Role != null && paginationRequest.Keyword == null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.VaiTro == paginationRequest.Role).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.VaiTro == paginationRequest.Role)
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .ToListAsync();
                }
                else if (paginationRequest.Status == null && paginationRequest.Role == null && paginationRequest.Keyword != null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.Ten.Contains(paginationRequest.Keyword) || c.DiaChi.Contains(paginationRequest.Keyword) || c.Ma.Contains(paginationRequest.Keyword)).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.Ten.Contains(paginationRequest.Keyword) || c.DiaChi.Contains(paginationRequest.Keyword) || c.Ma.Contains(paginationRequest.Keyword))
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .ToListAsync();
                }
                else if (paginationRequest.Status != null && paginationRequest.Role != null && paginationRequest.Keyword == null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.TrangThai == paginationRequest.Status && c.VaiTro == paginationRequest.Role).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.TrangThai == paginationRequest.Status && c.VaiTro == paginationRequest.Role)
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .ToListAsync();
                }
                else if (paginationRequest.Status == null && paginationRequest.Role != null && paginationRequest.Keyword != null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.VaiTro == paginationRequest.Role && c.Ten.Contains(paginationRequest.Keyword)).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.VaiTro == paginationRequest.Role && c.Ten.Contains(paginationRequest.Keyword))
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .ToListAsync();
                }
                else if (paginationRequest.Status != null && paginationRequest.Role == null && paginationRequest.Keyword != null)
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.TrangThai == paginationRequest.Status && c.Ten.Contains(paginationRequest.Keyword)).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.TrangThai == paginationRequest.Status && c.Ten.Contains(paginationRequest.Keyword))
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .ToListAsync();
                }
                else
                {
                    pageCount = Math.Ceiling(_context.NhanVien.Where(c => c.TrangThai == paginationRequest.Status && c.Ten.Contains(paginationRequest.Keyword) && c.VaiTro == paginationRequest.Role).Count() / pageResult);

                    NhanViens = await _context.NhanVien
                       .Where(c => c.TrangThai == paginationRequest.Status && c.Ten.Contains(paginationRequest.Keyword) && c.VaiTro == paginationRequest.Role)
                       .Skip((paginationRequest.CurrentPage - 1) * (int)pageResult)
                       .Take((int)pageResult)
                       .ToListAsync();
                }

                var Response = new PaginationResponse<Entities.NhanVien>()
                {
                    Data = NhanViens,
                    CurrentPage = paginationRequest.CurrentPage,
                    Pages = (int)pageCount,
                };

                return new ResponseObject<PaginationResponse<Entities.NhanVien>>(Response, "Lấy dữ liệu thành công");
            }
            catch (Exception exception)
            {
                return new ResponseError(Code.InternalServerError, exception.ToString());
            }
        }

        public async Task<Response> GetById(Guid Id)
        {
            var nhanvien = await _context.NhanVien.FirstOrDefaultAsync(c => c.Id == Id);
            return new ResponseObject<Entities.NhanVien>(nhanvien);
        }

        public async Task<Response> Post(Entities.NhanVien NhanVien)
        {
            try
            {
                var nv = await _context.NhanVien.FirstOrDefaultAsync(c => c.Ma == NhanVien.Ma);

                if (nv != null)
                {
                    return new ResponseError(Code.BadRequest,"Mã nhân viên đã tồn tại");
                }

                _context.NhanVien.Add(NhanVien);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new Response(Code.OK, "Thêm mới thành công");
                }

                return new ResponseError(Code.BadRequest, "Thêm mới thất bại");
            }
            catch (Exception exception)
            {
                return new ResponseError(Code.InternalServerError,exception.ToString());
            }
        }

        public async Task<Response> Put(Entities.NhanVien NhanVien)
        {
            try
            {
                _context.NhanVien.Update(NhanVien);
                var result = _context.SaveChanges();

                if (result > 0)
                {
                    return new Response(Code.OK, "Sửa thành công");
                }

                return new ResponseError(Code.BadRequest, "Sửa thất bại");
            }
            catch (Exception exception)
            {
                return new ResponseError(Code.InternalServerError, exception.ToString());
            }
        }
    }
}
