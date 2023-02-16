using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class ThuocTinh
    {
        public Guid Id { get; set; }

        public string Ten { get; set; } = null!;

        public TrangThaiThuocTinh TrangThai { get; set; }

        public DateTime NgayTao { get; set; }

        public IEnumerable<ThuocTinhSanPham> ThuocTinhSanPhams { get; set; } = null!;

        public IEnumerable<GiaTri> GiaTris { get; set; } = null!;
    }
}
