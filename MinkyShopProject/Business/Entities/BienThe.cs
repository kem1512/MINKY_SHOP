﻿using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class BienThe
    {
        public Guid Id { get; set; }

        public Guid IdSanPham { get; set; }

        public int SoLuong { get; set; }

        public float GiaBan { get; set; }

        public string Sku { get; set; } = null!;

        public string Anh { get; set; } = null!;

        public DateTime NgayTao { get; set; }

        public TrangThaiBienThe TrangThai { get; set; }

        public SanPham SanPham { get; set; } = null!;

        public IEnumerable<BienTheChiTiet> BienTheChiTiets { get; set; } = null!;
    }
}