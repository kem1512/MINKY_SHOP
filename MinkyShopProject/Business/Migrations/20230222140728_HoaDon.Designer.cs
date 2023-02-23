﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MinkyShopProject.Business.Context;

#nullable disable

namespace MinkyShopProject.Business.Migrations
{
    [DbContext(typeof(MinkyShopDbContext))]
    [Migration("20230222140728_HoaDon")]
    partial class HoaDon
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MinkyShopProject.Business.Entities.BienThe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(NEWID())");

                    b.Property<string>("Anh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("GiaBan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<Guid>("IdSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("NgayTao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 2, 22, 21, 7, 27, 990, DateTimeKind.Local).AddTicks(8767));

                    b.Property<string>("Sku")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)");

                    b.Property<int>("SoLuong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("TrangThai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("IdSanPham");

                    b.HasIndex("Sku")
                        .IsUnique();

                    b.ToTable("BienThe", (string)null);
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.BienTheChiTiet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(NEWID())");

                    b.Property<Guid>("IdBienThe")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdGiaTri")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdThuocTinhSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TrangThai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("IdBienThe");

                    b.HasIndex("IdGiaTri");

                    b.HasIndex("IdThuocTinhSanPham");

                    b.ToTable("BienTheChiTiet", (string)null);
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.GiaTri", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(NEWID())");

                    b.Property<Guid>("IdThuocTinh")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ten")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("IdThuocTinh");

                    b.ToTable("GiaTri", (string)null);
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.HinhThucThanhToan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(NEWID())");

                    b.Property<string>("GhiChu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdHoaDon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

                    b.Property<int>("KieuThanhToan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<DateTime>("NgayTao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 2, 22, 21, 7, 27, 991, DateTimeKind.Local).AddTicks(3810));

                    b.Property<float>("TongTienThanhToan")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.HasKey("Id");

                    b.HasIndex("IdHoaDon");

                    b.ToTable("HinhThucThanhToan", (string)null);
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.HoaDon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(NEWID())");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IdKhachHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

                    b.Property<Guid>("IdNhanVien")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

                    b.Property<int>("LoaiDonHang")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(1);

                    b.Property<string>("Ma")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("NgayGiaoHang")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayNhan")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("NgayTao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 2, 22, 21, 7, 27, 991, DateTimeKind.Local).AddTicks(9696));

                    b.Property<DateTime>("NgayThanhToan")
                        .HasColumnType("datetime2");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("0");

                    b.Property<string>("TenNguoiNhan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("TienShip")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<float>("TongTien")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<int>("TrangThai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("IdNhanVien");

                    b.HasIndex("Ma")
                        .IsUnique();

                    b.ToTable("HoaDon", (string)null);
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.HoaDonChiTiet", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(NEWID())");

                    b.Property<float>("DonGia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("real")
                        .HasDefaultValue(0f);

                    b.Property<Guid>("HoaDonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdBienThe")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

                    b.Property<Guid>("IdHoaDon")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

                    b.Property<int>("SoLuong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("HoaDonId");

                    b.HasIndex("IdBienThe");

                    b.ToTable("HoaDonChiTiet", (string)null);
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.NhanVien", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(NEWID())");

                    b.Property<string>("Anh")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GioiTinh")
                        .HasColumnType("bit");

                    b.Property<string>("Ma")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MatKhau")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("NgaySinh")
                        .HasColumnType("datetime2");

                    b.Property<string>("Sdt")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("0");

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("VaiTro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.HasIndex("Ma")
                        .IsUnique();

                    b.ToTable("NhanVien", (string)null);
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.SanPham", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(NEWID())");

                    b.Property<DateTime>("NgayTao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 2, 22, 21, 7, 27, 992, DateTimeKind.Local).AddTicks(4576));

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TrangThai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("SanPham", (string)null);
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.ThuocTinh", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(NEWID())");

                    b.Property<DateTime>("NgayTao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2023, 2, 22, 21, 7, 27, 992, DateTimeKind.Local).AddTicks(5308));

                    b.Property<string>("Ten")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("TrangThai")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("Id");

                    b.ToTable("ThuocTinh", (string)null);
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.ThuocTinhSanPham", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValueSql("(NEWID())");

                    b.Property<Guid>("IdSanPham")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdThuocTinh")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("IdSanPham");

                    b.HasIndex("IdThuocTinh");

                    b.ToTable("ThuocTinhSanPham", (string)null);
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.BienThe", b =>
                {
                    b.HasOne("MinkyShopProject.Business.Entities.SanPham", "SanPham")
                        .WithMany("BienThes")
                        .HasForeignKey("IdSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.BienTheChiTiet", b =>
                {
                    b.HasOne("MinkyShopProject.Business.Entities.BienThe", "BienThe")
                        .WithMany("BienTheChiTiets")
                        .HasForeignKey("IdBienThe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinkyShopProject.Business.Entities.GiaTri", "GiaTri")
                        .WithMany("BienTheChiTiets")
                        .HasForeignKey("IdGiaTri")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinkyShopProject.Business.Entities.ThuocTinhSanPham", "ThuocTinhSanPham")
                        .WithMany("BienTheChiTiets")
                        .HasForeignKey("IdThuocTinhSanPham")
                        .IsRequired();

                    b.Navigation("BienThe");

                    b.Navigation("GiaTri");

                    b.Navigation("ThuocTinhSanPham");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.GiaTri", b =>
                {
                    b.HasOne("MinkyShopProject.Business.Entities.ThuocTinh", "ThuocTinhs")
                        .WithMany("GiaTris")
                        .HasForeignKey("IdThuocTinh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ThuocTinhs");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.HinhThucThanhToan", b =>
                {
                    b.HasOne("MinkyShopProject.Business.Entities.HoaDon", "HoaDon")
                        .WithMany("HinhThucThanhToans")
                        .HasForeignKey("IdHoaDon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HoaDon");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.HoaDon", b =>
                {
                    b.HasOne("MinkyShopProject.Business.Entities.NhanVien", "NhanVien")
                        .WithMany("HoaDons")
                        .HasForeignKey("IdNhanVien")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NhanVien");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.HoaDonChiTiet", b =>
                {
                    b.HasOne("MinkyShopProject.Business.Entities.HoaDon", "HoaDon")
                        .WithMany("HoaDonChiTiets")
                        .HasForeignKey("HoaDonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinkyShopProject.Business.Entities.BienThe", "BienThe")
                        .WithMany("HoaDonChiTiets")
                        .HasForeignKey("IdBienThe")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BienThe");

                    b.Navigation("HoaDon");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.ThuocTinhSanPham", b =>
                {
                    b.HasOne("MinkyShopProject.Business.Entities.SanPham", "SanPham")
                        .WithMany("ThuocTinhSanPhams")
                        .HasForeignKey("IdSanPham")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MinkyShopProject.Business.Entities.ThuocTinh", "ThuocTinh")
                        .WithMany("ThuocTinhSanPhams")
                        .HasForeignKey("IdThuocTinh")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SanPham");

                    b.Navigation("ThuocTinh");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.BienThe", b =>
                {
                    b.Navigation("BienTheChiTiets");

                    b.Navigation("HoaDonChiTiets");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.GiaTri", b =>
                {
                    b.Navigation("BienTheChiTiets");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.HoaDon", b =>
                {
                    b.Navigation("HinhThucThanhToans");

                    b.Navigation("HoaDonChiTiets");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.NhanVien", b =>
                {
                    b.Navigation("HoaDons");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.SanPham", b =>
                {
                    b.Navigation("BienThes");

                    b.Navigation("ThuocTinhSanPhams");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.ThuocTinh", b =>
                {
                    b.Navigation("GiaTris");

                    b.Navigation("ThuocTinhSanPhams");
                });

            modelBuilder.Entity("MinkyShopProject.Business.Entities.ThuocTinhSanPham", b =>
                {
                    b.Navigation("BienTheChiTiets");
                });
#pragma warning restore 612, 618
        }
    }
}
