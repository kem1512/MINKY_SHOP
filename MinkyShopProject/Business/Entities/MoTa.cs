using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Entities
{
    public class MoTa
    {
        public Guid Id { get; set; }

        public Guid IdSanPham { get; set; }

        public string? Anh { get; set; }

        public SanPham SanPham { get; set; } = null!;
    }
}
