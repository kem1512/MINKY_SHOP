﻿using MinkyShopProject.Common;
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
        public Task<Response> GetAsync(HoaDonQueryModel obj);

        public Task<Response> FindAsync(Guid id);

        public Task<Response> AddAsync(HoaDonModel obj);

        public Task<Response> UpdateAsync(Guid id, HoaDonModel obj);

        public Task<Response> DeleteAsync(Guid id);
    }
}
