﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;
using Serilog;
using System.Net;
using System.Xml.Linq;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public SanPhamRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            try
            {
                var sanPham = await _context.SanPham.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (sanPham == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                _context.SanPham.Remove(sanPham);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    return new ResponseError(HttpStatusCode.OK, "Xóa thành công");
                }

                return new ResponseError(HttpStatusCode.BadRequest, "Xóa Thất Bại");
            }
            catch (Exception e)
            {
                Log.Error(e, string.Empty);
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> FindAsync(Guid id)
        {
            try
            {
                return new ResponseObject<SanPhamModel>(_mapper.Map<Entities.SanPham, SanPhamModel>(await _context.SanPham.Include(c => c.NhomSanPham).Include(c => c.BienThes).FirstAsync(c => c.Id == id)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> GetAsync(SanPhamQueryModel obj)
        {
            try
            {
                return new ResponsePagination<SanPhamModel>(_mapper.Map<Pagination<Entities.SanPham>, Pagination<SanPhamModel>>(await _context.SanPham.Include(c => c.NhomSanPham).Include(c => c.BienThes).AsQueryable().GetPageAsync(obj)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> UpdateAsync(Guid id, SanPhamModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Giá trị trả về không hợp lệ");

                var sanPham = _context.SanPham.AsNoTracking().FirstOrDefault(c => c.Id == id);

                if (sanPham == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                sanPham.Ten = obj.Ten;

                sanPham.Anh = obj.Anh;

                sanPham.TrangThai = obj.TrangThai;

                sanPham.IdNhomSanPham = obj.IdNhomSanPham;

                _context.SanPham.Update(sanPham);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.SanPham, SanPhamModel>(sanPham);
                    return new ResponseObject<SanPhamModel>(obj, "Cập nhật thành công");
                }

                return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
