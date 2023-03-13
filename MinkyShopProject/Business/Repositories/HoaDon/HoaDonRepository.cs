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

        public async Task<Response> GetAsync(HoaDonModel obj)
        {
            try
            {
                return new ResponsePagination<HoaDonModel>(_mapper.Map<Pagination<Entities.HoaDon>, Pagination<HoaDonModel>>(await _context.HoaDon.Include(c => c.HoaDonChiTiets).AsQueryable().GetPageAsync(obj)));
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

                hoaDon.TrangThai = obj.TrangThai;

                hoaDon.NgayNhan = obj.NgayNhan;

                hoaDon.NgayGiaoHang = obj.NgayGiaoHang;

                hoaDon.NgayThanhToan = obj.NgayThanhToan;

                if (obj.NhomSanPhams != null && obj.NhomSanPhams.Count() > 0)
                {
                    nhomSanPham.NhomSanPhams = _mapper.Map<List<NhomSanPhamModel>, List<Entities.NhomSanPham>>(obj.NhomSanPhams);
                }

                _context.NhomSanPham.Update(_mapper.Map<NhomSanPhamModel, Entities.NhomSanPham>(obj));

                var status = await _context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _mapper.Map<Entities.NhomSanPham, NhomSanPhamModel>(nhomSanPham);
                    return new ResponseObject<NhomSanPhamModel>(data, "Cập nhật thành công");
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
