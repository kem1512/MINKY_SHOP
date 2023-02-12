using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.ViewModels
{
    public class ThuocTinhViewModel
    {
        public ThuocTinhModel? ThuocTinh { get; set; }

        public IEnumerable<GiaTriModel> GiaTris { get; set; } = null!;
    }

    public class ThuocTinhViewCreateModel
    {
        public ThuocTinhCreateModel ThuocTinh { get; set; } = null!;

        public IEnumerable<GiaTriCreateModel> GiaTris { get; set; } = null!;
    }

    public class ThuocTinhViewUpdateModel
    {
        public ThuocTinhUpdateModel? ThuocTinh { get; set; }

        public GiaTriUpdateModel? GiaTri { get; set; }
    }
}
