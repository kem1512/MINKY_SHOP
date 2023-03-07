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
                obj.Id = Guid.NewGuid();
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
            var result = from tt in _context.ThuocTinh
                         join gt in _context.GiaTri on tt.Id equals gt.IdThuocTinh
                         join ttsp in _context.ThuocTinhSanPham on tt.Id equals ttsp.IdThuocTinh
                         join sp in _context.SanPham on ttsp.IdSanPham equals sp.Id
                         join btct in _context.BienTheChiTiet on new { gt = gt.Id, ttsp = ttsp.Id } equals new { gt = btct.IdGiaTri, ttsp = btct.IdThuocTinhSanPham }
                         join bt in _context.BienThe on btct.IdBienThe equals bt.Id
                         group new { gt, bt, sp } by new
                         {
                             ttsp.IdSanPham,
                             sp.Id,
                             sp.Ten,
                             sp.TrangThai,
                             sp.NgayTao,
                             btct.IdBienThe,
                             bt.SoLuong,
                             bt.GiaBan,
                             bt.Sku,
                         } into btc
                         select new SanPhamModel
                         {
                             Id = btc.First().sp.Id,
                             Ten = btc.First().sp.Ten,
                             NgayTao = btc.First().sp.NgayTao,
                             TrangThai = btc.First().sp.TrangThai,
                             SoLuongBienThe = btc.Count()
                         };

            return await result.Distinct().ToListAsync();
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
