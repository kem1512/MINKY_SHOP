using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Repositories.BienThe;
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
        public async Task<ActionResult<object>> AddAsync(BienTheCreateModel obj)
        {
            return Ok(await _repository.AddAsync(obj));
        }


        [HttpGet]
        public async Task<ActionResult<SanPhamModel[]>> GetAsync()
        {
            return Ok(await _repository.GetAsync());
        }

		[HttpGet("{id}")]
		public async Task<ActionResult<List<SanPhamModel>>> GetAsync(Guid id)
		{
			return Ok(await _repository.FindAsync(id));
		}


        [HttpDelete]
        public async Task<ActionResult<SanPhamModel[]>> DeleteAsync(Guid id)
        {
            return Ok(await _repository.DeleteAsync(id));
        }
    }
}
