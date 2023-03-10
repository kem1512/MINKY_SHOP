using MinkyShopProject.Common;
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

        public List<GiaTriModel> GiaTris { get; set; } = new List<GiaTriModel>();

        public List<GiaTriModel> GiaTriTemplates { get; set; } = new List<GiaTriModel>();
    }

    public class GiaTriModel
    {
        public Guid Id { get; set; }

        public Guid IdThuocTinh { get; set; }

        public string Ten { get; set; } = null!;
    }

    public class ThuocTinhQueryModel : PaginationRequest
    {
    }
}
