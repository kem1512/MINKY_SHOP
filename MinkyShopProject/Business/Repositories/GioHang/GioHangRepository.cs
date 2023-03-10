using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.GioHang
{
    public class GioHangRepository : IGioHangRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public GioHangRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(GioHangModel obj)
        {
            try
            {
                await _context.GioHang.AddAsync(_mapper.Map<GioHangModel, Entities.GioHang>(obj));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var gioHang = await _context.GioHang.FindAsync(id);
                if (gioHang != null)
                {
                    _context.GioHang.Remove(gioHang);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GioHangModel>> GetAsync()
        {
            return _mapper.Map<List<Entities.GioHang>, List<GioHangModel>>(await _context.GioHang.ToListAsync());
        }

        public Task<bool> UpdateAsync(GioHangModel obj)
        {
            throw new NotImplementedException();
        }
    }
}
