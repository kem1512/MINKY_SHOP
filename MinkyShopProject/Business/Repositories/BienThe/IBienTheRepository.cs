﻿using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.BienThe
{
    public interface IBienTheRepository
    {
        public Task<bool> AddAsync(Guid idSanPham, string tenSanPham, SanPhamCreateModel[] obj);

        public Task<bool> DeleteAsync(Guid id);

        public Task<bool> UpdateAsync(Guid id, BienTheModel obj);

        public Task<List<BienTheModel>> GetAsync();
    }
}