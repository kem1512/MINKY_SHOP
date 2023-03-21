using MinkyShopProject.Common;

namespace MinkyShopProject.Business.Repositories.ThongKe
{
    public interface IThongKeRepository
    {
        public Task<Response> ThongKeSanPhamBanNhieuNhatTheoThangNam();
    }
}
