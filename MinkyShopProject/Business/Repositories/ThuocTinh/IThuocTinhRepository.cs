using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Business.Repositories.ThuocTinh
{
    public interface IThuocTinhRepository
    {
        public Task<IEnumerable<ThuocTinhModel>> GetAsync();

        public Task<bool> AddAsync(ThuocTinhCreateModel obj);

        public Task<bool> AddRangeAsync(ThuocTinhCreateModel[] obj);

        public Task<bool> UpdateAsync(Guid id, ThuocTinhUpdateModel obj);

        public Task<bool> DeleteAsync(Guid id);

        public Task<bool> DeleteRangeAsync(Guid[] ids);
    }
}
