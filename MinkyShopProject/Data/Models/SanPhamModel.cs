using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public class SanPhamModel
    {
        public Guid Id { get; set; }

        public string Ten { get; set; } = null!;

        public TrangThaiChung TrangThai { get; set; }

        public DateTime NgayTao { get; set; }
    }

    public class SanPhamCreateModel
    {
        public string Ten { get; set; } = null!;

        public TrangThaiChung TrangThai { get; set; }

    }

    public class SanPhamUpdateModel
    {
        public string Ten { get; set; } = null!;

        public TrangThaiChung TrangThai { get; set; }
    }
}
