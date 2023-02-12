using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Business.Repositories.ThuocTinh;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.ViewModels;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThuocTinhController : ControllerBase
    {
        private readonly IThuocTinhRepository _repository;

        public ThuocTinhController(MinkyShopDbContext context, IThuocTinhRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        [HttpPost("AddAsync")]
        public async Task<IActionResult> AddAsync(ThuocTinhViewCreateModel obj)
        {
            return Ok(await _repository.AddAsync(obj));
        }

        [HttpPost("AddRangeAsync")]
        public async Task<IActionResult> AddRangeAsync(IEnumerable<ThuocTinhViewCreateModel> objs)
        {
            return Ok(await _repository.AddRangeAsync(objs));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(Guid id, ThuocTinhViewUpdateModel obj)
        {
            return Ok(await _repository.UpdateAsync(id, obj));
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpDelete("DeleteAsync")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(await _repository.DeleteAsync(id));
        }

        [HttpDelete("DeleteRangeAsync")]
        public async Task<IActionResult> DeleteRangeAsync(Guid[] ids)
        {
            return Ok(await _repository.DeleteRangeAsync(ids));
        }
    }
}
