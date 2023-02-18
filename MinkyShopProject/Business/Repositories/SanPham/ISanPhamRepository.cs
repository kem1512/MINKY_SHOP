using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public interface ISanPhamRepository
    {
        public Task<List<BienTheModel>> GetAsync();

        public Task<List<SanPhamModel>> GetSanPhamAsync();

        public Task<bool> AddAsync(Guid idSanPham, string tenSanPham, SanPhamCreateModel[] obj);

        public Task<bool> DeleteAsync(Guid id);

        public Task<bool> AddSanPhamAsync(string name);
    }
}
