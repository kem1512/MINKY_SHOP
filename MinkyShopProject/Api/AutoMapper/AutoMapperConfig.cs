using AutoMapper;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.ViewModels;

namespace MinkyShopProject.Api.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<SanPham, SanPhamModel>().ReverseMap();
            CreateMap<ThuocTinh, ThuocTinhModel>().ReverseMap();
            CreateMap<GiaTri, GiaTriModel>().ReverseMap();
            CreateMap<ThuocTinhCreateModel, ThuocTinh>().ReverseMap();
            CreateMap<GiaTriCreateModel, GiaTri>().ReverseMap();
        }
    }
}
