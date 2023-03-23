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

namespace MinkyShopProject.Business.Repositories.HoaDon
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public HoaDonRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response> AddAsync(HoaDonModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");

                obj.Ma = "HD" + Helper.RandomString(5);

                var hoaDon = _mapper.Map<HoaDonModel, Entities.HoaDon>(obj);

                await _context.HoaDon.AddAsync(hoaDon);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.HoaDon, HoaDonModel>(hoaDon);
                    return new ResponseObject<HoaDonModel>(data, "Thêm thành công");
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
                var hoaDon = await _context.HoaDon.FirstOrDefaultAsync(c => c.Id == id);

                if (hoaDon == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                _context.HoaDon.Remove(hoaDon);

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
                var result = await _context.HoaDon.Include(c => c.NhanVien).Include(c => c.KhachHang).Include(c => c.HinhThucThanhToans).Include(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe).ThenInclude(c => c.SanPham).FirstOrDefaultAsync(c => c.Id == id);
                if (result != null)
                    return new ResponseObject<HoaDonModel>(_mapper.Map<Entities.HoaDon, HoaDonModel>(result));
                return new ResponseError(HttpStatusCode.InternalServerError, "Không Tìm Thấy");
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> GetAsync(HoaDonQueryModel obj)
        {
            try
            {
                return new ResponsePagination<HoaDonModel>(_mapper.Map<Pagination<Entities.HoaDon>, Pagination<HoaDonModel>>(await _context.HoaDon.Include(c => c.NhanVien).Include(c => c.KhachHang).Include(c => c.HinhThucThanhToans).Include(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe).ThenInclude(c => c.BienTheChiTiets).AsQueryable().GetPageAsync(obj)));
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu thất bại");
                return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
            }
        }

        public async Task<Response> UpdateAsync(Guid id, HoaDonModel obj)
        {
            try
            {
                if (obj == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Giá trị trả về không hợp lệ");

                var hoaDon = _context.HoaDon.AsNoTracking().FirstOrDefault(c => c.Id == id);

                if (hoaDon == null)
                    return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

                hoaDon = _mapper.Map<HoaDonModel, Entities.HoaDon>(obj);

                _context.HoaDon.Update(hoaDon);

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.HoaDon, HoaDonModel>(hoaDon);
                    return new ResponseObject<HoaDonModel>(data, "Cập nhật thành công");
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
