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

        bool showAll = false;

        ResponsePagination<NhomSanPhamModel>? NhomSanPhamModels;

        List<NhomSanPhamModel>? NhomSanPhamModelsParent;

        ResponseObject<SanPhamModel>? SanPham;

        List<string>? ModelImage;

        bool showNhomSanPham = false;

        string Url = "https://localhost:7053/api";

        protected override async Task OnInitializedAsync()
        {
            SanPham = await HttpClient.GetFromJsonAsync<ResponseObject<SanPhamModel>>($"{Url}/SanPham/{IdSanPham}");
            NhomSanPhamModels = await HttpClient.GetFromJsonAsync<ResponsePagination<NhomSanPhamModel>>($"{Url}/NhomSanPham");
            if (NhomSanPhamModels != null && NhomSanPhamModels.Data.Content.Any())
            {
                NhomSanPhamModelsParent = NhomSanPhamModels.Data.Content;
            }
            ModelImage = await JSRuntime.InvokeAsync<List<string>>("storageImages");
        }

        async Task DeleteAsync(Guid id)
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Xóa", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            var result = await HttpClient.DeleteAsync($"{Url}/BienThe?id={id}");

            if (result.IsSuccessStatusCode)
                SanPham = await HttpClient.GetFromJsonAsync<ResponseObject<SanPhamModel>>($"{Url}/SanPham/{IdSanPham}");
        }

        async Task UpdateSanPhamAsync()
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Sửa", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            var result = await HttpClient.PutAsJsonAsync($"{Url}/SanPham/{SanPham?.Data.Id}", SanPham?.Data);

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
