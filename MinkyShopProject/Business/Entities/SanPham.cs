using MinkyShopProject.Data.Enums;
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

        public string? Ten { get; set; }

        public TrangThaiSanPham TrangThai { get; set; }

        public DateTime NgayTao { get; set; }

        public IEnumerable<BienThe> BienThes { get; set; } = null!;

        public IEnumerable<ThuocTinhSanPham> ThuocTinhSanPhams { get; set; } = null!;
    }
}
