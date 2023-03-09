using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
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

        List<ThuocTinhModel> ThuocTinhModels = new List<ThuocTinhModel>();

        List<ThuocTinhModel> ThuocTinhModelsTemplate = new List<ThuocTinhModel>();

        List<NhomSanPhamModel> NhomSanPhamModels = new List<NhomSanPhamModel>();

        SanPhamModel SanPham = new SanPhamModel();

        protected override async Task OnInitializedAsync()
        {
            ThuocTinhModels = await HttpClient.GetFromJsonAsync<List<ThuocTinhModel>>($"{Url}/ThuocTinh") ?? new List<ThuocTinhModel>();
            NhomSanPhamModels = await HttpClient.GetFromJsonAsync<List<NhomSanPhamModel>>($"{Url}/NhomSanPham") ?? new List<NhomSanPhamModel>();
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

                var result2 = await HttpClient.PostAsJsonAsync($"{Url}/BienThe", new BienTheCreateModel() { ThuocTinhs = thuocTinhs, SanPham = SanPham });

                if (result2.IsSuccessStatusCode)
                {
                    ThuocTinhModelsTemplate = new List<ThuocTinhModel>();
                }
            }
        }
    }
}
