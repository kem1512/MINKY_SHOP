﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinkyShopProject.Business.Migrations
{
    public partial class MinkyShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "0"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaiTro = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdTheLoai = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 334, DateTimeKind.Local).AddTicks(5734))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SanPham_SanPham_IdTheLoai",
                        column: x => x.IdTheLoai,
                        principalTable: "SanPham",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ThuocTinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    Ten = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 334, DateTimeKind.Local).AddTicks(8286))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuocTinh", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViDiem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TongDiem = table.Column<float>(type: "real", nullable: false),
                    SoDiemDaDung = table.Column<float>(type: "real", nullable: false),
                    SoDiemDaCong = table.Column<float>(type: "real", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViDiem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voucher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LoaiGiamGia = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    HinhThucGiamGia = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    SoTienCan = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    SoTienGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    NgayApDung = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 335, DateTimeKind.Local).AddTicks(3919)),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 335, DateTimeKind.Local).AddTicks(4034)),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiaoCa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ThoiGianNhanCa = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 331, DateTimeKind.Local).AddTicks(3284)),
                    ThoiGianGiaoCa = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 331, DateTimeKind.Local).AddTicks(3416)),
                    IdNhanVienTrongCa = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNhanVienCaTiepTheo = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TienBanDau = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TongTienTrongCa = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TongTienMat = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TongTienKhac = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TienPhatSinh = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    GhiChuPhatSinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TongTienMatCaTruoc = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    ThoiGianReset = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 331, DateTimeKind.Local).AddTicks(4422)),
                    TongTienMatRut = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaoCa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiaoCa_NhanVien_IdNhanVienTrongCa",
                        column: x => x.IdNhanVienTrongCa,
                        principalTable: "NhanVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BienThe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    GiaBan = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    Sku = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 330, DateTimeKind.Local).AddTicks(1221)),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienThe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BienThe_SanPham_IdSanPham",
                        column: x => x.IdSanPham,
                        principalTable: "SanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MoTa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoTa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoTa_SanPham_IdSanPham",
                        column: x => x.IdSanPham,
                        principalTable: "SanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GiaTri",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdThuocTinh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiaTri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GiaTri_ThuocTinh_IdThuocTinh",
                        column: x => x.IdThuocTinh,
                        principalTable: "ThuocTinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThuocTinhSanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdThuocTinh = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuocTinhSanPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThuocTinhSanPham_SanPham_IdSanPham",
                        column: x => x.IdSanPham,
                        principalTable: "SanPham",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThuocTinhSanPham_ThuocTinh_IdThuocTinh",
                        column: x => x.IdThuocTinh,
                        principalTable: "ThuocTinh",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdViDiem = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 333, DateTimeKind.Local).AddTicks(8497)),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "0"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    SoLanMua = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KhachHang_ViDiem_IdViDiem",
                        column: x => x.IdViDiem,
                        principalTable: "ViDiem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BienTheChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdThuocTinhSanPham = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdBienThe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGiaTri = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BienTheChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BienTheChiTiet_BienThe_IdBienThe",
                        column: x => x.IdBienThe,
                        principalTable: "BienThe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BienTheChiTiet_GiaTri_IdGiaTri",
                        column: x => x.IdGiaTri,
                        principalTable: "GiaTri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BienTheChiTiet_ThuocTinhSanPham_IdThuocTinhSanPham",
                        column: x => x.IdThuocTinhSanPham,
                        principalTable: "ThuocTinhSanPham",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GioHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 332, DateTimeKind.Local).AddTicks(6030))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHang_KhachHang_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 333, DateTimeKind.Local).AddTicks(4368)),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiDonHang = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    TenNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "0"),
                    TienShip = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TongTien = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    KhachHangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDon_KhachHang_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDon_NhanVien_IdNhanVien",
                        column: x => x.IdNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherKhachHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    VoucherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhachHangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherKhachHang", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherKhachHang_KhachHang_KhachHangId",
                        column: x => x.KhachHangId,
                        principalTable: "KhachHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherKhachHang_Voucher_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GioHangChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdBienThe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGioHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DonGia = table.Column<float>(type: "real", nullable: false, defaultValue: 0f)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GioHangChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_BienThe_IdBienThe",
                        column: x => x.IdBienThe,
                        principalTable: "BienThe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GioHangChiTiet_GioHang_IdGioHang",
                        column: x => x.IdGioHang,
                        principalTable: "GioHang",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdBienThe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoDanhGia = table.Column<int>(type: "int", nullable: false, defaultValue: 5),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 330, DateTimeKind.Local).AddTicks(4792)),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DanhGia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DanhGia_BienThe_IdBienThe",
                        column: x => x.IdBienThe,
                        principalTable: "BienThe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGia_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DanhGia_KhachHang_IdKhachHang",
                        column: x => x.IdKhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HinhThucThanhToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 332, DateTimeKind.Local).AddTicks(8472)),
                    KieuThanhToan = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TongTienThanhToan = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhThucThanhToan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HinhThucThanhToan_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HoaDonChiTiet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdBienThe = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DonGia = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    HoaDonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDonChiTiet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_BienThe_IdBienThe",
                        column: x => x.IdBienThe,
                        principalTable: "BienThe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HoaDonChiTiet_HoaDon_HoaDonId",
                        column: x => x.HoaDonId,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    IdVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    TienTruocKhiGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TienSauKhiGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    SoTienGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 28, 16, 22, 18, 335, DateTimeKind.Local).AddTicks(6266))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoucherLog_HoaDon_IdHoaDon",
                        column: x => x.IdHoaDon,
                        principalTable: "HoaDon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherLog_Voucher_IdVoucher",
                        column: x => x.IdVoucher,
                        principalTable: "Voucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BienThe_IdSanPham",
                table: "BienThe",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_BienThe_Sku",
                table: "BienThe",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BienTheChiTiet_IdBienThe",
                table: "BienTheChiTiet",
                column: "IdBienThe");

            migrationBuilder.CreateIndex(
                name: "IX_BienTheChiTiet_IdGiaTri",
                table: "BienTheChiTiet",
                column: "IdGiaTri");

            migrationBuilder.CreateIndex(
                name: "IX_BienTheChiTiet_IdThuocTinhSanPham",
                table: "BienTheChiTiet",
                column: "IdThuocTinhSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IdBienThe",
                table: "DanhGia",
                column: "IdBienThe");

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IdHoaDon",
                table: "DanhGia",
                column: "IdHoaDon",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DanhGia_IdKhachHang",
                table: "DanhGia",
                column: "IdKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoCa_IdNhanVienTrongCa",
                table: "GiaoCa",
                column: "IdNhanVienTrongCa");

            migrationBuilder.CreateIndex(
                name: "IX_GiaoCa_Ma",
                table: "GiaoCa",
                column: "Ma",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GiaTri_IdThuocTinh",
                table: "GiaTri",
                column: "IdThuocTinh");

            migrationBuilder.CreateIndex(
                name: "IX_GioHang_IdKhachHang",
                table: "GioHang",
                column: "IdKhachHang",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_IdBienThe",
                table: "GioHangChiTiet",
                column: "IdBienThe");

            migrationBuilder.CreateIndex(
                name: "IX_GioHangChiTiet_IdGioHang",
                table: "GioHangChiTiet",
                column: "IdGioHang");

            migrationBuilder.CreateIndex(
                name: "IX_HinhThucThanhToan_IdHoaDon",
                table: "HinhThucThanhToan",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdNhanVien",
                table: "HoaDon",
                column: "IdNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_KhachHangId",
                table: "HoaDon",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_Ma",
                table: "HoaDon",
                column: "Ma",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_HoaDonId",
                table: "HoaDonChiTiet",
                column: "HoaDonId");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDonChiTiet_IdBienThe",
                table: "HoaDonChiTiet",
                column: "IdBienThe");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_IdViDiem",
                table: "KhachHang",
                column: "IdViDiem",
                unique: true,
                filter: "[IdViDiem] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_Ma",
                table: "KhachHang",
                column: "Ma",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoTa_IdSanPham",
                table: "MoTa",
                column: "IdSanPham",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_Ma",
                table: "NhanVien",
                column: "Ma",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SanPham_IdTheLoai",
                table: "SanPham",
                column: "IdTheLoai");

            migrationBuilder.CreateIndex(
                name: "IX_ThuocTinhSanPham_IdSanPham",
                table: "ThuocTinhSanPham",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ThuocTinhSanPham_IdThuocTinh",
                table: "ThuocTinhSanPham",
                column: "IdThuocTinh");

            migrationBuilder.CreateIndex(
                name: "IX_Voucher_Ma",
                table: "Voucher",
                column: "Ma",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherKhachHang_KhachHangId",
                table: "VoucherKhachHang",
                column: "KhachHangId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherKhachHang_VoucherId",
                table: "VoucherKhachHang",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLog_IdHoaDon",
                table: "VoucherLog",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherLog_IdVoucher",
                table: "VoucherLog",
                column: "IdVoucher");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BienTheChiTiet");

            migrationBuilder.DropTable(
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "GiaoCa");

            migrationBuilder.DropTable(
                name: "GioHangChiTiet");

            migrationBuilder.DropTable(
                name: "HinhThucThanhToan");

            migrationBuilder.DropTable(
                name: "HoaDonChiTiet");

            migrationBuilder.DropTable(
                name: "MoTa");

            migrationBuilder.DropTable(
                name: "VoucherKhachHang");

            migrationBuilder.DropTable(
                name: "VoucherLog");

            migrationBuilder.DropTable(
                name: "GiaTri");

            migrationBuilder.DropTable(
                name: "ThuocTinhSanPham");

            migrationBuilder.DropTable(
                name: "GioHang");

            migrationBuilder.DropTable(
                name: "BienThe");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.DropTable(
                name: "ThuocTinh");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "ViDiem");
        }
    }
}
