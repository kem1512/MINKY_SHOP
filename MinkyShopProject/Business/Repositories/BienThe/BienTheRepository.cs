using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Commons;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.BienThe
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

    public class BienTheRepository : IBienTheRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public BienTheRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(BienTheCreateModel obj)
        {
            try
            {
                var skus = new List<string[]>();

                var idBienThe = Guid.NewGuid();

                var idSanPham = Guid.NewGuid();

                var idThuocTinhSanPham = Guid.NewGuid();

                await _context.SanPham.AddAsync(new Entities.SanPham() { Id = idSanPham, Ma = "SP" + Common.RandomString(5), IdNhomSanPham = obj.SanPham.IdNhomSanPham, Ten = obj.SanPham.Ten, NgayTao = obj.SanPham.NgayTao, TrangThai = obj.SanPham.TrangThai });

                foreach (var x in obj.ThuocTinhs)
                {

                    if (x.Id == Guid.Empty)
                    {
                        x.Id = Guid.NewGuid();
                        await _context.ThuocTinh.AddAsync(new Entities.ThuocTinh { Id = x.Id, Ten = x.Ten });
                    }

                    var thuocTinhSanPhamExist = await _context.ThuocTinhSanPham.FirstOrDefaultAsync(c => c.IdSanPham == idSanPham && c.IdThuocTinh == x.Id);

                    // Thêm Thuộc Tính Cho Sản Phẩm
                    if (thuocTinhSanPhamExist == null)
                    {
                        var thuocTinhSanPham = new ThuocTinhSanPham() { Id = idThuocTinhSanPham, IdSanPham = idSanPham, IdThuocTinh = x.Id };
                        await _context.ThuocTinhSanPham.AddAsync(thuocTinhSanPham);
                    }
                    else
                    {
                        idThuocTinhSanPham = thuocTinhSanPhamExist.Id;
                    }

                    // Tạo Ra Các Biến Giá Trị Sku Cho Biến Thể
                    var sku = new List<string>();

                    foreach (var y in x.GiaTris)
                    {
                        if (y.Id == Guid.Empty)
                        {
                            y.Id = Guid.NewGuid();
                            await _context.GiaTri.AddAsync(new Entities.GiaTri { Id = y.Id, Ten = y.Ten, IdThuocTinh = x.Id });
                        }

                        // Nếu Tên Thuộc Tính Có 2 Kí Tự Trở Lên Thì Lấy 2 Ký Tự Không Thì Chỉ Lấy Kí Tự Đầu Tiên
                        var thuocTinh = x.Ten.Split(" ");

                        var tenThuocTinh = thuocTinh.Count() > 2 ? thuocTinh[0].Substring(0, 1) + thuocTinh[1].Substring(0, 1) : thuocTinh[0].Substring(0, 1);

                        // Lấy Kí Tự Đầu Tiên
                        var tenGiaTri = y.Ten.Substring(0, 1);

                        sku.Add(tenThuocTinh + tenGiaTri + "/" + y.Id + "/" + x.Id + "/" + idThuocTinhSanPham);
                    }
                    skus.Add(sku.ToArray());
                    idThuocTinhSanPham = Guid.NewGuid();
                }

                // Tổ Hợp Các Trường Hợp Từ Các Giá Trị
                foreach (var x in skus.CartesianProduct())
                {
                    // Mỗi Giá Trị X Sẽ Là Một Biến Thể
                    var bienThe = new Entities.BienThe() { Id = idBienThe, IdSanPham = idSanPham, Ten = obj.SanPham.Ten + " " + String.Join(" + ", obj.ThuocTinhs.SelectMany(c => c.GiaTris.Select(c => c.Ten))), Sku = String.Join("", x.Select(c => c.Split("/")[0])) };
                    await _context.BienThe.AddAsync(bienThe);

                    foreach (var y in x)
                    {
                        var sku = y.Split("/"); // sku[0] : sku_id, sku[1] : Id Giá Trị, sku[2] : Id Thuộc Tính, sku[3] : Id Thuộc Tính Sản Phẩm

                        // Tạo Ra Biến Thể Chi Tiết Của Từng Biến Thể
                        var bienTheChiTiet = new Entities.BienTheChiTiet() { IdGiaTri = Guid.Parse(sku[1]), IdThuocTinhSanPham = Guid.Parse(sku[3]), IdBienThe = idBienThe };
                        await _context.BienTheChiTiet.AddAsync(bienTheChiTiet);
                    }

                    idBienThe = Guid.NewGuid();
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
                var bienThe = await _context.BienThe.FindAsync(id);
                if (bienThe != null)
                {
                    _context.BienThe.Remove(bienThe);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<BienTheChiTietModel> FindAsync(Guid id)
        {
            var bienTheModels = from tt in _context.ThuocTinh
                                join gt in _context.GiaTri on tt.Id equals gt.IdThuocTinh
                                join ttsp in _context.ThuocTinhSanPham on tt.Id equals ttsp.IdThuocTinh
                                join sp in _context.SanPham on ttsp.IdSanPham equals sp.Id
                                join btct in _context.BienTheChiTiet on new { gt = gt.Id, ttsp = ttsp.Id } equals new { gt = btct.IdGiaTri, ttsp = btct.IdThuocTinhSanPham }
                                join bt in _context.BienThe on btct.IdBienThe equals bt.Id
                                where sp.Id == id
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
                                select new BienTheModel
                                {
                                    Id = btc.First().bt.Id,
                                    Ten = btc.First().bt.Ten,
                                    Sku = btc.First().bt.Sku,
                                    GiaBan = btc.First().bt.GiaBan,
                                    SoLuong = btc.First().bt.SoLuong,
                                    Anh = btc.First().bt.Anh,
                                    GiaTri = String.Join(" + ", btc.Select(c => c.gt.Ten))
                                };

            var thuocTinhModels = from tt in _context.ThuocTinh
                                  join gt in _context.GiaTri on tt.Id equals gt.IdThuocTinh
                                  join ttsp in _context.ThuocTinhSanPham on tt.Id equals ttsp.IdThuocTinh
                                  join sp in _context.SanPham on ttsp.IdSanPham equals sp.Id
                                  join btct in _context.BienTheChiTiet on new { gt = gt.Id, ttsp = ttsp.Id } equals new { gt = btct.IdGiaTri, ttsp = btct.IdThuocTinhSanPham }
                                  join bt in _context.BienThe on btct.IdBienThe equals bt.Id
                                  where sp.Id == id
                                  group new { tt, gt } by new
                                  {
                                      tt.Id,
                                      tt.NgayTao,
                                      tt.TrangThai,
                                      tt.Ten,
                                      gt.IdThuocTinh,
                                  } into ttc
                                  select new ThuocTinhModel
                                  {
                                      Id = ttc.First().tt.Id,
                                      Ten = ttc.First().tt.Ten,
                                      GiaTris = _mapper.Map<List<GiaTri>, List<GiaTriModel>>(ttc.Select(c => c.gt).Distinct().ToList())
                                  };

            var bienTheChiTietModel = new BienTheChiTietModel() { BienTheModels = bienTheModels.ToList(), ThuocTinhModels = thuocTinhModels.ToList() };

            return await Task.FromResult(bienTheChiTietModel);
        }

        public async Task<List<BienTheModel>> GetAsync()
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
                         select new BienTheModel
                         {
                             Id = btc.First().bt.Id,
                             Ten = btc.First().bt.Ten,
                             Sku = btc.First().bt.Sku,
                             GiaBan = btc.First().bt.GiaBan,
                             SoLuong = btc.First().bt.SoLuong,
                             Anh = btc.First().bt.Anh,
                             IdSanPham = btc.First().sp.Id,
                             GiaTri = String.Join(" + ", btc.Select(c => c.gt.Ten))
                         };

            return await result.ToListAsync();
        }

        public async Task<bool> UpdateAsync(Guid id, BienTheModel obj)
        {
            try
            {
                var bienThe = await _context.BienThe.AsNoTracking().FirstOrDefaultAsync(c => c.Id == obj.Id);
                if (bienThe != null)
                {
                    _context.BienThe.Update(_mapper.Map<BienTheModel, Entities.BienThe>(obj));
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
