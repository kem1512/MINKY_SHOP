using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Repositories.GiaoCa;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaoCasController : ControllerBase
    {
        private readonly IGiaoCaRepositories _Repositories;

        public GiaoCasController(IGiaoCaRepositories Repositories)
        {
            _Repositories = Repositories;
        }

        [HttpPost]
        public async Task<IActionResult> KhaiBaoDauCa(GiaoCaModels.GiaoCaCreateModel model)
        {
            return Helper.TransformData(await _Repositories.KhaiBaoDauCa(model));
        }

        [HttpGet]
        public async Task<IActionResult> GetCaHienTai([FromQuery] Guid Id,DateTime Time)
        {
            return Helper.TransformData(await _Repositories.GetCaHienThai(Id, Time));
        }

        [HttpGet("HoaDonTrongCa")]
        public async Task<IActionResult> GetHoaDonCaHienTai([FromQuery] Guid Id, DateTime Time)
        {
            return Helper.TransformData(await _Repositories.GetHoaDonCa(Id, Time));
        }
    }
}
