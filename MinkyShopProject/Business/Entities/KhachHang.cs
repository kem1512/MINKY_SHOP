﻿using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class KhachHang
    {
        public Guid Id { get; set; }

        public Guid? IdViDiem { get; set; }

        public string? Ma { get; set; }

        public string? Ten { get; set; }

        public string? Anh { get; set; }

        public bool GioiTinh { get; set; }

        public DateTime NgaySinh { get; set; }

        public string? DiaChi { get; set; }

        public string? Sdt { get; set; }

        public string? Email { get; set; }

        public string? MatKhau { get; set; }

        public int SoLanMua { get; set; }

        public int TrangThai { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public GioHang? GioHang { get; set; }

        public ViDiem? ViDiem { get; set; }

        public List<HoaDon>? HoaDons { get; set; }

        public List<DanhGia>? DanhGias { get; set; }
    }
}
