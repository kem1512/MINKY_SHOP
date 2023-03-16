using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Repositories.HoaDon;
using MinkyShopProject.Business.Repositories.ThuocTinh;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IHoaDonRepository _repository;

        public HoaDonController(IHoaDonRepository repository)
        {
            _repository = repository;
        }

        [HttpPost()]
        public async Task<IActionResult> AddAsync([FromBody] HoaDonModel obj)
        {
            return Helper.TransformData(await _repository.AddAsync(obj));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] HoaDonModel obj)
        {
            return Helper.TransformData(await _repository.UpdateAsync(id, obj));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] HoaDonQueryModel obj)
        {
            obj.Url = $"{Request.Scheme}://{Request.Host.Value}/";
            return Helper.TransformData(await _repository.GetAsync(obj));
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Helper.TransformData(await _repository.DeleteAsync(id));
        }
    }
}
