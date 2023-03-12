using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinkyShopProject.Business.Entities;
using MinkyShopProject.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Configurations
{
    public class KhachHangConfiguration : IEntityTypeConfiguration<KhachHang>
    {
        public void Configure(EntityTypeBuilder<KhachHang> builder)
        {
            builder.ToTable("KhachHang");

            builder.HasKey(x => x.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.HasIndex(c => c.Ma).IsUnique();

            builder.Property(c => c.Ten).IsRequired(true);

            builder.Property(c => c.Anh).IsRequired(false);

            builder.Property(c => c.GioiTinh).HasDefaultValue(true);

            builder.Property(c => c.NgaySinh);

            builder.Property(c => c.DiaChi).IsRequired(false);

            builder.Property(c => c.Sdt).IsRequired(false);

            builder.Property(c => c.Email).IsRequired(false);

            builder.Property(c => c.MatKhau).IsRequired(false);

            builder.Property(c => c.SoLanMua).HasDefaultValue(0);

            builder.Property(c => c.TrangThai).HasDefaultValue(0);

            builder.HasOne(c => c.ViDiem).WithOne(c => c.KhachHang).HasForeignKey<KhachHang>(c => c.IdViDiem).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
