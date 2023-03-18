using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.NhanVien
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly MinkyShopDbContext _context;

        public NhanVienRepository(MinkyShopDbContext context)
        {
            _context = context;
        }

        public async Task<string> Delete(Guid Id)
        {
            try
            {
                var nhanvienhoadon = await _context.HoaDon.FirstOrDefaultAsync(c => c.IdNhanVien == Id);
                var nhanvien = await _context.NhanVien.FirstOrDefaultAsync(c => c.Id == Id);


                if (nhanvienhoadon != null)
                {
                    return "Nhân viên này tồn tại hóa đơn không thể xóa";
                }

                if (nhanvien.TrangThai != 0)
                {
                    return "Không thể xóa nhân viên còn hoạt động";
                }

                _context.NhanVien.Remove(nhanvien);
                _context.SaveChanges();
                return "Xóa thành công";
            }
            catch (Exception)
            {
                return "Xóa thất bại";
            }
        }

        public async Task<PaginationResponse> Get(int perPage, int currentPage, int status, string? keyword)
        {
            List<Entities.NhanVien> NhanViens = new List<Entities.NhanVien>();

            int pageResults = perPage;
            int pageCount = (_context.NhanVien.Count() / pageResults);

            if (keyword == null)
            {
                NhanViens = await _context.NhanVien
                .Where(c => c.TrangThai == status)
                .Skip((currentPage - 1) * pageResults)
                .Take(pageResults)
                .ToListAsync();
            }
            else
            {
                NhanViens = await _context.NhanVien
               .Where(c => c.TrangThai == status && c.Ten.Contains(keyword) || c.Ma.ToLower().Contains(keyword) || c.DiaChi.ToLower().Contains(keyword))
               .Skip((currentPage - 1) * pageResults)
               .Take(pageResults)
               .ToListAsync();
            }

            var Response = new PaginationResponse()
            {
                Data = NhanViens,
                CurrentPage = currentPage,
                Pages = pageCount,
            };

            return Response;
        }

        public async Task<Entities.NhanVien> GetById(Guid Id)
        {
            return _context.NhanVien.FirstOrDefault(c => c.Id == Id);
        }

        public async Task<string> Post(Entities.NhanVien NhanVien)
        {
            try
            {
                var nv = _context.NhanVien.FirstOrDefaultAsync(c => c.Ma == NhanVien.Ma);

                if (nv != null)
                {
                    return "Mã nhân viên đã tồn tại";
                }

                _context.NhanVien.Add(NhanVien);
                _context.SaveChanges();
                return "Thêm thành công";
            }
            catch (Exception)
            {
                return "Thêm thất bại";
            }
        }

        public async Task<string> Put(Entities.NhanVien NhanVien)
        {
            try
            {
                _context.NhanVien.Update(NhanVien);
                _context.SaveChanges();
                return "Sửa thành công";
            }
            catch (Exception)
            {
                return "Sửa thất bại";
            }
        }
    }
}
