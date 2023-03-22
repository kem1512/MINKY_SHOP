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

        [HttpGet, Route("ThongKeTongTienNgayTienThangNam")]
        public async Task<IActionResult> ThongKeTongTienNgayTienThangNam()
        {
            var result = await _IThongKeRepository.ThongKeTongTienNgayTienThangNam();
            return Helper.TransformData(result);
        }

        [HttpGet, Route("ThongKeNhanVienBanDuocNhieuHoaDonvaTienNhat")]
        public async Task<IActionResult> ThongKeNhanVienBanDuocNhieuHoaDonvaTienNhat()
        {
            var result = await _IThongKeRepository.ThongKeNhanVienBanDuocNhieuHoaDonvaTienNhat();
            return Helper.TransformData(result);
        }
    }
}
