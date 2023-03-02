using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;
using System.Xml.Linq;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public static class Test
    {
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> result = new[] { Enumerable.Empty<T>() };
            foreach (var sequence in sequences)
            {
                var localSequence = sequence;
                result = result.SelectMany(
                  _ => localSequence,
                  (seq, item) => seq.Concat(new[] { item })
                );
            }
            return result;
        }
    }

    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public SanPhamRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(SanPhamCreateModel obj)
        {
            try
            {
                if(obj != null)
                {
                    obj.Id = Guid.NewGuid();
                    await _context.SanPham.AddAsync(_mapper.Map<SanPhamCreateModel, Entities.SanPham>(obj));
                    await _context.SaveChangesAsync();
                }
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

        public async Task<List<SanPhamModel>> GetAsync()
        {
            var result = from sp in _context.SanPham
                         join tl in _context.SanPham on sp.Id equals tl.IdTheLoai
                         into spc from sp_null in spc.DefaultIfEmpty()
                         select new SanPhamModel
                         {
                             Id = sp.Id,
                             Ten = sp.Ten,
                             NgayTao = sp.NgayTao,
                             TrangThai = sp.TrangThai,
                             //TheLoais = _mapper.Map<List<Entities.SanPham>, List<SanPhamModel>>(spc.Select(c => c).ToList())
                         };
            return await result.ToListAsync();
        }

    public async Task<bool> UpdateAsync(Guid id, SanPhamUpdateModel obj)
        {
            try
            {
                var sanPham = await _context.SanPham.FindAsync(id);
                if (sanPham != null)
                {
                    _context.SanPham.Update(_mapper.Map<SanPhamUpdateModel, Entities.SanPham>(obj));
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
