using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System.Net.Http.Json;

namespace MinkyShopProject.Admin.Pages.Admin
{
    public class ThuocTinh
    {
        public bool Show { get; set; } = false;

        public bool Disabled { get; set; } = false;
    }

    public partial class VariantComponent
    {
        [Inject]
        HttpClient HttpClient { get; set; } = null!;

        [Inject]
        SweetAlertService Swal { get; set; } = null!;

        List<ThuocTinh> ThuocTinhs = new List<ThuocTinh>();

        string GiaTri = "";

        string LoaiHangHoa = "";

        bool showNhomSanPham = false;

        string Url = "https://localhost:7053/api";

        List<bool> showGiaTri = new List<bool>();

        ResponsePagination<ThuocTinhModel>? ThuocTinhModels;

        List<ThuocTinhModel> ThuocTinhModelsTemplate = new List<ThuocTinhModel>();

        ResponsePagination<NhomSanPhamModel>? NhomSanPhamModels;

        SanPhamModel SanPham = new SanPhamModel();

        protected override async Task OnInitializedAsync()
        {
            ThuocTinhModels = await HttpClient.GetFromJsonAsync<ResponsePagination<ThuocTinhModel>>($"{Url}/ThuocTinh");
            NhomSanPhamModels = await HttpClient.GetFromJsonAsync<ResponsePagination<NhomSanPhamModel>>($"{Url}/NhomSanPham");
        }

        public async Task AddAsync()
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Thêm", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            if (ThuocTinhModelsTemplate != null && ThuocTinhModelsTemplate.Count() > 0)
            {

                var thuocTinhs = new List<ThuocTinhModel>();

                foreach (var x in ThuocTinhModelsTemplate)
                {
                    if (x.GiaTriTemplates.Count() > 0)
                    {
                        thuocTinhs.Add(new ThuocTinhModel() { Ten = x.Ten, Id = x.Id, GiaTris = x.GiaTriTemplates });
                    }
                }

                var result = await HttpClient.PostAsJsonAsync($"{Url}/BienThe", new BienTheCreateModel() { ThuocTinhs = thuocTinhs, SanPham = SanPham });

                if (result.IsSuccessStatusCode)
                {
                    ThuocTinhModelsTemplate = new List<ThuocTinhModel>();
                }
            }
        }
    }
}