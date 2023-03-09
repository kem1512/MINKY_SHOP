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

        public bool TrangThai { get; set; }

        public DateTime NgayTao { get; set; }

        public List<NhomSanPhamModel>? NhomSanPhams { get; set; } = null!;
    }
}
