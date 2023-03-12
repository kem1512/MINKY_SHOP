using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System.Net.Http.Json;

namespace MinkyShopProject.Admin.Pages.Admin
{
    public partial class ProductDetailComponent
    {
        [Inject]
        HttpClient HttpClient { get; set; } = null!;

        [Inject]
        SweetAlertService Swal { get; set; } = null!;

        [Inject]
        IJSRuntime JSRuntime { get; set; } = null!;

        [Parameter]
        public Guid IdBienThe { get; set; }

        [Parameter]
        public Guid IdSanPham { get; set; }

        bool Anh = false;

        ResponsePagination<NhomSanPhamModel>? NhomSanPhamModels;

        BienTheModel BienThe = new BienTheModel();

        ResponseObject<SanPhamModel>? SanPham;

        string NhomSanPham = "";

        List<string>? ModelImage;

        bool showNhomSanPham = false;

        string Url = "https://localhost:7053/api";

        protected override async Task OnInitializedAsync()
        {
            SanPham = await HttpClient.GetFromJsonAsync<ResponseObject<SanPhamModel>>($"{Url}/SanPham/{IdSanPham}");
            NhomSanPhamModels = await HttpClient.GetFromJsonAsync<ResponsePagination<NhomSanPhamModel>>($"{Url}/NhomSanPham");
        }

        protected async override void OnAfterRender(bool firstRender)
        {
            ModelImage = await JSRuntime.InvokeAsync<List<string>>("storageImages");
        }

        async Task DeleteAsync()
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Xóa", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            var result = await HttpClient.DeleteAsync($"{Url}/BienThe?id={BienThe.Id}");

            if (result.IsSuccessStatusCode)
                SanPham?.Data.BienThes?.Remove(BienThe);
        }

        async Task UpdateSanPhamAsync()
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Sửa", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            var result = await HttpClient.PutAsJsonAsync<SanPhamModel?>($"{Url}/SanPham/{SanPham?.Data.Id}", SanPham?.Data);

            if (result.IsSuccessStatusCode)
                await Swal.FireAsync("Thông Báo", "Cập Nhật Thành Công", SweetAlertIcon.Success);
        }

        async Task UpdateAsync()
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Sửa", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            BienThe.IdSanPham = IdSanPham;

            var result = await HttpClient.PutAsJsonAsync<BienTheModel>($"{Url}/BienThe/{BienThe.Id}", BienThe);

            if (result.IsSuccessStatusCode)
                await Swal.FireAsync("Thông Báo", "Cập Nhật Thành Công", SweetAlertIcon.Success);
        }

        private List<IBrowserFile> loadedFiles = new();

        private void LoadFiles(InputFileChangeEventArgs e)
        {
            loadedFiles.Clear();

            foreach (var file in e.GetMultipleFiles())
            {
                loadedFiles.Add(e.File);
            }
        }
    }
}
