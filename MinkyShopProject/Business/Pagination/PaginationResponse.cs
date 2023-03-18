using MinkyShopProject.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Data.Pagination
{
    public class PaginationResponse
    {
        public List<NhanVien> Data { get; set; } = new List<NhanVien>();

        public int Pages { get; set; }

        public int CurrentPage { get; set; }
    }
}
