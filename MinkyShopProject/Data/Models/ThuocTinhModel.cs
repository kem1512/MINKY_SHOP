using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public class ThuocTinhModel
    {
        public Guid Id { get; set; }

        public string Ten { get; set; } = null!;

        public TrangThaiThuocTinh TrangThai { get; set; }

        public DateTime NgayTao { get; set; }
    }

    public class ThuocTinhCreateModel
    {
        public string Ten { get; set; } = null!;

        public TrangThaiThuocTinh TrangThai { get; set; }
    }

    public class ThuocTinhUpdateModel
    {
        public string Ten { get; set; } = null!;

        public TrangThaiThuocTinh TrangThai { get; set; }
    }
}
