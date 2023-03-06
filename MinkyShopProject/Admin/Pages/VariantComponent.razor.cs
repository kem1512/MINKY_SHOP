using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using MinkyShopProject.Data.Models;
using System.Net.Http.Json;

namespace MinkyShopProject.Admin.Pages
{
    public partial class VariantComponent
    {
        [Inject]
        HttpClient HttpClient { get; set; } = null!;

        [Inject]
        SweetAlertService Swal { get; set; } = null!;

        List<bool> showThuocTinh = new List<bool>();

        string GiaTri = "";

        string LoaiHangHoa = "";

        bool showTheLoai = false;

        string Url = "https://localhost:7053/api";

        List<bool> showGiaTri = new List<bool>();

        List<ThuocTinhModel> ModelThuocTinh = new List<ThuocTinhModel>();

        List<ThuocTinhModel> ModelTemplate = new List<ThuocTinhModel>();

        List<SanPhamModel> ModelSanPham = new List<SanPhamModel>();

        SanPhamModel SanPham = new SanPhamModel();

        protected override async Task OnInitializedAsync()
        {
            ModelThuocTinh = await HttpClient.GetFromJsonAsync<List<ThuocTinhModel>>($"{Url}/ThuocTinh") ?? new List<ThuocTinhModel>();
            ModelSanPham = await HttpClient.GetFromJsonAsync<List<SanPhamModel>>($"{Url}/SanPham") ?? new List<SanPhamModel>();
        }

        public async Task AddAsync()
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Thêm", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Warning });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            if (ModelTemplate.Count() > 0)
            {

                var result = new List<ThuocTinhModel>();

                foreach (var x in ModelTemplate)
                {
                    if (x.GiaTriTemplates.Count() > 0)
                    {
                        result.Add(new ThuocTinhModel() { Ten = x.Ten, Id = x.Id, GiaTris = x.GiaTriTemplates });
                    }
                }

                var result2 = await HttpClient.PostAsJsonAsync<BienTheCreateModel>($"{Url}/BienThe", new BienTheCreateModel() { ThuocTinhs = result, SanPham = SanPham });

                if (result2.IsSuccessStatusCode)
                {
                    ModelTemplate = new List<ThuocTinhModel>();
                }
            }
        }
    }
}
