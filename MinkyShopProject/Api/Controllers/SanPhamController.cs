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

        [HttpGet("{id}")]
        public async Task<ActionResult<SanPhamModel[]>> FindAsync(Guid id)
        {
            return Ok(await _repository.FindAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, SanPhamModel obj)
        {
            return Ok(await _repository.UpdateAsync(id, obj));
        }

        [HttpDelete()]
        public async Task<ActionResult<SanPhamModel[]>> DeleteSanPhamAsync(Guid id)
        {
            return Ok(await _repository.DeleteAsync(id));
        }
    }
}
