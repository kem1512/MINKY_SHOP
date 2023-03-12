using MinkyShopProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public class NhomSanPhamModel
    {
        public Guid Id { get; set; }

        public Guid? IdParent { get; set; }

        public string Ten { get; set; } = null!;

        public int TrangThai { get; set; }

        public DateTime NgayTao { get; set; }

        public List<NhomSanPhamModel> NhomSanPhams { get; set; } = new List<NhomSanPhamModel>();

        public NhomSanPhamModel? NhomSanPhamEntity { get; set; } = null!;
    }

    public class NhomSanPhamQueryModel : PaginationRequest
    {

    }
}
