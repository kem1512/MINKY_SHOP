using AutoMapper;
using AutoMapper.Internal;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
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

		public async Task<Response> AddAsync(HoaDonCreateModel obj)
		{
			try
			{
				if (obj == null)
					return new ResponseError(HttpStatusCode.BadRequest, "Thêm Thất Bại");

				obj.Ma = "HD" + Helper.RandomString(5);

				var hoaDon = _mapper.Map<HoaDonCreateModel, Entities.HoaDon>(obj);

				await _context.HoaDon.AddAsync(hoaDon);

				foreach (var x in obj.HoaDonChiTiets)
				{
					var bienThe = _context.BienThe.FirstOrDefault(c => c.Id == x.IdBienThe);
					if (bienThe != null)
						bienThe.SoLuong -= x.SoLuong;
				}

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
				var result = await _context.HoaDon.Include(c => c.VoucherLogs).ThenInclude(c => c.Voucher).Include(c => c.NhanVien).Include(c => c.KhachHang).Include(c => c.HinhThucThanhToans).Include(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe).ThenInclude(c => c.SanPham).FirstOrDefaultAsync(c => c.Id == id);
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
				return new ResponsePagination<HoaDonModel>(_mapper.Map<Pagination<Entities.HoaDon>, Pagination<HoaDonModel>>(await _context.HoaDon.Include(c => c.VoucherLogs).ThenInclude(c => c.Voucher).Include(c => c.NhanVien).Include(c => c.KhachHang).Include(c => c.HinhThucThanhToans).Include(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe.BienTheChiTiets).ThenInclude(c => c.GiaTri).Where(c => c.LoaiDonHang == obj.LoaiHoaDon || (c.LoaiDonHang < 5 && obj.LoaiHoaDon == null)).Where(c => c.TrangThaiGiaoHang == obj.TrangThaiGiaoHang || (c.TrangThaiGiaoHang < 20 && obj.TrangThaiGiaoHang == null)).Where(c => c.Ma == obj.Ma || c.TenNguoiNhan.ToLower().Trim().Contains(!string.IsNullOrEmpty(obj.Ma) ? obj.Ma.ToLower().Trim() : "")).Where(c => (obj.IdKhachHang != null && c.IdKhachHang == obj.IdKhachHang) || c.LoaiDonHang < 5).AsNoTracking().AsQueryable().GetPageAsync(obj)));
			}
			catch (Exception e)
			{
				Log.Error(e, "Lấy dữ liệu thất bại");
				return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
			}
		}

		public async Task<Response> GetHoaDonKhachHangAsync(Guid id)
		{
			try
			{
				return new ResponseList<HoaDonModel>(_mapper.Map<List<Entities.HoaDon>, List<HoaDonModel>>(await _context.HoaDon.Include(c => c.VoucherLogs).ThenInclude(c => c.Voucher).Include(c => c.NhanVien).Include(c => c.KhachHang).Include(c => c.HinhThucThanhToans).Include(c => c.HoaDonChiTiets).ThenInclude(c => c.BienThe.BienTheChiTiets).ThenInclude(c => c.GiaTri).Where(c => c.IdKhachHang == id).AsNoTracking().ToListAsync()));
			}
			catch (Exception e)
			{
				Log.Error(e, "Lấy dữ liệu thất bại");
				return new ResponseError(HttpStatusCode.InternalServerError, "Có lỗi trong quá trình xử lý : " + e.Message);
			}
		}

		public async Task<Response> UpdateAsync(Guid id, HoaDonCreateModel obj)
		{
			try
			{
				if (obj == null)
					return new ResponseError(HttpStatusCode.BadRequest, "Giá trị trả về không hợp lệ");

				obj.NhanVien = null;

				var hoaDon = _context.HoaDon.AsNoTracking().Include(c => c.HoaDonChiTiets).AsNoTracking().FirstOrDefault(c => c.Id == id);

				if (hoaDon == null)
					return new ResponseError(HttpStatusCode.BadRequest, "Không tìm thấy giá trị");

				var result = hoaDon.HoaDonChiTiets.Where(p => !obj.HoaDonChiTiets.Any(l => p.IdBienThe == l.IdBienThe)).ToList();

				foreach (var x in result)
				{
					_context.HoaDonChiTiet.Remove(x);
				}

				hoaDon = _mapper.Map<HoaDonCreateModel, Entities.HoaDon>(obj);

				hoaDon.VoucherLogs = new List<Entities.VoucherLog>();

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
