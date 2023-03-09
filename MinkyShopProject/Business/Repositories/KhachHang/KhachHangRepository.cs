using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using Newtonsoft.Json;
using Serilog;
using Code = System.Net.HttpStatusCode;

namespace MinkyShopProject.Business.Repositories.KhachHang
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly MinkyShopDbContext _Context;
        private readonly IMapper _Mapper;

        public KhachHangRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            ;
        }
        public async Task<Response> CreateKhachHang(KhachHangThemVaSuaModel model)
        {
            try
            {
                var entityModel = new Entities.KhachHang
                {
                    IdViDiem = model.IdViDiem,
                    Ma = model.Ma,
                    Ten = model.Ten,
                    Anh = model.Anh,
                    GioiTinh = model.GioiTinh,
                    NgaySinh = model.NgaySinh,
                    DiaChi = model.DiaChi,
                    Sdt = model.Sdt,
                    Email = model.Email,
                    MatKhau = model.MatKhau,
                    SoLanMua = model.SoLanMua
                };
                _Context.Add(entityModel);

                var status = await _Context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _Mapper.Map<Entities.KhachHang, KhachHangModel>(entityModel);
                    return new ResponseObject<KhachHangModel>(data, "Thêm thành công");
                }

                return new ResponseError(Code.BadRequest, "Thêm thất bại");
            }
            catch (Exception e)
            {
                Log.Error(e, string.Empty);
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> UpdateKhachHang(Guid Id, KhachHangThemVaSuaModel model)
        {
            try

            {
                var entityModel = await _Context.KhachHang.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (entityModel == null)
                {
                    return new ResponseError(Code.BadRequest, "không tìm thấy Id khách hàng");
                }
                entityModel.IdViDiem = model.IdViDiem;
                entityModel.Ma = model.Ma;
                entityModel.Ten = model.Ten;
                entityModel.Anh = model.Anh;
                entityModel.GioiTinh = model.GioiTinh;
                entityModel.NgaySinh = model.NgaySinh;
                entityModel.DiaChi = model.DiaChi;
                entityModel.Sdt = model.Sdt;
                entityModel.Email = model.Email;
                entityModel.MatKhau = model.MatKhau;
                entityModel.SoLanMua = model.SoLanMua;

                var status = await _Context.SaveChangesAsync();

                if (status > 0)
                {
                    var data = _Mapper.Map<Entities.KhachHang, KhachHangModel>(entityModel);
                    return new ResponseObject<KhachHangModel>(data, "Sửa thành công");
                }
                return new ResponseError(Code.BadRequest, "Sửa thất bại");
            }
            catch (Exception e)
            {
                Log.Error(e, string.Empty);
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> GetKhachHang(KhachHangQueryModel filter)
        {
            try
            {

                var result = _Context.KhachHang.GetPage(filter);
                var khachHangDto =
                    JsonConvert.DeserializeObject<Pagination<KhachHangModel>>(JsonConvert.SerializeObject(result));
                return new ResponsePagination<KhachHangModel>(khachHangDto);

            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu bộ phận không thành công");
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> GetbyIdKhachHang(Guid Id)
        {
            try
            {
                var entity = await _Context.KhachHang.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (entity == null)
                {
                    return new ResponseError(Code.BadRequest, "Không tìm thấy Id khách hàng");
                }

                var data = _Mapper.Map<Entities.KhachHang, KhachHangModel>(entity);
                return new ResponseObject<KhachHangModel>(data);
            }
            catch (Exception e)
            {
                Log.Error(e, "Lấy dữ liệu khách hàng thất bại");
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }

        public async Task<Response> DeleteKhachHang(Guid Id)
        {
            try
            {
                var entity = await _Context.KhachHang.Where(c => c.Id == Id).FirstOrDefaultAsync();
                if (entity == null)
                {
                    return new ResponseError(Code.BadRequest, "Không tìm thấy Id Khách Hàng");
                }
                _Context.Remove(entity);
                _Context.SaveChanges();

                var tile = entity.Ten;
                return new ResponseDelete(Code.OK, "Xóa thành công", Id, tile);
            }
            catch (Exception e)
            {
                Log.Error(e, string.Empty);
                return new ResponseError(Code.InternalServerError, "Có lỗi trong quá trình xử lý: " + e.Message);
            }
        }
    }
}
