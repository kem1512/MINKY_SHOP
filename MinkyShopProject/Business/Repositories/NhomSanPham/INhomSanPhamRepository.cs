using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.NhomSanPham
{
    public interface INhomSanPhamRepository
    {
        public Task<List<NhomSanPhamModel>> GetAsync();

        public Task<bool> AddAsync(NhomSanPhamModel obj);

        public Task<bool> UpdateAsync(Guid id, NhomSanPhamModel obj);

        public Task<bool> DeleteAsync(Guid id);
    }
}
