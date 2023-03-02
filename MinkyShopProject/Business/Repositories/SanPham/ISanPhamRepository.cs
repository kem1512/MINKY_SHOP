using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public interface ISanPhamRepository
    {
        public Task<List<SanPhamModel>> GetAsync();

        public Task<bool> UpdateAsync(Guid id, SanPhamUpdateModel obj);

        public Task<bool> DeleteAsync(Guid id);

        public Task<bool> AddAsync(SanPhamCreateModel obj);
    }
}
