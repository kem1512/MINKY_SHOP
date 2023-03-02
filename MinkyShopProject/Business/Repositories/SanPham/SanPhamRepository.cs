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
                    if (obj.Id != Guid.Empty)
                    {
                        if(obj.TheLoais != null)
                        {
                            foreach (var x in obj.TheLoais)
                            {
                                if(!_context.SanPham.Any(c => c.Id == x.Id))
                                {
                                    x.IdTheLoai = obj.Id;
                                    await _context.SanPham.AddAsync(_mapper.Map<SanPhamCreateModel, Entities.SanPham>(x));
                                }
                            }
                            await _context.SaveChangesAsync();
                        }
                    }
                    else
                    {
                        obj.Id = Guid.NewGuid();
                        await _context.SanPham.AddAsync(_mapper.Map<SanPhamCreateModel, Entities.SanPham>(obj));
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
                         join tl in _context.SanPham on sp.Id equals tl.IdTheLoai into spc
                         from tl in spc.DefaultIfEmpty()
                         where sp.IdTheLoai == null
                         group new { sp, tl } by sp.Id into spc
                         select new SanPhamModel
                         {
                             Id = spc.First().sp.Id,
                             Ten = spc.First().sp.Ten,
                             NgayTao = spc.First().sp.NgayTao,
                             TrangThai = spc.First().sp.TrangThai,
                             TheLoais = _mapper.Map<List<Entities.SanPham>, List<SanPhamModel>>(spc.Select(c => c.tl).ToList())
                         };
            return await result.ToListAsync();
        }

    public async Task<bool> UpdateAsync(Guid id, SanPhamUpdateModel obj)
        {
            try
            {
                if (obj != null)
                {
                    if (obj.Id != Guid.Empty)
                    {
                        _context.SanPham.Update(_mapper.Map<SanPhamUpdateModel, Entities.SanPham>(obj));

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
