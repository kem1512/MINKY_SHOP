using MinkyShopProject.Common;
using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public class ThuocTinhModel
    {
        public Guid Id { get; set; }

        public string? Ten { get; set; }

        public int TrangThai { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public List<GiaTriModel>? GiaTris { get; set; } = new List<GiaTriModel>();

        public List<GiaTriModel>? GiaTriTemplates { get; set; } = new List<GiaTriModel>();
    }

    public class GiaTriModel
    {
        public Guid Id { get; set; }

        public Guid IdThuocTinh { get; set; }

        public string? Ten { get; set; }

        public ThuocTinhModel? ThuocTinh { get; set; }
    }

    public class ThuocTinhQueryModel : PaginationRequest
    {
        public string? Ten { get; set; }

        public int? TrangThai { get; set; }
    }
}
