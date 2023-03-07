﻿using AutoMapper;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ThuocTinhCreateModel, ThuocTinh>().ForMember(c => c.GiaTris, c => c.Ignore()).ReverseMap();

            CreateMap<GiaTri, GiaTriModel>().ReverseMap();

            CreateMap<BienTheCreateModel, BienThe>().ReverseMap();

            CreateMap<BienTheModel, BienThe>().ReverseMap();

            CreateMap<SanPham, SanPhamModel>().ReverseMap();

            CreateMap<NhomSanPham, NhomSanPhamModel>().ReverseMap();
        }
    }
}
