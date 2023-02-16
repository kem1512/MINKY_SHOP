using Microsoft.AspNetCore.Components;
//using MinkyShopProject.Data.ViewModels;
using System.Net.Http.Json;

namespace MinkyShopProject.Admin.Pages
{
    public partial class PropertyComponent
    {
        [Inject] HttpClient HttpClient { get; set; } = null!;

        //private IEnumerable<ThuocTinhViewModel> Model = null!;

        //protected async override Task OnInitializedAsync()
        //{
        //    Model = await HttpClient.GetFromJsonAsync<IEnumerable<ThuocTinhViewModel>>("https://localhost:7053/api/ThuocTinh");
        //}
    }
}
