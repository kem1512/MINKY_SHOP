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

            builder.Property(c => c.Ten).HasDefaultValue(null);

            builder.Property(c => c.Anh).HasDefaultValue(null);

            builder.Property(c => c.GioiTinh).HasDefaultValue(null);

            builder.Property(c => c.NgaySinh).HasDefaultValue(null);

            builder.Property(c => c.DiaChi).HasDefaultValue(null);

            builder.Property(c => c.Sdt).HasDefaultValue(0);

            builder.Property(c => c.Email).HasDefaultValue(null);

            builder.Property(c => c.MatKhau).HasDefaultValue(null);

            builder.Property(c => c.SoLanMua).HasDefaultValue(0);

            builder.Property(c => c.TrangThai).HasDefaultValue(TrangThaiKhachHang.Online);
        }
    }
}
