using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinkyShopProject.Business.Migrations
{
    public partial class Voucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "ThuocTinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 512, DateTimeKind.Local).AddTicks(4323),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 992, DateTimeKind.Local).AddTicks(5308));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "SanPham",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 512, DateTimeKind.Local).AddTicks(3598),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 992, DateTimeKind.Local).AddTicks(4576));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoaDon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 511, DateTimeKind.Local).AddTicks(7089),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 991, DateTimeKind.Local).AddTicks(9696));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HinhThucThanhToan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 511, DateTimeKind.Local).AddTicks(1438),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 991, DateTimeKind.Local).AddTicks(3810));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "BienThe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 509, DateTimeKind.Local).AddTicks(8362),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 990, DateTimeKind.Local).AddTicks(8767));

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdViDiem = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<bool>(type: "bit", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sdt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLanMua = table.Column<int>(type: "int", nullable: false),
                    TrangThaiKhachHang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.Id);
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
                    NgayApDung = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 512, DateTimeKind.Local).AddTicks(9624)),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 512, DateTimeKind.Local).AddTicks(9743)),
                    SoLuong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voucher", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DanhGia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdBienThe = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdKhachHang = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoDanhGia = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NgayDanhGia = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 510, DateTimeKind.Local).AddTicks(1427)),
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                name: "VoucherLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    IdHoaDon = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    IdVoucher = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValue: new Guid("00000000-0000-0000-0000-000000000000")),
                    TienTruocKhiGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    TienSauKhiGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    SoTienGiam = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 513, DateTimeKind.Local).AddTicks(1878))
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
                name: "IX_MoTa_IdSanPham",
                table: "MoTa",
                column: "IdSanPham",
                unique: true);

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
                name: "DanhGia");

            migrationBuilder.DropTable(
                name: "MoTa");

            migrationBuilder.DropTable(
                name: "VoucherKhachHang");

            migrationBuilder.DropTable(
                name: "VoucherLog");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "Voucher");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "ThuocTinh",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 992, DateTimeKind.Local).AddTicks(5308),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 512, DateTimeKind.Local).AddTicks(4323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "SanPham",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 992, DateTimeKind.Local).AddTicks(4576),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 512, DateTimeKind.Local).AddTicks(3598));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HoaDon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 991, DateTimeKind.Local).AddTicks(9696),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 511, DateTimeKind.Local).AddTicks(7089));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "HinhThucThanhToan",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 991, DateTimeKind.Local).AddTicks(3810),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 511, DateTimeKind.Local).AddTicks(1438));

            migrationBuilder.AlterColumn<DateTime>(
                name: "NgayTao",
                table: "BienThe",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2023, 2, 22, 21, 7, 27, 990, DateTimeKind.Local).AddTicks(8767),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2023, 2, 22, 23, 33, 5, 509, DateTimeKind.Local).AddTicks(8362));
        }
    }
}
