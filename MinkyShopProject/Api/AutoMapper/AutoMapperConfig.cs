using AutoMapper;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ThuocTinh, ThuocTinhModel>().ReverseMap();

            CreateMap<NhomSanPham, NhomSanPhamModel>().ReverseMap();

            CreateMap<Pagination<KhachHang>, Pagination<KhachHangModel>>().ReverseMap();

            CreateMap<Pagination<NhomSanPham>, Pagination<NhomSanPhamModel>>().ReverseMap();

            CreateMap<Pagination<SanPham>, Pagination<SanPhamModel>>().ReverseMap();

            CreateMap<Pagination<ThuocTinh>, Pagination<ThuocTinhModel>>().ReverseMap();

            CreateMap<GiaTri, GiaTriModel>().ReverseMap();

            CreateMap<BienTheCreateModel, BienThe>().ReverseMap();

            CreateMap<BienTheCreateModel, BienTheModel>().ReverseMap();

            CreateMap<BienTheModel, BienThe>().ReverseMap();

            CreateMap<GioHang, GioHangModel>().ReverseMap();

            CreateMap<SanPham, SanPhamModel>().ReverseMap();

            CreateMap<NhomSanPham, NhomSanPhamModel>().ReverseMap();

            CreateMap<ViDiem, ViDiemModel>().ReverseMap();

            CreateMap<KhachHang, KhachHangModel>().ReverseMap();
        }
    }
}
