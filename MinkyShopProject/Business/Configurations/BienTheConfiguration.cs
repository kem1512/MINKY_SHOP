using Microsoft.EntityFrameworkCore;
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
    public class BienTheConfiguration : IEntityTypeConfiguration<BienThe>
    {
        public void Configure(EntityTypeBuilder<BienThe> builder)
        {
            builder.ToTable("BienThe");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.Sku).HasColumnType("VARCHAR(20)");

            builder.Property(c => c.NgayTao).HasDefaultValue(DateTime.Now);

            builder.Property(c => c.SoLuong).HasDefaultValue(0);

            builder.Property(c => c.GiaBan).HasDefaultValue(0);

            builder.Property(c => c.TrangThai).HasDefaultValue(TrangThaiBienThe.DangBan);

            builder.HasOne(c => c.SanPham).WithMany(c => c.BienThes).HasForeignKey(c => c.IdSanPham).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
