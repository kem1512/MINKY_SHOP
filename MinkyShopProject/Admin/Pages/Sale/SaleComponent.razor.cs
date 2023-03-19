using Microsoft.AspNetCore.Components;
using Blazored.SessionStorage;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Common;
using MinkyShopProject.Admin.Pages.Client;
using CurrieTechnologies.Razor.SweetAlert2;

namespace MinkyShopProject.Admin.Pages.Sale
{
    public partial class SaleComponent
    {
        [Inject]
        ISessionStorageService Session { get; set; } = null!;

        [Inject]
        HttpClient HttpClient { get; set; } = null!;

        [Inject]
        IJSRuntime JSRuntime { get; set; } = null!;

        [Inject]
        SweetAlertService Swal { get; set; } = null!;

        ProductDetailComponent? _component;

        Guid Id = Guid.Empty;

        bool ThanhToan = false;

        ResponsePagination<SanPhamModel>? SanPhams;

        ResponsePagination<KhachHangModel>? KhachHangs;

        List<HoaDonModel>? HoaDons = new List<HoaDonModel>();

        List<HinhThucThanhToanModel>? Coc;

        public int index = 0;

        Uri Url = new Uri("https://localhost:7053/api/hoadon");

        protected async override Task OnInitializedAsync()
        {
            HoaDons = await Session.GetItemAsync<List<HoaDonModel>>("cart") ?? new List<HoaDonModel>();
            SanPhams = await HttpClient.GetFromJsonAsync<ResponsePagination<SanPhamModel>>("https://localhost:7053/api/SanPham");
            KhachHangs = await HttpClient.GetFromJsonAsync<ResponsePagination<KhachHangModel>>("https://localhost:7053/api/KhachHang/Get");
        }

        async Task AddHoaDonThat()
        {
            var hoaDon = HoaDons?[index];
            if (hoaDon != null)
            {
                hoaDon.NhanVien = null;

                hoaDon.KhachHang = null;

                if (hoaDon.LoaiDonHang == 0 && hoaDon.HinhThucThanhToans?.Sum(c => c.TongTienThanhToan) + hoaDon.TienShip < hoaDon.TongTien)
                {
                    hoaDon.TrangThai = Data.Enums.TrangThaiHoaDon.Debt;

                    if (hoaDon.HinhThucThanhToans != null)
                    {
                        foreach (var x in hoaDon.HinhThucThanhToans)
                        {
                            // Cọc
                            x.KieuThanhToan = 2;
                        }
                    }
                }
                else if (hoaDon.LoaiDonHang == 1)
                {
                    hoaDon.TrangThai = Data.Enums.TrangThaiHoaDon.Ship;
                }
                else
                {
                    hoaDon.TrangThai = Data.Enums.TrangThaiHoaDon.Done;
                }

                if (hoaDon.Id != Guid.Empty)
                {
                    var status = await HttpClient.PutAsJsonAsync(Url.AddQuery("id", hoaDon.Id.ToString()), hoaDon);
                    if (status.IsSuccessStatusCode)
                    {
                        await Swal.FireAsync("Thông báo", "Thêm Thành Công", SweetAlertIcon.Success);
                        HoaDons?.RemoveAt(index);
                        await Session.SetItemAsync("cart", HoaDons);
                    }
                    else
                    {
                        await Swal.FireAsync("Thông báo", "Thêm Thất Bại", SweetAlertIcon.Error);
                    }
                }
                else
                {
                    var status = await HttpClient.PostAsJsonAsync(Url, HoaDons?[index]);
                    if (status.IsSuccessStatusCode)
                    {
                        await Swal.FireAsync("Thông báo", "Thêm Thành Công", SweetAlertIcon.Success);
                        HoaDons?.RemoveAt(index);
                        await Session.SetItemAsync("cart", HoaDons);
                    }
                    else
                    {
                        await Swal.FireAsync("Thông báo", "Thêm Thất Bại", SweetAlertIcon.Error);
                    }
                }
            }
        }

