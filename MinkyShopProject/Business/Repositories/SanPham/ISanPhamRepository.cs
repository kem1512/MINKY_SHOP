using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public interface ISanPhamRepository
    {
        public Task<SanPhamModel[]> GetAsync();

        public Task<object> AddAsync(Guid idSanPham, SanPhamCreateModel[] obj);

        public Task<bool> UpdateAsync();

        public Task<bool> DeleteAsync(Guid id);
    }
}
