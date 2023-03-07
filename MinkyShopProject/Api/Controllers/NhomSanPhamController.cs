using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Repositories.NhomSanPham;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Data.Enums;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhomSanPhamController : ControllerBase
    {
        private readonly INhomSanPhamRepository _repository;

        public NhomSanPhamController(INhomSanPhamRepository repository)
        {
            _repository = repository;
        }


        [HttpGet()]
        public async Task<ActionResult<List<NhomSanPham>>> GetAsync()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpPost()]
        public async Task<ActionResult<bool>> AddAsync(NhomSanPhamModel obj)
        {
            return Ok(await _repository.AddAsync(obj));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateAsync(Guid id, NhomSanPhamModel obj)
        {
            return Ok(await _repository.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(Guid id)
        {
            return Ok(await _repository.DeleteAsync(id));
        }
    }
}
