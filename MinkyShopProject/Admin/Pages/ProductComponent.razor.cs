using Microsoft.AspNetCore.Components;
using MinkyShopProject.Data.Models;
using System.Net.Http.Json;

namespace MinkyShopProject.Admin.Pages
{
    public partial class ProductComponent
    {
        [Inject] HttpClient HttpClient { get; set; } = null!;

        private IEnumerable<SanPhamModel> Model = null!;

        private string Url = "https://localhost:7053/api/SanPham";

        protected override async Task OnInitializedAsync()
        {
            Model = await HttpClient.GetFromJsonAsync<IEnumerable<SanPhamModel>>(Url) ?? Enumerable.Empty<SanPhamModel>();
        }
    }
}
