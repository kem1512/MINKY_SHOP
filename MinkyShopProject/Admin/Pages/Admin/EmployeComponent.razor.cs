using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Business.Pagination;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System.Net.Http.Json;
using PaginationRequest = MinkyShopProject.Business.Pagination.PaginationRequest;
using System.Text;
using Firebase.Auth;
using Firebase.Storage;
using System.IdentityModel.Tokens.Jwt;
using Blazored.LocalStorage;

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

        [Inject]
        private ILocalStorageService local { get; set; } = null!;

        [Inject]
        public NavigationManager Navigation { get; set; } = null!;

        [Parameter]
        public int Page { get; set; }

        private PaginationRequest PaginationRequest = new PaginationRequest() { PerPage = 5 };
        private Response<PaginationResponse<NhanVien>> Response = new Response<PaginationResponse<NhanVien>>();
        private string url = "https://localhost:7053/api/NhanViens";
        private string query = "";
        private bool viewform = false;
        private bool Create = true;
        private NhanVienModel.NhanVienCreateModel Model = new NhanVienModel.NhanVienCreateModel();
        private Guid IdNhanVien = Guid.Empty;
        private int maxAllowFiles = int.MaxValue;
        private long maxSizeFile = long.MaxValue;
        private static string ApiKey = "AIzaSyC1CQ9feCUbui7LV6qId8nesbF9TNma05E";
        private static string Bucket = "imagesangularapp.appspot.com";
        private static string AuthEmail = "truong@gmail.com";
        private static string AuthPassword = "123456";
        private Guid IdNhanVienDangNhap = Guid.Empty;
        private List<NhanVien> lstNhanVien = new();

        protected override async Task OnParametersSetAsync()
        {
            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(await local.GetItemAsStringAsync("Token"));
            var Role = jwt.Claims.FirstOrDefault(c => c.Type.Equals("VaiTro"))?.Value;
            var Id = jwt.Claims.FirstOrDefault(c => c.Type.Equals("Id"))?.Value;
            IdNhanVienDangNhap = Guid.Parse(Id);
            if (Role == "1")
            {
                Navigation.NavigateTo("/admin");
                await Swal.FireAsync("Thông báo", "Bạn Không Có Quyền Truy Cập Vào Quản Lí Nhân Viên", SweetAlertIcon.Warning);
            }

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

            lstNhanVien = Response.Data.Data.Where(c => c.Id != IdNhanVienDangNhap).ToList();
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
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Xóa Nhân Viên Này", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Question });

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
            await ResetModel();
            if (!Create)
            {
                Create = true;
            }
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
                    var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Thêm Nhân Viên Này", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Question });

                    if (string.IsNullOrEmpty(confirm.Value)) return;

                    Model.TrangThai = 1;
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
                        viewform = false;
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
                else
                {
                    var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Sửa Nhân Viên Này", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Question });
                    if (string.IsNullOrEmpty(confirm.Value)) return;

                    var result = await HttpClient.PutAsJsonAsync($"{url}/{IdNhanVien}", Model);
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
                        viewform = false;
                        Create = true;
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
            }
        }

        async Task GetNhanVien(Guid Id)
        {
            var response = await HttpClient.GetFromJsonAsync<Response<NhanVien>>($"{url}/{Id}");
            var nhanvien = response.Data;
            Model = new NhanVienModel.NhanVienCreateModel()
            {
                Anh = nhanvien.Anh,
                DiaChi = nhanvien.DiaChi,
                Email = nhanvien.Email,
                GioiTinh = nhanvien.GioiTinh,
                Ma = nhanvien.Ma,
                MatKhau = nhanvien.MatKhau,
                NgaySinh = nhanvien.NgaySinh ?? DateTime.Now,
                Sdt = nhanvien.Sdt,
                Ten = nhanvien.Ten,
                TrangThai = nhanvien.TrangThai,
                VaiTro = nhanvien.VaiTro,
            };
            viewform = true;
            Create = false;
            IdNhanVien = nhanvien.Id;
        }

        async Task UploadImage(InputFileChangeEventArgs e)
        {
            var file = e.File;
            var stream = file.OpenReadStream(maxSizeFile);
            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellation = new CancellationTokenSource();

            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("images")
                .Child(file.Name)
                .PutAsync(stream, cancellation.Token);

            try
            {
                Model.Anh = await task;
            }
            catch (Exception ex)
            {
                await Swal.FireAsync(new SweetAlertOptions
                {
                    Title = ex.Message,
                    ShowConfirmButton = true,
                    Icon = SweetAlertIcon.Error
                });
            }
        }

        async Task ChangeStatus(Guid Id, int Status)
        {
            var confirm = await Swal.FireAsync(new SweetAlertOptions { Title = "Bạn Có Chắc Muốn Sửa Trạng Thái", ShowConfirmButton = true, ShowCancelButton = true, Icon = SweetAlertIcon.Question });
            if (string.IsNullOrEmpty(confirm.Value)) return;

            var result = await HttpClient.DeleteAsync($"{url}/ChangeStatus/{Id}/{Status}");
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
                await Get();
            }
        }
    }
}
