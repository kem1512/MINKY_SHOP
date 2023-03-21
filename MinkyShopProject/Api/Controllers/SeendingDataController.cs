using Microsoft.AspNetCore.Mvc;
using MinkyShopProject.Business.Repositories.SeedingData;

namespace MinkyShopProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeendingDataController : ControllerBase
    {
        private readonly SeendingData _seendingData;

        public SeendingDataController(SeendingData seendingData)
        {
            this._seendingData = seendingData;
        }
        [HttpGet]
        [Route("SeeddingData")]
        public async Task<string> SeeddingData()
        {
            //await _seendingData.SeeddingKhachHang();
            //await _seendingData.SeeddingNhanVien();
            await _seendingData.SeeddingHoaDon();

            return "Thành Công";
        }
    }
}
