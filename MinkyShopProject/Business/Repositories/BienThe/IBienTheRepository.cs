using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.BienThe
{
    public interface IBienTheRepository
    {
        public Task<bool> AddAsync(BienTheCreateModel obj);

        public Task<bool> DeleteAsync(Guid id);

        public Task<bool> UpdateAsync(Guid id, BienTheModel obj);

        public Task<List<BienTheModel>> GetAsync();

		public Task<BienTheChiTietModel> FindAsync(Guid id);
	}
}
