using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class KhachHang
    {
        public Guid Id { get; set; }

        public Guid IdViDiem { get; set; }

        public string Ma { get; set; } = null!;

        public string Ten { get; set; } = null!;

        public string Anh { get; set; } = null!;

        public bool GioiTinh { get; set; }

        public DateTime NgaySinh { get; set; }

        public string DiaChi { get; set; } = null!;

        public string Sdt { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string MatKhau { get; set; } = null!;

        public int SoLanMua { get; set; }

        public TrangThaiKhachHang TrangThai { get; set; }

        public List<DanhGia> DanhGias { get; set; } = null!;
    }
}
