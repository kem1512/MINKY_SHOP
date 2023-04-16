using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;
using System.IO;

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

        public int IdBienThe = 999;

        [Parameter]
        public Guid IdSanPham { get; set; }

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
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Chắc Muốn Xóa", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            var result = await HttpClient.DeleteAsync($"{Url}/BienThe?id={id}");

            if (result.IsSuccessStatusCode)
                SanPham = await HttpClient.GetFromJsonAsync<ResponseObject<SanPhamModel>>($"{Url}/SanPham/{IdSanPham}");
        }

        async Task UpdateSanPhamAsync()
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Text = "Bạn Có Chắc Muốn Sửa", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            if (SanPham?.Data.BienThes != null)
            {
                foreach (var x in SanPham.Data.BienThes)
                {
                    x.BienTheChiTiets = null;
                }
            }

            var result = await HttpClient.PutAsJsonAsync($"{Url}/SanPham/{SanPham?.Data.Id}", SanPham?.Data);

            if (result.IsSuccessStatusCode)
                await Swal.FireAsync("Thông Báo", "Cập Nhật Thành Công", SweetAlertIcon.Success);
        }

        private List<IBrowserFile> loadedFiles = new();

        private long maxFileSize = 1024 * 15;
        private int maxAllowedFiles = 3;
        private bool isLoading;

        private void LoadFiles(InputFileChangeEventArgs e)
        {
            isLoading = true;
            loadedFiles.Clear();

            foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
            {
                loadedFiles.Add(file);
            }

            isLoading = false;
        }
    }
}
