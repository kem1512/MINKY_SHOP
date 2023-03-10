using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Common;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> FindAsync(Guid id)
        {
            return Helper.TransformData(await _repository.FindAsync(id));
        }

        [HttpGet()]
        public async Task<ActionResult> GetAsync([FromQuery] SanPhamQueryModel obj)
        {
            return Helper.TransformData(await _repository.GetAsync(obj));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response>> UpdateAsync(Guid id, [FromBody] SanPhamModel obj)
        {
            return Helper.TransformData(await _repository.UpdateAsync(id, obj));
        }

        [HttpDelete()]
        public async Task<ActionResult<Response>> DeleteSanPhamAsync(Guid id)
        {
            return Helper.TransformData(await _repository.DeleteAsync(id));
        }
    }
}
