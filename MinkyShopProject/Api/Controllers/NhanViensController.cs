using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Repositories.NhanVien;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.Pagination;
using System.Reflection.Metadata.Ecma335;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanViensController : ControllerBase
    {
        private readonly INhanVienRepository _Repository;

        public NhanViensController(INhanVienRepository Repository)
        {
            _Repository = Repository;
        }

        [HttpGet("{perPage}/{currentPage}/{status}/{keyword?}")]
        public async Task<ActionResult<PaginationResponse>> Get(int perPage, int currentPage, int status, string? keyword = null)
        {
            return Ok(await _Repository.Get(perPage, currentPage, status, keyword));
        }

        [HttpGet("/nhanvien/{id}")]
        public async Task<ActionResult<NhanVien>> GetById(Guid id)
        {
            return Ok(await _Repository.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, NhanVienModel.NhanVienCreateModel model)
        {
            var NhanVien = new NhanVien()
            {
                Id = id,
                Anh = model.Anh,
                DiaChi = model.DiaChi,
                Email = model.Email,
                GioiTinh = model.GioiTinh,
                Ma = model.Ma,
                MatKhau = model.MatKhau,
                NgaySinh = model.NgaySinh,
                Sdt = model.Sdt,
                Ten = model.Ten,
                TrangThai = model.TrangThai,
                VaiTro = model.VaiTro,
            };

            return Ok(await _Repository.Put(NhanVien));
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(NhanVienModel.NhanVienCreateModel model)
        {
            var NhanVien = new NhanVien()
            {
                Id = Guid.NewGuid(),
                Anh = model.Anh,
                DiaChi = model.DiaChi,
                Email = model.Email,
                GioiTinh = model.GioiTinh,
                Ma = model.Ma,
                MatKhau = model.MatKhau,
                NgaySinh = model.NgaySinh,
                Sdt = model.Sdt,
                Ten = model.Ten,
                TrangThai = model.TrangThai,
                VaiTro = model.VaiTro,
            };

            return Ok(await _Repository.Post(NhanVien));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            return Ok(await _Repository.Delete(id));
        }
    }
}
