using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Repositories.NhomSanPham;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Common;
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
        public async Task<ActionResult<Response>> GetAsync([FromQuery] NhomSanPhamQueryModel obj)
        {
            return Helper.TransformData(await _repository.GetAsync(obj));
        }

        [HttpPost()]
        public async Task<ActionResult> AddAsync([FromBody] NhomSanPhamModel obj)
        {
            return Helper.TransformData(await _repository.AddAsync(obj));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateAsync(Guid id, [FromBody] NhomSanPhamModel obj)
        {
            return Helper.TransformData(await _repository.UpdateAsync(id, obj));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(Guid id)
        {
            return Helper.TransformData(await _repository.DeleteAsync(id));
        }
    }
}
