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

        [HttpPost("KetCa/{Id}")]
        public async Task<IActionResult> KetCa(Guid Id,GiaoCaModels.GiaoCaEditModel model)
        {
            return Helper.TransformData(await _Repositories.KetCa(Id,model));
        }

        [HttpGet]
        public async Task<IActionResult> GetCaHienTai([FromQuery] Guid Id ,DateTime ThoiGian)
        {
            return Helper.TransformData(await _Repositories.GetCaHienThai(Id,ThoiGian));
        }

        [HttpGet("HoaDonTrongCa")]
        public async Task<IActionResult> GetHoaDonCaHienTai([FromQuery] Guid Id,DateTime ThoiGian)
        {
            return Helper.TransformData(await _Repositories.GetHoaDonCa(Id,ThoiGian));
        }

        [HttpGet("TienMatHoaDonTrongCa")]
        public async Task<IActionResult> GetHoaDonCaHienTai([FromQuery] Guid IdHoaDon)
        {
            return Helper.TransformData(await _Repositories.GetTienMatHoaDon(IdHoaDon));
        }
    }
}
