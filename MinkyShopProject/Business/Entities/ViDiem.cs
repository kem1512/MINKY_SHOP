using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class ViDiem
    {
        public Guid Id { get; set; }

        public float TongDiem { get; set; }

        public float SoDiemDaDung { get; set; }

        public float SoDiemDaCong { get; set; }

        public TrangThaiViDiem TrangThai { get; set; }

        public KhachHang KhachHang { get; set; } = null!;
    }
}
