using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Repositories.GioHang;
using MinkyShopProject.Business.Repositories.NhomSanPham;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GioHangController : ControllerBase
    {
        private readonly IGioHangRepository _repository;

        public GioHangController(IGioHangRepository repository)
        {
            _repository = repository;
        }


        [HttpGet()]
        public async Task<ActionResult<List<GioHangModel>>> GetAsync()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpPost()]
        public async Task<ActionResult<bool>> AddAsync(GioHangModel obj)
        {
            return Ok(await _repository.AddAsync(obj));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(Guid id)
        {
            return Ok(await _repository.DeleteAsync(id));
        }
    }
}
