using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinkyShopProject.Business.Migrations
{
    public partial class ThuocTinhDong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SanPham",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 19, 9, 55, 29, 639, DateTimeKind.Local).AddTicks(769))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SanPham", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThuocTinh",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(NEWID())"),
                    Ten = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    TrangThai = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 19, 9, 55, 29, 639, DateTimeKind.Local).AddTicks(1403))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuocTinh", x => x.Id);
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
                    Anh = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: ""),
                    NgayTao = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2023, 2, 19, 9, 55, 29, 638, DateTimeKind.Local).AddTicks(6192)),
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
                name: "IX_GiaTri_IdThuocTinh",
                table: "GiaTri",
                column: "IdThuocTinh");

            migrationBuilder.CreateIndex(
                name: "IX_ThuocTinhSanPham_IdSanPham",
                table: "ThuocTinhSanPham",
                column: "IdSanPham");

            migrationBuilder.CreateIndex(
                name: "IX_ThuocTinhSanPham_IdThuocTinh",
                table: "ThuocTinhSanPham",
                column: "IdThuocTinh");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BienTheChiTiet");

            migrationBuilder.DropTable(
                name: "BienThe");

            migrationBuilder.DropTable(
                name: "GiaTri");

            migrationBuilder.DropTable(
                name: "ThuocTinhSanPham");

            migrationBuilder.DropTable(
                name: "SanPham");

            migrationBuilder.DropTable(
                name: "ThuocTinh");
        }
    }
}
