using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Repositories.KhachHang;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;

namespace MinkyShopProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangRepository _IkhachHangRepository;

        public KhachHangController(IKhachHangRepository IkhachHangRepository)
        {
            _IkhachHangRepository = IkhachHangRepository;
        }

        [HttpGet, Route("Get")]
        public async Task<IActionResult> LayDuLieuKhachHang([FromQuery] KhachHangQueryModel item)
        {
            var khachHangRepo = await _IkhachHangRepository.GetKhachHang(item);
            return Helper.TransformData(khachHangRepo);
        }

        [HttpGet, Route("{Id}/GetById")]
        public async Task<IActionResult> LayDuLieuKhachHangBangId(Guid Id)
        {
            var result = await _IkhachHangRepository.GetbyIdKhachHang(Id);
            return Helper.TransformData(result);
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> ThemKhachHang([FromQuery] KhachHangThemVaSuaModel model)
        {
            var result = await _IkhachHangRepository.CreateKhachHang(model);
            return Helper.TransformData(result);
        }

        [HttpPut, Route("Update")]
        public async Task<IActionResult> SuaKhachHang(Guid Id, [FromQuery] KhachHangThemVaSuaModel model)
        {
            var result = await _IkhachHangRepository.UpdateKhachHang(Id, model);
            return Helper.TransformData(result);
        }

        [HttpDelete, Route("{Id}/Delete")]
        public async Task<IActionResult> XoaKhachHang(Guid Id)
        {
            var result = await _IkhachHangRepository.DeleteKhachHang(Id);
            return Helper.TransformData(result);
        }
    }
}
