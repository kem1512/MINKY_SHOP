using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.ViewModels;

namespace MinkyShopProject.Business.Repositories.ThuocTinh
{
    public interface IThuocTinhRepository
    {
        public Task<IEnumerable<ThuocTinhViewModel>> GetAsync();

        public Task<bool> AddAsync(ThuocTinhViewCreateModel obj);

        public Task<bool> AddRangeAsync(IEnumerable<ThuocTinhViewCreateModel> obj);

        public Task<bool> UpdateAsync(Guid id, ThuocTinhViewUpdateModel obj);

        public Task<bool> DeleteAsync(Guid id);

        public Task<bool> DeleteRangeAsync(Guid[] ids);
    }
}
