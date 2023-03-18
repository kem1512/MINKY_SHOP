using Microsoft.AspNetCore.Components;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using MinkyShopProject.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using MinkyShopProject.Common;
using MinkyShopProject.Admin.Pages.Client;
using CurrieTechnologies.Razor.SweetAlert2;

namespace MinkyShopProject.Admin.Pages.Sale
{
    public partial class SaleComponent
    {
        [Inject]
        ISessionStorageService Session { get; set; } = null!;

        [Inject]
        HttpClient HttpClient { get; set; } = null!;

        [Inject]
        IJSRuntime JSRuntime { get; set; } = null!;

        [Inject]
        SweetAlertService Swal { get; set; } = null!;

        ProductDetailComponent? _component;

        Guid Id = Guid.Empty;

        bool ThanhToan = false;

        ResponsePagination<SanPhamModel>? SanPhams;

        ResponsePagination<KhachHangModel>? KhachHangs;

        List<HoaDonModel>? HoaDons = new List<HoaDonModel>();

        public int index = 0;

        string Url = "https://localhost:7053/api";

        protected async override Task OnInitializedAsync()
        {
            HoaDons = await Session.GetItemAsync<List<HoaDonModel>>("cart") ?? new List<HoaDonModel>();
            SanPhams = await HttpClient.GetFromJsonAsync<ResponsePagination<SanPhamModel>>($"{Url}/SanPham");
            KhachHangs = await HttpClient.GetFromJsonAsync<ResponsePagination<KhachHangModel>>($"{Url}/KhachHang/Get");
        }

        async Task AddHoaDonThat()
        {
            var status = await HttpClient.PostAsJsonAsync($"{Url}/HoaDon", HoaDons?[index]);
            if (status.IsSuccessStatusCode)
            {
                await Swal.FireAsync("Thông báo", "Thêm Thành Công", SweetAlertIcon.Success);
                HoaDons?.RemoveAt(index);
                await Session.SetItemAsync("cart", HoaDons);
            }
            else
            {
                await Swal.FireAsync("Thông báo", "Thêm Thất Bại", SweetAlertIcon.Error);
            }
        }

        async Task AddKhachHang(int id)
        {
            if (id == 999)
            {
                HoaDons[index].KhachHang = new KhachHangModel() { Ten = "Khách Lẻ" };
                HoaDons[index].IdKhachHang = null;
                await Session.SetItemAsync("cart", HoaDons);
            }
            else
            {
                var khachHang = KhachHangs?.Data.Content[id];
                if (khachHang != null)
                {
                    var hoaDon = HoaDons?[index];
                    if (hoaDon != null)
                    {
                        hoaDon.KhachHang = khachHang;
                        hoaDon.IdKhachHang = khachHang.Id;
                        await Session.SetItemAsync("cart", HoaDons);
                    }
                }
            }
        }

        public async Task AddItem(int index, BienTheModel obj, int soLuong)
        {
            if (HoaDons?[index].HoaDonChiTiets != null)
            {
                var hoaDon = HoaDons?[index];
                if (hoaDon != null)
                {
                    foreach (var x in hoaDon.HoaDonChiTiets)
                    {
                        if (x.BienThe?.Id == obj.Id)
                        {
                            x.SoLuong += soLuong;
                            await Session.SetItemAsync("cart", HoaDons);
                            HoaDons = await Session.GetItemAsync<List<HoaDonModel>>("cart");
                            return;
                        }
                    }
                    hoaDon.HoaDonChiTiets.Add(new HoaDonChiTietModel() { BienThe = obj, SoLuong = soLuong, IdBienThe = obj.Id, DonGia = obj.GiaBan });
                    hoaDon.TongTien = hoaDon.HoaDonChiTiets.Sum(c => c.BienThe?.GiaBan * c.SoLuong) + hoaDon?.TienShip ?? 0;
                    await Session.SetItemAsync("cart", HoaDons);
                }
            }
        }

        public async Task RemoveItem(int index, int indexHdct)
        {
            if (HoaDons?[index].HoaDonChiTiets != null)
            {
                var hoaDon = HoaDons?[index];
                if (hoaDon != null)
                {
                    HoaDons?[index].HoaDonChiTiets.Remove(hoaDon.HoaDonChiTiets[indexHdct]);
                    hoaDon.TongTien = hoaDon.HoaDonChiTiets.Sum(c => c.BienThe?.GiaBan * c.SoLuong) + hoaDon?.TienShip ?? 0;
                }
                await Session.SetItemAsync("cart", HoaDons);
            }
        }

        public async Task Update(int index, int indexHdct, bool plus, int soLuong = 0)
        {
            if (HoaDons?[index].HoaDonChiTiets != null)
            {
                if (soLuong != 0)
                {
                    HoaDons[index].HoaDonChiTiets[indexHdct].SoLuong = soLuong;
                }
                else
                {
                    if (plus)
                    {
                        HoaDons[index].HoaDonChiTiets[indexHdct].SoLuong += 1;
                    }
                    else
                    {
                        HoaDons[index].HoaDonChiTiets[indexHdct].SoLuong -= 1;
                    }
                }
                HoaDons[index].TongTien = HoaDons?[index].HoaDonChiTiets.Sum(c => c.BienThe?.GiaBan * c.SoLuong) + HoaDons?[index]?.TienShip ?? 0;
                await Session.SetItemAsync("cart", HoaDons);
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("overflow");
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("choiceLoad", "{'searchEnabled': true, 'searchFields': ['label'] }", ".select-khachhang");
            }
        }

        public async Task AddOrder()
        {
            HoaDons?.Add(new HoaDonModel());
            await Session.SetItemAsync("cart", HoaDons);
        }



        public async Task Reload()
        {
            await Session.SetItemAsync("cart", HoaDons);
            await JSRuntime.InvokeVoidAsync("choiceLoad", "{'searchEnabled': true, 'searchFields': ['label'] }", ".select-khachhang");
        }

        private async Task RemoveOrder(int index)
        {
            HoaDons?.RemoveAt(index);
            await Session.SetItemAsync("cart", HoaDons);
        }
    }
}
