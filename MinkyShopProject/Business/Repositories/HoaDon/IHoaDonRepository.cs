using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.HoaDon
{
    public interface IHoaDonRepository
    {
        public Task<Response> GetAsync(HoaDonModel obj);

        public Task<Response> AddAsync(HoaDonModel obj);

        public Task<Response> UpdateAsync(Guid id, HoaDonModel obj);

        public Task<Response> DeleteAsync(Guid id);
    }
}
