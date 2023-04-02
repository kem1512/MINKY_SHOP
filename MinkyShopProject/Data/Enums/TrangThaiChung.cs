﻿using System.ComponentModel;

namespace MinkyShopProject.Data.Enums
{
    public enum TrangThaiChung
    {

    }

    public enum TrangThaiHoaDon
    {
        HoanThanh, DaHuy, Ship, ChoXacNhan, DaXacNhan
    }

    public enum TrangThaiHoaDonChiTiet
    {
        Done, Unfinished, Cancelled,
    }

    public enum TrangThaiGiaoHang
    {
        ChoXacNhan, ChoLayHang, DangGiao, DaGiao, DaHuy, DaGiaoNhungKhachChiNhanMotPhanHang, GiaoThatBai, KhachKhongNgheMay
    }

    public enum LoaiHoaDon
    {
        BanTaiQuay, DatHang, GiaoHang
    }
}
