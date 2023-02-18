using Microsoft.AspNetCore.Components;
using MinkyShopProject.Data.Models;
using System.Net.Http.Json;

namespace MinkyShopProject.Admin.Pages
{
    public partial class ProductDetailComponent
    {
        [Inject] HttpClient HttpClient { get; set; } = null!;

        private BienTheModel Model = null!;

        protected override async Task OnInitializedAsync()
        {
            Model = await HttpClient.GetFromJsonAsync<BienTheModel>("https://localhost:7053/api/SanPham");
        }

        private void ChangeProduct(Guid id)
        {
            Console.WriteLine(id);
        }
    }
}
