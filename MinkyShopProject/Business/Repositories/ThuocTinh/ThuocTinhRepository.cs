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

namespace MinkyShopProject.Business.Repositories.ThuocTinh
{
    public class ThuocTinhRepository : IThuocTinhRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public ThuocTinhRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(ThuocTinhCreateModel obj)
        {
            try
            {
                if (obj == null || obj.GiaTris == null) return false;

                //  Tạo Id Mới Trước Khi Thêm
                Guid id = Guid.NewGuid();

                // Map 2 Object Lại Với Nhau
                var thuocTinh = _mapper.Map<ThuocTinhCreateModel, Entities.ThuocTinh>(obj);

                // Gán Lại Id Cho Thuộc Tính
                thuocTinh.Id = id;

                await _context.ThuocTinh.AddAsync(thuocTinh);

                foreach (var x in obj.GiaTris)
                {
                    await _context.GiaTri.AddAsync(new GiaTri() { IdThuocTinh = id, Ten = x });
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddRangeAsync(ThuocTinhCreateModel[] obj)
        {
            try
            {
                if(obj == null || obj.Length == 0) return false;

                foreach (var x in obj)
                {
                    if (x == null || x.GiaTris == null) return false;

                    //  Tạo Id Mới Trước Khi Thêm
                    Guid id = Guid.NewGuid();

                    // Map 2 Object Lại Với Nhau
                    var thuocTinh = _mapper.Map<ThuocTinhCreateModel, Entities.ThuocTinh>(x);

                    // Gán Lại Id Cho Thuộc Tính
                    thuocTinh.Id = id;

                    await _context.ThuocTinh.AddAsync(thuocTinh);

                    foreach (var y in x.GiaTris)
                    {
                        await _context.GiaTri.AddAsync(new GiaTri() { IdThuocTinh = id, Ten = y });
                    }
                }

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
                var thuocTinh = await _context.ThuocTinh.FindAsync(id);
                var giaTri = await _context.GiaTri.FindAsync(id);

                if(thuocTinh != null)
                {
                    _context.ThuocTinh.Remove(thuocTinh);
                }

                if(giaTri != null)
                {
                    _context.GiaTri.Remove(giaTri);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteRangeAsync(Guid[] ids)
        {
            try
            {
                if (ids == null || ids.Count() <= 0) return false;

                foreach (var x in ids)
                {
                    var thuocTinh = await _context.ThuocTinh.FindAsync(x);
                    var giaTri = await _context.GiaTri.FindAsync(x);

                    if (thuocTinh != null)
                    {
                        _context.ThuocTinh.Remove(thuocTinh);
                    }

                    if (giaTri != null)
                    {
                        _context.GiaTri.Remove(giaTri);
                    }
                }

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<ThuocTinhModel>> GetAsync()
        {
            var result = from tt in await _context.ThuocTinh.ToListAsync()
                         join gt in await _context.GiaTri.ToListAsync() on tt.Id equals gt.IdThuocTinh
                         group gt by gt.IdThuocTinh into ttc
                         select new ThuocTinhModel
                         {
                             Id = ttc.First().ThuocTinhs.Id,
                             Ten = ttc.First().ThuocTinhs.Ten,
                             NgayTao = ttc.First().ThuocTinhs.NgayTao,
                             TrangThai = ttc.First().ThuocTinhs.TrangThai,
                             GiaTris = _mapper.Map<List<GiaTri>, List<GiaTriModel>>(ttc.ToList())
                         };
            return result;
        }

        public async Task<bool> UpdateAsync(Guid id, ThuocTinhUpdateModel obj)
        {
            try
            {
                // Tìm Thuộc Tính Theo Id
                var thuocTinh = _context.ThuocTinh.AsNoTracking().FirstOrDefault(c => c.Id == id);

                // Tìm Giá Trị Theo Id
                var giaTri = _context.GiaTri.AsNoTracking().FirstOrDefault(c => c.Id == id);

                if(thuocTinh != null)
                {
                    thuocTinh.Ten = obj.Ten;
                    thuocTinh.TrangThai = obj.TrangThai;
                    _context.ThuocTinh.Update(thuocTinh);
                }
                else
                {
                    if (giaTri != null)
                    {
                        giaTri.Ten = obj.Ten;
                        _context.GiaTri.Update(giaTri);
                    }
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
