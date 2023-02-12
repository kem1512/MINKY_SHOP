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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SanPhamModel>>> GetSanPhams()
        {
            return Ok(await _repository.GetSanPhams());
        }
    }
}
