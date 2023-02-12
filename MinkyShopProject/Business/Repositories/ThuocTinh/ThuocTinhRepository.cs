using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.ViewModels;
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

        public async Task<bool> AddAsync(ThuocTinhViewCreateModel obj)
        {
            try
            {
                //  Tạo Id Mới Trước Khi Thêm
                Guid id = Guid.NewGuid();

                // Map 2 Object Lại Với Nhau
                var thuocTinh = _mapper.Map<ThuocTinhCreateModel, Entities.ThuocTinh>(obj.ThuocTinh);

                // Gán Lại Id Cho Thuộc Tính
                thuocTinh.Id = id;

                await _context.ThuocTinh.AddAsync(thuocTinh);

                foreach (var x in obj.GiaTris)
                {
                    var giaTri = _mapper.Map<GiaTriCreateModel, GiaTri>(x);
                    giaTri.IdThuocTinh = id;
                    await _context.GiaTri.AddAsync(giaTri);
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> AddRangeAsync(IEnumerable<ThuocTinhViewCreateModel> obj)
        {
            try
            {
                foreach (var x in obj)
                {
                    //  Tạo Id Mới Trước Khi Thêm
                    Guid id = Guid.NewGuid();

                    // Map 2 Object Lại Với Nhau
                    var thuocTinh = _mapper.Map<ThuocTinhCreateModel, Entities.ThuocTinh>(x.ThuocTinh);

                    // Gán Lại Id Cho Thuộc Tính
                    thuocTinh.Id = id;

                    await _context.ThuocTinh.AddAsync(thuocTinh);

                    foreach (var y in x.GiaTris)
                    {
                        var giaTri = _mapper.Map<GiaTriCreateModel, GiaTri>(y);
                        giaTri.IdThuocTinh = id;
                        await _context.GiaTri.AddAsync(giaTri);
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

        public async Task<IEnumerable<ThuocTinhViewModel>> GetAsync()
        {
            var result = from tt in await _context.ThuocTinh.ToListAsync()
                         join gt in await _context.GiaTri.ToListAsync() on tt.Id equals gt.IdThuocTinh
                         group gt by gt.IdThuocTinh into ttc
                         select new ThuocTinhViewModel
                         {
                             GiaTris = _mapper.Map<IEnumerable<GiaTri>, IEnumerable<GiaTriModel>>(ttc),
                             ThuocTinh = _mapper.Map<Entities.ThuocTinh, ThuocTinhModel>(ttc.FirstOrDefault()?.ThuocTinhs ?? new Entities.ThuocTinh())
                         };
            return result;
        }

        public async Task<bool> UpdateAsync(Guid id, ThuocTinhViewUpdateModel obj)
        {
            try
            {
                // Nếu Truyền Vào Thuộc Tính Thì Cập Nhật Thuộc Tính
                if (obj.ThuocTinh != null)
                {
                    // Tìm Thuộc Tính Theo Id
                    var result = _context.ThuocTinh.AsNoTracking().FirstOrDefault(c => c.Id == id);

                    // Gán Lại Giá Trị Cho Thuộc Tính
                    if (result != null)
                    {
                        result.Ten = obj.ThuocTinh.Ten;
                        result.TrangThai = obj.ThuocTinh.TrangThai;

                        _context.ThuocTinh.Update(result);
                    }

                    await _context.SaveChangesAsync();

                    return true;
                }
                else
                {
                    // Không Thì Sẽ Cập Nhật Lại Giá Trị

                    if (obj.GiaTri != null)
                    {
                        // Tìm Giá Trị Theo Id
                        var result = _context.GiaTri.AsNoTracking().FirstOrDefault(c => c.Id == id);

                        // Gán Lại Giá Trị Cho Giá Trị
                        if (result != null)
                        {
                            result.Ten = obj.GiaTri.Ten;

                            _context.GiaTri.Update(result);
                        }

                        await _context.SaveChangesAsync();

                        return true;
                    }

                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
