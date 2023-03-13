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
    public class GiaoCaConfiguration : IEntityTypeConfiguration<GiaoCa>
    {
        public void Configure(EntityTypeBuilder<GiaoCa> builder)
        {
            builder.ToTable("GiaoCa");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.HasIndex(c => c.Ma).IsUnique();

            builder.Property(c => c.TienBanDau).HasDefaultValue(0);

            builder.Property(c => c.TongTienTrongCa).HasDefaultValue(0);

            builder.Property(c => c.TongTienMat).HasDefaultValue(0);

            builder.Property(c => c.TongTienKhac).HasDefaultValue(0);

            builder.Property(c => c.TienPhatSinh).HasDefaultValue(0);

            builder.Property(c => c.TongTienMatCaTruoc).HasDefaultValue(0);

            builder.Property(c => c.TongTienMatRut).HasDefaultValue(0);

            builder.Property(c => c.TrangThai).HasDefaultValue(0);

            builder.HasOne(c => c.NhanVien).WithMany(c => c.GiaoCas).HasForeignKey(c => c.IdNhanVienCaTiepTheo).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.NhanVien).WithMany(c => c.GiaoCas).HasForeignKey(c => c.IdNhanVienTrongCa).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
