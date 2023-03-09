using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;
using System.Xml.Linq;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public SanPhamRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(SanPhamModel obj)
        {
            try
            {
                await _context.SanPham.AddAsync(_mapper.Map<SanPhamModel, Entities.SanPham>(obj));
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
                var sanPham = await _context.SanPham.FindAsync(id);
                if (sanPham != null)
                {
                    _context.SanPham.Remove(sanPham);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<SanPhamModel> FindAsync(Guid id)
        {
            var result = await _context.SanPham.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<Entities.SanPham, SanPhamModel>(result ?? new Entities.SanPham());
        }

        public async Task<List<SanPhamModel>> GetAsync()
        {
            var bienThes = from sp in _context.SanPham
                           join bt in _context.BienThe on sp.Id equals bt.IdSanPham
                           group new { sp, bt } by sp.Id into spc
                           select new SanPhamModel()
                           {
                               Anh = spc.First().sp.Anh,
                               Id = spc.First().sp.Id,
                               IdNhomSanPham = spc.First().sp.IdNhomSanPham,
                               Ma = spc.First().sp.Ma,
                               NgayTao = spc.First().sp.NgayTao,
                               Ten = spc.First().sp.Ten,
                               TrangThai = spc.First().sp.TrangThai,
                               Gia = 0,
                               BienThes = _mapper.Map<List<Entities.BienThe>, List<BienTheModel>>(spc.Select(c => c.bt).ToList())
                           };
            return await bienThes.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, SanPhamModel obj)
        {
            try
            {
                if (obj != null)
                {
                    if (obj.Id != Guid.Empty)
                    {
                        _context.SanPham.Update(_mapper.Map<SanPhamModel, Entities.SanPham>(obj));

                        await _context.SaveChangesAsync();
                    }
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
