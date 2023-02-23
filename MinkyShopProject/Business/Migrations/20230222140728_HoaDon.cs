using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinkyShopProject.Business.Migrations
{
    public partial class HoaDon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "ThuocTinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 992, DateTimeKind.Local).AddTicks(5308),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 19, 9, 55, 29, 639, DateTimeKind.Local).AddTicks(1403));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "SanPham",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 992, DateTimeKind.Local).AddTicks(4576),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 19, 9, 55, 29, 639, DateTimeKind.Local).AddTicks(769));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "BienThe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 990, DateTimeKind.Local).AddTicks(8767),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 19, 9, 55, 29, 638, DateTimeKind.Local).AddTicks(6192));

            migrationBuilder.AlterColumn<string>(
                name: "Anh",
                table: "BienThe",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "");

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
                name: "HoaDon",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdNhanVien = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    Ma = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 991, DateTimeKind.Local).AddTicks(9696)),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayGiaoHang = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayNhan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoaiDonHang = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    TenNguoiNhan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "0"),
                    TienShip = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TongTien = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDon", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HoaDon_NhanVien_IdNhanVien",
                        column: x => x.IdNhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HinhThucThanhToan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 991, DateTimeKind.Local).AddTicks(3810)),
                    KieuThanhToan = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    TongTienThanhToan = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_HinhThucThanhToan_IdHoaDon",
                table: "HinhThucThanhToan",
                column: "IdHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDon_IdNhanVien",
                table: "HoaDon",
                column: "IdNhanVien");

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
                name: "IX_NhanVien_Ma",
                table: "NhanVien",
                column: "Ma",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HinhThucThanhToan");

            migrationBuilder.DropTable(
                name: "HoaDonChiTiet");

            migrationBuilder.DropTable(
                name: "HoaDon");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "ThuocTinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 19, 9, 55, 29, 639, DateTimeKind.Local).AddTicks(1403),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 992, DateTimeKind.Local).AddTicks(5308));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "SanPham",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 19, 9, 55, 29, 639, DateTimeKind.Local).AddTicks(769),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 992, DateTimeKind.Local).AddTicks(4576));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "BienThe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 19, 9, 55, 29, 638, DateTimeKind.Local).AddTicks(6192),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 990, DateTimeKind.Local).AddTicks(8767));

            migrationBuilder.AlterColumn<string>(
                name: "Anh",
                table: "BienThe",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
