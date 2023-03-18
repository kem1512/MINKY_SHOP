using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.GioHang
{
    public class GioHangRepository : IGioHangRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public GioHangRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> AddAsync(GioHangModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");

                var gioHang = _mapper.Map<GioHangModel, Entities.GioHang>(obj);

                await _context.GioHang.AddAsync(gioHang);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.GioHang, GioHangModel>(gioHang);
                    return new ResponseObject<GioHangModel>(data, "Thêm thành công");
                }

                return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");
            }
            catch (Exception e)
            {
                Log.Error(e, string.Empty);
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            try
            {
                var gioHang = await _context.GioHang.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

                if (gioHang == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                if (gioHang.GioHangChiTiets != null && gioHang.GioHangChiTiets.Any())
                    _context.GioHangChiTiet.RemoveRange(gioHang.GioHangChiTiets);

                _context.GioHang.Remove(gioHang);

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

        public async Task<Response> GetAsync(Guid id, GioHangQueryModel obj)
        {
            try
            {
                return new ResponsePagination<GioHangModel>(_mapper.Map<Pagination<Entities.GioHang>, Pagination<GioHangModel>>(await _context.GioHang.Include(c => c.GioHangChiTiets).ThenInclude(c => c.BienThe).ThenInclude(c => c.SanPham).Where(c => c.IdKhachHang == id).AsQueryable().GetPageAsync(obj)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> UpdateAsync(Guid id, GioHangModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Giá trị trả về không hợp lệ");

                var gioHang = _context.GioHang.AsNoTracking().FirstOrDefault(c => c.Id == id);

                if (gioHang == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                if (obj.GioHangChiTiets != null && obj.GioHangChiTiets.Count() > 0)
                {
                    gioHang.GioHangChiTiets = _mapper.Map<List<GioHangChiTietModel>, List<Entities.GioHangChiTiet>>(obj.GioHangChiTiets);
                }

                _context.GioHang.Update(_mapper.Map<GioHangModel, Entities.GioHang>(obj));

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.GioHang, GioHangModel>(gioHang);
                    return new ResponseObject<GioHangModel>(data, "Cập nhật thành công");
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
