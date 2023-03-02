﻿using MinkyShopProject.Data.Enums;

namespace MinkyShopProject.Data.Models
{
    public class SanPhamModel
    {
        public Guid Id { get; set; }

        public Guid? IdTheLoai { get; set; }

        public string Ten { get; set; } = null!;

        public TrangThaiSanPham TrangThai { get; set; }

        public DateTime NgayTao { get; set; }

        public List<SanPhamModel>? SanPhamModels { get; set; } = null!;
    }

    public class SanPhamCreateModel
    {
        public Guid IdThuocTinh { get; set; }

        public string TenThuocTinh { get; set; } = null!;

        public GiaTriModel[] GiaTris { get; set; } = null!;
    }

    // Thuộc Tính Sản Phẩm

    public class ThuocTinhSanPhamCreateModel
    {
        public Guid Id { get; set; }

        public Guid IdSanPham { get; set; }

        public Guid IdThuocTinh { get; set; }
    }

    // Biến Thể

    public class BienTheModel
    {
        public Guid Id { get; set; }

        public string Ten { get; set; } = null!;

        public string Sku { get; set; } = null!;

        public float GiaBan { get; set; }

        public string Anh { get; set; } = null!;

        public int SoLuong { get; set; }

        public string GiaTri { get; set; } = null!;
    }

    public class BienTheCreateModel
    {
        public Guid Id { get; set; }

        public Guid IdSanPham { get; set; }

        public string Sku { get; set; } = null!;
    }

    // Biến Thể Chi Tiêt

    public class BienTheChiTietCreateModel
    {
        public Guid IdBienThe { get; set; }

        public Guid IdGiaTri { get; set; }

        public Guid IdThuocTinhSanPham { get; set; }
    }
}
