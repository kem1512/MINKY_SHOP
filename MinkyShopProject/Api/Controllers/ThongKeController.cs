using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Repositories.ThongKe;
using MinkyShopProject.Common;

namespace MinkyShopProject.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThongKeController
    {
        private readonly IThongKeRepository _IThongKeRepository;

        public ThongKeController(IThongKeRepository IThongKeRepository)
        {
            _IThongKeRepository = IThongKeRepository;
        }

        [HttpGet, Route("ThongKeSanPhamBanNhieuNhatTheoThangNam")]
        public async Task<IActionResult> ThongKeSanPhamBanNhieuNhatTheoThangNam()
        {
            var result = await _IThongKeRepository.ThongKeSanPhamBanNhieuNhatTheoThangNam();
            return Helper.TransformData(result);
        }
    }
}
