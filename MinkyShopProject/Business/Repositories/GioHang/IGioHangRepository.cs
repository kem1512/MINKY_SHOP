using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.GioHang
{
    public interface IGioHangRepository
    {
        Task<bool> AddAsync(GioHangModel obj);

        Task<bool> UpdateAsync(GioHangModel obj);

        Task<bool> DeleteAsync(Guid id);

        Task<List<GioHangModel>> GetAsync();
    }
}
