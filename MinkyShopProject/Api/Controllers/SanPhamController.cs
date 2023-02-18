using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly MinkyShopDbContext _context;
        private readonly ISanPhamRepository _repository;
        private readonly IMapper _mapper;

        public SanPhamController(MinkyShopDbContext context, ISanPhamRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<object>> AddAsync(Guid idSanPham, string tenSanPham, SanPhamCreateModel[] obj)
        {
            return Ok(await _repository.AddAsync(idSanPham, tenSanPham, obj));
        }

        [HttpPost("AddSanPhamAsync")]
        public async Task<ActionResult<object>> AddSanPhamAsync(string name)
        {
            return Ok(await _repository.AddSanPhamAsync(name));
        }

        [HttpGet]
        public async Task<ActionResult<SanPhamModel[]>> GetAsync()
        {
            return Ok(await _repository.GetAsync());
        }

        [HttpGet("GetSanPhamAsync")]
        public async Task<ActionResult<SanPhamModel[]>> GetSanPhamAsync()
        {
            return Ok(await _repository.GetSanPhamAsync());
        }

        [HttpDelete]
        public async Task<ActionResult<SanPhamModel[]>> DeleteAsync(Guid id)
        {
            return Ok(await _repository.DeleteAsync(id));
        }
    }
}
