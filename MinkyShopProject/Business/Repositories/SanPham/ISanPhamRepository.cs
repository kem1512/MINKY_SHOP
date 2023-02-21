using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public interface ISanPhamRepository
    {
        public Task<List<SanPhamModel>> GetAsync();

        public Task<bool> UpdateAsync(Guid id, string name, TrangThaiSanPham TrangThai);

        public Task<bool> DeleteAsync(Guid id);

        public Task<bool> AddAsync(string name);
    }
}
