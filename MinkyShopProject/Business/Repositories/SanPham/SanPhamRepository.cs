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

        public async Task<bool> AddAsync(string name)
        {
            try
            {
                await _context.SanPham.AddAsync(new Entities.SanPham() { Ten = name });
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

        public async Task<List<SanPhamModel>> GetAsync()
        {
            return await Task.FromResult(_mapper.Map<List<Entities.SanPham>, List<SanPhamModel>>(await _context.SanPham.ToListAsync()));
        }

        public async Task<bool> UpdateAsync(Guid id, string name, TrangThaiSanPham trangThai)
        {
            try
            {
                var sanPham = await _context.SanPham.FindAsync(id);
                if (sanPham != null)
                {
                    sanPham.Ten = name;
                    sanPham.TrangThai = trangThai;
                    _context.SanPham.Update(sanPham);
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