        async Task AddHinhThucThanhToan(int i)
        {
            var hoaDon = HoaDons?[index];
            if (hoaDon != null)
            {
                if (i == 1)
                {
                    if (hoaDon.Id != Guid.Empty)
                    {
                        if (hoaDon.HinhThucThanhToans.Count() == 3)
                        {
                            hoaDon.HinhThucThanhToans.RemoveAt(2);
                        }
                        else
                        {
                            hoaDon.HinhThucThanhToans[1].KieuThanhToan = 1;
                        }
                    }
                    else
                    {
                        if (hoaDon.HinhThucThanhToans.Count() > 1)
                        {
                            hoaDon.HinhThucThanhToans.RemoveAt(1);
                        }
                        else
                        {
                            hoaDon.HinhThucThanhToans[0].KieuThanhToan = 1;
                        }
                    }
                }
                else if (i == 0)
                {
                    if (hoaDon.Id != Guid.Empty)
                    {
                        if (hoaDon.HinhThucThanhToans.Count == 3)
                        {
                            hoaDon.HinhThucThanhToans.RemoveAt(1);
                        }
                        else
                        {
                            hoaDon.HinhThucThanhToans[1].KieuThanhToan = 0;
                        }
                    }
                    else
                    {
                        if (hoaDon.HinhThucThanhToans.Count() > 1)
                        {
                            hoaDon.HinhThucThanhToans.RemoveAt(0);
                        }
                        else
                        {
                            hoaDon.HinhThucThanhToans[0].KieuThanhToan = 0;
                        }
                    }
                }
                else if (i == 2)
                {
                    if (hoaDon.Id != Guid.Empty)
                    {
                        switch (hoaDon.HinhThucThanhToans.Count())
                        {
                            case 1:
                                hoaDon.HinhThucThanhToans.AddRange(new List<HinhThucThanhToanModel>() { new HinhThucThanhToanModel() { KieuThanhToan = 1 }, new HinhThucThanhToanModel() { KieuThanhToan = 0 } });
                                break;
                            case 2:
                                hoaDon.HinhThucThanhToans.RemoveAt(1);
                                hoaDon.HinhThucThanhToans.AddRange(new List<HinhThucThanhToanModel>() { new HinhThucThanhToanModel() { KieuThanhToan = 1 }, new HinhThucThanhToanModel() { KieuThanhToan = 0 } });
                                break;
                            case 3:
                                hoaDon.HinhThucThanhToans[1].KieuThanhToan = 1;
                                hoaDon.HinhThucThanhToans[2].KieuThanhToan = 0;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        hoaDon.HinhThucThanhToans = new List<HinhThucThanhToanModel>() { new HinhThucThanhToanModel() { KieuThanhToan = 1 }, new HinhThucThanhToanModel() { KieuThanhToan = 0 } };
                    }
                }
                await Reload();
            }
        }

        async Task AddKhachHang(int id)
        {
            if (id == 999)
            {
                HoaDons[index].KhachHang = new KhachHangModel() { Ten = "Khách Lẻ" };
                HoaDons[index].IdKhachHang = null;
                await Session.SetItemAsync("cart", HoaDons);
            }
            else
            {
                var khachHang = KhachHangs?.Data.Content[id];
                if (khachHang != null)
                {
                    var hoaDon = HoaDons?[index];
                    if (hoaDon != null)
                    {
                        hoaDon.KhachHang = khachHang;
                        hoaDon.IdKhachHang = khachHang.Id;
                        await Session.SetItemAsync("cart", HoaDons);
                    }
                }
            }
        }

        public async Task AddItem(int index, BienTheModel obj, int soLuong)
        {
            if (HoaDons?[index].HoaDonChiTiets != null)
            {
                var hoaDon = HoaDons?[index];
                if (hoaDon != null)
                {
                    foreach (var x in hoaDon.HoaDonChiTiets)
                    {
                        if (x.BienThe?.Id == obj.Id)
                        {
                            x.SoLuong += soLuong;
                            await Session.SetItemAsync("cart", HoaDons);
                            HoaDons = await Session.GetItemAsync<List<HoaDonModel>>("cart");
                            return;
                        }
                    }
                    hoaDon.HoaDonChiTiets.Add(new HoaDonChiTietModel() { BienThe = obj, SoLuong = soLuong, IdBienThe = obj.Id, DonGia = obj.GiaBan });
                    hoaDon.TongTien = hoaDon.HoaDonChiTiets.Sum(c => c.BienThe?.GiaBan * c.SoLuong) + hoaDon?.TienShip ?? 0;
                    await Session.SetItemAsync("cart", HoaDons);
                }
            }
        }

        async Task RemoveItem(int index, int indexHdct)
        {
            if (HoaDons?[index].HoaDonChiTiets != null)
            {
                var hoaDon = HoaDons?[index];
                if (hoaDon != null)
                {
                    HoaDons?[index].HoaDonChiTiets.Remove(hoaDon.HoaDonChiTiets[indexHdct]);
                    hoaDon.TongTien = hoaDon.HoaDonChiTiets.Sum(c => c.BienThe?.GiaBan * c.SoLuong) + hoaDon?.TienShip ?? 0;
                }
                await Session.SetItemAsync("cart", HoaDons);
            }
        }

        async Task Update(int index, int indexHdct, bool plus, int soLuong = 0)
        {
            if (HoaDons?[index].HoaDonChiTiets != null)
            {
                if (soLuong != 0)
                {
                    HoaDons[index].HoaDonChiTiets[indexHdct].SoLuong = soLuong;
                }
                else
                {
                    if (plus)
                    {
                        HoaDons[index].HoaDonChiTiets[indexHdct].SoLuong += 1;
                    }
                    else
                    {
                        HoaDons[index].HoaDonChiTiets[indexHdct].SoLuong -= 1;
                    }
                }
                HoaDons[index].TongTien = HoaDons?[index].HoaDonChiTiets.Sum(c => c.BienThe?.GiaBan * c.SoLuong) + HoaDons?[index]?.TienShip ?? 0;
                await Session.SetItemAsync("cart", HoaDons);
            }
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("overflow");
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("choiceLoad", "{'searchEnabled': true, 'searchFields': ['label'] }", ".select-khachhang");
            }
        }

        async Task AddOrder()
        {
            HoaDons?.Add(new HoaDonModel());
            await Session.SetItemAsync("cart", HoaDons);
        }

        async Task Reload()
        {
            await Session.SetItemAsync("cart", HoaDons);
        }

        async Task RemoveOrder(int index)
        {
            HoaDons?.RemoveAt(index);
            await Session.SetItemAsync("cart", HoaDons);
        }


    }
}
