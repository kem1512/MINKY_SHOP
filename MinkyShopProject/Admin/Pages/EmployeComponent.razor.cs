using Microsoft.AspNetCore.Components;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Pagination;
using System.Net.Http.Json;

namespace MinkyShopProject.Admin.Pages
{
    public partial class EmployeComponent
    {
        [Inject]
        public HttpClient HttpClient { get; set; } = null!;

        private PaginationResponse Response = new PaginationResponse();

        private string url = "https://localhost:7053/api";

        private int PerPage = 5;

        private int CurrentPage = 1;

        private int Status = 0;

        private string? Keyword = null;



        protected override async Task OnInitializedAsync()
        {
            await Get();
        }

        private async Task Get()
        {
            string uri = Keyword == null ? $"/NhanViens/{PerPage}/{CurrentPage}/{Status}" : $"/NhanViens/{PerPage}/{CurrentPage}/{Status}/{Keyword}";
            Response = await HttpClient.GetFromJsonAsync<PaginationResponse>($"{url}{uri}");
        }

        private async Task FilterStatus(ChangeEventArgs e)
        {
            Status = Convert.ToInt32(e.Value.ToString());
            await Get();
        }

        private async Task Search(ChangeEventArgs e)
        {
            Keyword = e.Value.ToString() == "" ? null : e.Value.ToString();
            await Get();
        }
    }
}
