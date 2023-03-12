﻿using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class SanPham
    {
        public Guid Id { get; set; }

        public Guid? IdNhomSanPham { get; set; }

        public string Ten { get; set; } = null!;

        public string Ma { get; set; } = null!;

        public string Anh { get; set; } = null!;

        public int TrangThai { get; set; }

        public DateTime NgayTao { get; set; }

        public MoTa MoTa { get; set; } = null!;

        public NhomSanPham NhomSanPham { get; set; } = null!;

        public List<BienThe> BienThes { get; set; } = null!;

        public List<ThuocTinhSanPham> ThuocTinhSanPhams { get; set; } = null!;
    }
}
