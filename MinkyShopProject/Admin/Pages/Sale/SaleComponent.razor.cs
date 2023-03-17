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

        Guid Id = Guid.Empty;

        ProductDetailComponent? _component;

        ResponsePagination<SanPhamModel>? SanPhams;

        List<HoaDonModel>? HoaDons = new List<HoaDonModel>();

        public int index = 0;

        string Url = "https://localhost:7053/api";

        protected async override Task OnInitializedAsync()
        {
            if (HoaDons == null && HoaDons?.Count == 0 && !HoaDons.Any()) { await AddOrder(); }
            HoaDons = await Session.GetItemAsync<List<HoaDonModel>>("cart");
            SanPhams = await HttpClient.GetFromJsonAsync<ResponsePagination<SanPhamModel>>($"{Url}/SanPham");
        }

        public async Task AddItem(int index, BienTheModel obj, int soLuong)
        {
            if (HoaDons?[index].HoaDonChiTiets != null)
            {
                foreach (var x in HoaDons[index].HoaDonChiTiets)
                {
                    if (x.BienThe?.Id == obj.Id)
                    {
                        x.SoLuong += soLuong;
                        await Session.SetItemAsync("cart", HoaDons);
                        HoaDons = await Session.GetItemAsync<List<HoaDonModel>>("cart");
                        return;
                    }
                }
                HoaDons?[index].HoaDonChiTiets.Add(new HoaDonChiTietModel() { BienThe = obj, SoLuong = soLuong });
                HoaDons[index].TongTien = HoaDons?[index].HoaDonChiTiets.Sum(c => c.BienThe?.GiaBan * c.SoLuong) ?? 0;
                await Session.SetItemAsync("cart", HoaDons);
                HoaDons = await Session.GetItemAsync<List<HoaDonModel>>("cart");
            }
        }

        public async Task RemoveItem(int index, int indexHdct)
        {
            if (HoaDons?[index].HoaDonChiTiets != null)
            {
                HoaDons?[index].HoaDonChiTiets.Remove(HoaDons?[index].HoaDonChiTiets[indexHdct]);
                await Session.SetItemAsync("cart", HoaDons);
                HoaDons = await Session.GetItemAsync<List<HoaDonModel>>("cart");
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("overflow");
        }

        public async Task AddOrder()
        {
            HoaDons?.Add(new HoaDonModel());
            await Session.SetItemAsync("cart", HoaDons);
            HoaDons = await Session.GetItemAsync<List<HoaDonModel>>("cart");
        }

        private async Task RemoveOrder(int index)
        {
            HoaDons?.RemoveAt(index);
            await Session.SetItemAsync("cart", HoaDons);
            HoaDons = await Session.GetItemAsync<List<HoaDonModel>>("cart");
        }
    }
}
