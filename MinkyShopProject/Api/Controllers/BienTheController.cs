using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Repositories.BienThe;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BienTheController : ControllerBase
    {
        private readonly IBienTheRepository _repository;

        public BienTheController(MinkyShopDbContext context, IBienTheRepository repository, IMapper mapper)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult<object>> AddAsync(Guid idSanPham, ThuocTinhModel[] obj)
        {
            return Ok(await _repository.AddAsync(idSanPham, obj));
        }


        [HttpGet]
        public async Task<ActionResult<SanPhamModel[]>> GetAsync()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateBienTheAsync(Guid id, BienTheUpdateModel obj)
        {
            return Ok(await _repository.UpdateAsync(id, obj));
        }

        [HttpDelete]
        public async Task<ActionResult<SanPhamModel[]>> DeleteAsync(Guid id)
        {
            return Ok(await _repository.DeleteAsync(id));
        }
    }
}
