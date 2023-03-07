using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.NhomSanPham
{
    public class NhomSanPhamRepository : INhomSanPhamRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public NhomSanPhamRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(NhomSanPhamModel obj)
        {
            try
            {
                await _context.NhomSanPham.AddAsync(_mapper.Map<NhomSanPhamModel, Entities.NhomSanPham>(obj));
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
                var nhomSanPham = await _context.NhomSanPham.FindAsync(id);
                if (nhomSanPham != null)
                {
                    _context.NhomSanPham.Remove(nhomSanPham);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<NhomSanPhamModel>> GetAsync()
        {
            var result = from nsp in _context.NhomSanPham
                         join nsp2 in _context.NhomSanPham on nsp.Id equals nsp2.IdParent into nspc
                         from nsp2 in nspc.DefaultIfEmpty()
                         where nsp.IdParent == null
                         group new { nsp, nsp2 } by nsp.Id into nspc
                         select new NhomSanPhamModel
                         {
                             Id = nspc.First().nsp.Id,
                             Ten = nspc.First().nsp.Ten,
                             NgayTao = nspc.First().nsp.NgayTao,
                             TrangThai = nspc.First().nsp.TrangThai,
                             NhomSanPhamModels = _mapper.Map<List<Entities.NhomSanPham>, List<NhomSanPhamModel>>(nspc.Select(c => c.nsp2).ToList())
                         };
            return await result.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, NhomSanPhamModel obj)
        {
            try
            {
                var nhomSanPham = await _context.NhomSanPham.FindAsync(id);
                if (nhomSanPham != null)
                {
                    nhomSanPham.Ten = obj.Ten;
                    nhomSanPham.TrangThai = obj.TrangThai;
                    nhomSanPham.IdParent = obj.IdParent;
                    _context.NhomSanPham.Update(nhomSanPham);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
