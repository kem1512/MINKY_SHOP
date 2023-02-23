﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Configurations
{
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.ToTable("Voucher");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.HasIndex(c => c.Ma).IsUnique();

            builder.Property(c => c.Ten).HasMaxLength(30).HasDefaultValue(null);

            builder.Property(c => c.LoaiGiamGia).HasDefaultValue(LoaiGiamGia.HoaDon);

            builder.Property(c => c.HinhThucGiamGia).HasDefaultValue(HinhThucGiamGia.PhanTram);

            builder.Property(c => c.SoTienCan).HasDefaultValue(0);

            builder.Property(c => c.SoTienGiam).HasDefaultValue(0);

            builder.Property(c => c.NgayApDung).HasDefaultValue(DateTime.Now);

            builder.Property(c => c.NgayKetThuc).HasDefaultValue(DateTime.Now);

            builder.Property(c => c.SoLuong).HasDefaultValue(0);

            builder.Property(c => c.MoTa).HasDefaultValue(null);

            builder.Property(c => c.TrangThai).HasDefaultValue(TrangThaiVoucher.DangHoatDong);
        }
    }
}
