using Microsoft.AspNetCore.Components;
using Blazored.SessionStorage;

namespace MinkyShopProject.Admin.Pages.Sale
{
    public partial class SaleComponent
    {
        [Inject]
        ISessionStorageService SessionStorage { get; set; } = null!;

        [Inject]
        HttpClient HttpClient { get; set; } = null!;



        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }

        public async Task AddAsync()
        {

        }
    }
}
