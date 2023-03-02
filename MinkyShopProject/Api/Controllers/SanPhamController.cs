using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ISanPhamRepository _repository;

        public SanPhamController(ISanPhamRepository repository)
        {
            _repository = repository;
        }

        [HttpPost()]
        public async Task<ActionResult<object>> AddAsync(SanPhamModel obj)
        {
            return Ok(await _repository.AddAsync(obj));
        }

        [HttpGet()]
        public async Task<ActionResult<SanPhamModel[]>> GetAsync()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateAsync(Guid id, string name, TrangThaiSanPham TrangThai)
        {
            return Ok(await _repository.UpdateAsync(id, name, TrangThai));
        }

        [HttpDelete()]
        public async Task<ActionResult<SanPhamModel[]>> DeleteSanPhamAsync(Guid id)
        {
            return Ok(await _repository.DeleteAsync(id));
        }
    }
}
