using MinkyShopProject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public class GioHangModel
    {
        public Guid Id { get; set; }

        public Guid IdKhachHang { get; set; } = Guid.Parse("68ad5c67-81d8-4f1f-a596-5d7fd7eda692");

        public DateTime NgayTao { get; set; }

        public List<GioHangChiTietModel>? GioHangChiTiets { get; set; }
    }

    public class GioHangChiTietModel
    {
        public Guid Id { get; set; }

        public Guid IdBienThe { get; set; }

        public Guid IdGioHang { get; set; }

        public int SoLuong { get; set; }

        public int SoLuongLucDau { get; set; }

        public float DonGia { get; set; }

        public BienTheModel? BienThe { get; set; }
    }

    public class GioHangQueryModel : PaginationRequest
    {

    }
}
