using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Pagination;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.Pagination;
using System.Net.Http.Json;
using PaginationRequest = MinkyShopProject.Business.Pagination.PaginationRequest;

namespace MinkyShopProject.Admin.Pages.Admin
{
    public partial class EmployeComponent
    {
        [Inject]
        private HttpClient HttpClient { get; set; } = null!;

        [Inject]
        private IJSRuntime JSRuntime { get; set; } = null!;

        [Inject]
        private SweetAlertService Swal { get; set; } = null!;

        [Parameter]
        public int Page { get; set; }

        private PaginationRequest PaginationRequest = new PaginationRequest() { PerPage = 5};

        private Response<PaginationResponse<NhanVien>> Response = new Response<PaginationResponse<NhanVien>>();

        private string url = "https://localhost:7053/api/NhanViens";

        private string query = "";

        private bool viewform = false;

        private bool Create = true;

        private NhanVienModel.NhanVienCreateModel Model = new NhanVienModel.NhanVienCreateModel();

        protected override async Task OnParametersSetAsync()
        {
            PaginationRequest.CurrentPage = Page;
            await Get();
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("choiceLoad", "{'searchEnabled': false}", ".js-choice");
                await JSRuntime.InvokeVoidAsync("choiceLoad", "{'searchEnabled': false}", ".js-choice2");
                await JSRuntime.InvokeVoidAsync("choiceLoad", "{'searchEnabled': false}", ".js-choice-size");
            }
        }

        async Task Get()
        {
            query = $"?PerPage={PaginationRequest.PerPage}&CurrentPage={PaginationRequest.CurrentPage}&Status={PaginationRequest.Status}&Role={PaginationRequest.Role}&Keyword={PaginationRequest.Keyword}";

            Response = await HttpClient.GetFromJsonAsync<Response<PaginationResponse<NhanVien>>>($"{url}{query}");
        }

        async Task FilterWithStatus(ChangeEventArgs e)
        {
            int? status = e.Value.ToString() == "" ? null : Convert.ToInt32(e.Value.ToString());
            PaginationRequest.Status = status;
            await Get();
        }

        async Task FilterWithRole(ChangeEventArgs e)
        {
            int? role = e.Value.ToString() == "" ? null : Convert.ToInt32(e.Value.ToString());
            PaginationRequest.Role = role;
            await Get();
        }

        async Task ChangePerPage(ChangeEventArgs e)
        {
            int perPage = Convert.ToInt32(e.Value.ToString());
            PaginationRequest.PerPage = perPage;
            await Get();
        }

        async Task Search(ChangeEventArgs e)
        {
            string? keyword = e.Value.ToString() == "" ? null : e.Value.ToString();
            PaginationRequest.Keyword = keyword;
            await Get();
        }

        async Task Remove(Guid Id)
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Xóa", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Question });

            if (string.IsNullOrEmpty(confirm.Value)) return;

            string idNhanVien = $"/{Id}";

            var result = await HttpClient.DeleteAsync($"{url}{idNhanVien}");
            var response = await result.Content.ReadFromJsonAsync<Response>();

            if (result.IsSuccessStatusCode)
            {
                await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = response.Message,
                    ShowConfirmButton = true,
                    Icon = SweetAlertIcon.Success
                });
                await Get();
            }
            else
            {
                await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = response.Message,
                    ShowConfirmButton = true,
                    Icon = SweetAlertIcon.Error
                });
            }
        }

        async Task ViewForm()
        {
            viewform = true;
        }

        async Task CancelForm()
        {
            viewform = false;
        }

        async Task ResetModel()
        {
            Model = new NhanVienModel.NhanVienCreateModel();
        }

        async Task Submit(EditContext editContext)
        {
            if (editContext.Validate())
            {
                if (Create)
                {
                    var result = await HttpClient.PostAsJsonAsync(url, Model);
                    var response = await result.Content.ReadFromJsonAsync<Response>();

                    if (result.IsSuccessStatusCode)
                    {
                        await Swal.FireAsync(new SweetAlertOptions
                        {
                            Title = response.Message,
                            ShowConfirmButton = true,
                            Icon = SweetAlertIcon.Success
                        });
                        await Get();
                        await ResetModel();
                    }
                    else
                    {
                        await Swal.FireAsync(new SweetAlertOptions
                        {
                            Title = response.Message,
                            ShowConfirmButton = true,
                            Icon = SweetAlertIcon.Error
                        });
                        await Get();
                    }
                }
            }
        }
    }
}
