using Microsoft.AspNetCore.Components;
using Blazored.SessionStorage;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Common;
using MinkyShopProject.Admin.Pages.Client;
using CurrieTechnologies.Razor.SweetAlert2;
using MinkyShopProject.Business.Entities;

namespace MinkyShopProject.Admin.Pages.Sale
{
    public partial class SaleComponent
    {
        [Inject]
        ISessionStorageService Session { get; set; } = null!;

        Guid Id = Guid.Empty;

        [Inject]
        HttpClient HttpClient { get; set; } = null!;

        [Inject]
        IJSRuntime JSRuntime { get; set; } = null!;

        [Inject]
        SweetAlertService Swal { get; set; } = null!;

        List<HoaDonCreateModel> HoaDons = new List<HoaDonCreateModel>();

        ResponsePagination<SanPhamModel>? SanPhams;

        List<SanPhamModel> SanPhamsSearch = new List<SanPhamModel>();

        Uri Url = new Uri("https://localhost:7053/api/");

        int index = 0;

        float soTienThanhToan = 0;

        List<HinhThucThanhToanModel>? HinhThucThanhToans;

        protected override async Task OnInitializedAsync()
        {
            SanPhams = await HttpClient.GetFromJsonAsync<ResponsePagination<SanPhamModel>>(Url + "sanpham");
            await Reload();
        }

        async Task CapNhatTongTien()
        {
            if (HoaDons != null && HoaDons.Any())
            {
                var hoaDon = HoaDons[index];
                hoaDon.TongTien = hoaDon.HoaDonChiTiets.Sum(c => c.DonGia * c.SoLuong) + hoaDon.TienShip;
            }
        }

        async Task TimKiemSanPham(string value)
        {
            if (SanPhams != null)
            {
                SanPhamsSearch = SanPhams.Data.Content.Where(c => c.Ma?.ToLower().Trim() == value.ToLower().Trim() || c.Ten.ToLower().Trim().Contains(value.ToLower().Trim())).ToList();
            }
        }

        async Task XoaSanPham(int hdct)
        {
            if (HoaDons != null && HoaDons.Any())
            {
                HoaDons[index].HoaDonChiTiets.RemoveAt(hdct);
            }
        }

        public async Task ThemSanPham(List<BienTheModel> obj)
        {
            if (HoaDons != null && HoaDons.Any())
            {
                foreach (var x in obj)
                {
                    if (x.SoLuongTam > x.SoLuong)
                    {
                        await Swal.FireAsync("", $"{x.SanPham?.Ten} Không Đủ Sản Phẩm Trong Kho", SweetAlertIcon.Error);
                        return;
                    }
                    else
                    {
                        var hdct = HoaDons[index].HoaDonChiTiets.FirstOrDefault(c => c.IdBienThe == x.Id);
                        if (hdct != null && hdct.BienThe != null)
                        {
                            hdct.SoLuong += x.SoLuongTam;
                        }
                        else
                        {
                            HoaDons[index].HoaDonChiTiets.Add(new HoaDonChiTietModel() { IdBienThe = x.Id, SoLuong = x.SoLuongTam, DonGia = x.GiaBan, BienThe = x });
                        }
                    }
                }
                HoaDons[index].TongTien = HoaDons[index].HoaDonChiTiets.Sum(c => c.DonGia * c.SoLuong);
                StateHasChanged();
            }
        }

        void XoaHinhThucThanhToan(int kieuThanhToan)
        {
            if (HinhThucThanhToans != null)
            {
                var hinhThucThanhToan = HinhThucThanhToans.FirstOrDefault(c => c.KieuThanhToan == kieuThanhToan);
                if (hinhThucThanhToan != null)
                {
                    HinhThucThanhToans.Remove(hinhThucThanhToan);
                    if (HinhThucThanhToans.Count <= 0)
                    {
                        HinhThucThanhToans.Add(new HinhThucThanhToanModel() { KieuThanhToan = 0, TongTienThanhToan = 0 });
                    }
                }
            }
        }

        public async Task ThemHinhThucThanhToan(int kieuThanhToan)
        {
            if (HinhThucThanhToans != null)
            {
                var hinhThucThanhToan = HinhThucThanhToans.FirstOrDefault(c => c.KieuThanhToan == kieuThanhToan);
                if (hinhThucThanhToan == null)
                {
                    HinhThucThanhToans.Add(new HinhThucThanhToanModel() { KieuThanhToan = kieuThanhToan, TongTienThanhToan = soTienThanhToan });
                }
                else
                {
                    hinhThucThanhToan.TongTienThanhToan += soTienThanhToan;
                }
                soTienThanhToan = 0;
            }
        }

        async Task ThemHoaDon()
        {
            if (HoaDons != null && HoaDons.Any() && HoaDons.Count < 10)
            {
                HoaDons.Add(new HoaDonCreateModel());
                await JSRuntime.InvokeVoidAsync("overflow");
            }
            else
            {
                await Swal.FireAsync("", "Không Thể Thêm Quá 10 Hóa Đơn", SweetAlertIcon.Error);
            }
        }

        async Task XoaHoaDon(int index)
        {
            if (HoaDons != null && HoaDons.Any())
            {
                HoaDons.RemoveAt(index);
                await JSRuntime.InvokeVoidAsync("overflow");
                if (HoaDons.Count <= 0)
                {
                    HoaDons = new List<HoaDonCreateModel>() { new HoaDonCreateModel() { } };
                }
            }
        }

        async Task Reload()
        {
            HoaDons = new List<HoaDonCreateModel>() { new HoaDonCreateModel() };
        }
    }
}
