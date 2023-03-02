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
    public class SanPhamConfiguration : IEntityTypeConfiguration<SanPham>
    {
        public void Configure(EntityTypeBuilder<SanPham> builder)
        {
            builder.ToTable("SanPham");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");;

            builder.Property(c => c.NgayTao).HasDefaultValue(DateTime.Now);

            builder.Property(c => c.TrangThai).HasDefaultValue(TrangThaiSanPham.DangBan);

            builder.HasOne(c => c.SanPhamEntity).WithMany(c => c.TheLoais).HasForeignKey(c => c.IdTheLoai).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
