using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Pagination;
using MinkyShopProject.Business.Repositories.NhanVien;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.Pagination;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using PaginationRequest = MinkyShopProject.Business.Pagination.PaginationRequest;

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

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] PaginationRequest paginationRequest)
        {
            return Helper.TransformData(await _Repository.Get(paginationRequest));
        }

        [HttpGet("/nhanvien/{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            return Helper.TransformData(await _Repository.GetById(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id,NhanVienModel.NhanVienCreateModel model)
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

            return Helper.TransformData(await _Repository.Put(NhanVien));
        }

        [HttpPost]
        public async Task<ActionResult> Post(NhanVienModel.NhanVienCreateModel model)
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
                NgayTao = DateTime.Now
            };

            return Helper.TransformData(await _Repository.Post(NhanVien));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            return Helper.TransformData(await _Repository.Delete(id));
        }
    }
}
