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

        public Guid IdKhachHang { get; set; }

        public DateTime NgayTao { get; set; }
    }
}
