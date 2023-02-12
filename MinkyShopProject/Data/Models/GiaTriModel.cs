using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Models
{
    public class GiaTriModel
    {
        public Guid Id { get; set; }

        public Guid IdThuocTinh { get; set; }

        public string Ten { get; set; } = null!;
    }

    public class GiaTriCreateModel
    {
        public string Ten { get; set; } = null!;
    }

    public class GiaTriUpdateModel
    {
        public string Ten { get; set; } = null!;
    }
}
