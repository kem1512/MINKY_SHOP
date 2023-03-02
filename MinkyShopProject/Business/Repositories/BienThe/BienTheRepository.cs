using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.BienThe
{
    public class BienTheRepository : IBienTheRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public BienTheRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(Guid idSanPham, string tenSanPham, SanPhamCreateModel[] obj)
        {
            try
            {
                var skus = new List<string[]>();
                var idBienThe = Guid.NewGuid();

                var idThuocTinhSanPham = Guid.NewGuid();

                var sanPham = await _context.SanPham.FirstOrDefaultAsync(c => c.Id == idSanPham);

                if (sanPham == null)
                {
                    _context.SanPham.Add(new Entities.SanPham() { Id = idSanPham, Ten = tenSanPham });
                }

                foreach (var x in obj)
                {
                    var thuocTinhSanPhamExist = await _context.ThuocTinhSanPham.FirstOrDefaultAsync(c => c.IdSanPham == idSanPham && c.IdThuocTinh == x.IdThuocTinh);

                    // Thêm Thuộc Tính Cho Sản Phẩm
                    if (thuocTinhSanPhamExist == null)
                    {
                        var thuocTinhSanPham = new ThuocTinhSanPhamCreateModel() { Id = idThuocTinhSanPham, IdSanPham = idSanPham, IdThuocTinh = x.IdThuocTinh };
                        await _context.ThuocTinhSanPham.AddAsync(_mapper.Map<ThuocTinhSanPhamCreateModel, ThuocTinhSanPham>(thuocTinhSanPham));
                    }
                    else
                    {
                        idThuocTinhSanPham = thuocTinhSanPhamExist.Id;
                    }

                    // Tạo Ra Các Biến Giá Trị Sku Cho Biến Thể
                    var sku = new List<string>();
                    foreach (var y in x.GiaTris)
                    {
                        // Nếu Tên Thuộc Tính Có 2 Kí Tự Trở Lên Thì Lấy 2 Ký Tự Không Thì Chỉ Lấy Kí Tự Đầu Tiên
                        var thuocTinh = x.TenThuocTinh.Split(" ");

                        var tenThuocTinh = thuocTinh.Count() > 2 ? thuocTinh[0].Substring(0, 1) + thuocTinh[1].Substring(0, 1) : thuocTinh[0].Substring(0, 1);

                        // Lấy Kí Tự Đầu Tiên
                        var tenGiaTri = y.Ten.Substring(0, 1);

                        sku.Add(tenThuocTinh + tenGiaTri + "/" + y.Id + "/" + x.IdThuocTinh + "/" + idThuocTinhSanPham);
                    }
                    skus.Add(sku.ToArray());
                    idThuocTinhSanPham = Guid.NewGuid();
                }

                // Tổ Hợp Các Trường Hợp Từ Các Giá Trị
                foreach (var x in skus.CartesianProduct())
                {
                    // Mỗi Giá Trị X Sẽ Là Một Biến Thể
                    var bienThe = new BienTheCreateModel() { Id = idBienThe, IdSanPham = idSanPham, Sku = String.Join("", x.Select(c => c.Split("/")[0])) };
                    await _context.BienThe.AddAsync(_mapper.Map<BienTheCreateModel, Entities.BienThe>(bienThe));

                    foreach (var y in x)
                    {
                        var sku = y.Split("/"); // sku[0] : sku_id, sku[1] : Id Giá Trị, sku[2] : Id Thuộc Tính, sku[3] : Id Thuộc Tính Sản Phẩm

                        // Tạo Ra Biến Thể Chi Tiết Của Từng Biến Thể
                        var bienTheChiTiet = new BienTheChiTietCreateModel() { IdGiaTri = Guid.Parse(sku[1]), IdThuocTinhSanPham = Guid.Parse(sku[3]), IdBienThe = idBienThe };
                        await _context.BienTheChiTiet.AddAsync(_mapper.Map<BienTheChiTietCreateModel, BienTheChiTiet>(bienTheChiTiet));
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

        public async Task<List<BienTheModel>> GetAsync()
        {
            var bienThe = from tt in _context.ThuocTinh
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
                              Ten = btc.First().sp.Ten,
                              Sku = btc.First().bt.Sku,
                              GiaBan = btc.First().bt.GiaBan,
                              SoLuong = btc.First().bt.SoLuong,
                              Anh = btc.First().bt.Anh,
                              GiaTri = String.Join(" ", btc.Select(c => c.gt.Ten))
                          };

            return await Task.FromResult(bienThe.ToList());
        }

        public async Task<bool> UpdateAsync(Guid id, BienTheModel obj)
        {
            try
            {
                var bienThe = await _context.BienThe.FindAsync(id);
                if (bienThe != null)
                {
                    bienThe.Anh = obj.Anh;
                    bienThe.GiaBan = obj.GiaBan;
                    bienThe.SoLuong = obj.SoLuong;
                    _context.BienThe.Update(bienThe);
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
