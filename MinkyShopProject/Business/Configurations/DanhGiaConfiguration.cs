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
    public class DanhGiaConfiguration : IEntityTypeConfiguration<DanhGia>
    {
        public void Configure(EntityTypeBuilder<DanhGia> builder)
        {
            builder.ToTable("DanhGia");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");

            builder.Property(c => c.IdKhachHang).HasDefaultValue(Guid.Empty);

            builder.Property(c => c.SoDanhGia).HasDefaultValue(0);

            builder.Property(c => c.NgayDanhGia).HasDefaultValue(DateTime.Now);

            builder.Property(c => c.Anh).HasDefaultValue(null);

            builder.Property(c => c.TrangThai).HasDefaultValue(TrangThaiDanhGia.HienThi);

            builder.HasOne(c => c.BienThe).WithMany(c => c.DanhGias).HasForeignKey(c => c.IdBienThe).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.KhachHang).WithMany(c => c.DanhGias).HasForeignKey(c => c.IdKhachHang).OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.HoaDon).WithOne(c => c.DanhGia).HasForeignKey<DanhGia>(c => c.IdHoaDon);
        }
    }
}
