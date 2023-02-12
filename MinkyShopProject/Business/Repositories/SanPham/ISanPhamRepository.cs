using MinkyShopProject.Data;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public interface ISanPhamRepository
    {
        public Task<IEnumerable<SanPhamModel>> GetSanPhams();
    }
}
