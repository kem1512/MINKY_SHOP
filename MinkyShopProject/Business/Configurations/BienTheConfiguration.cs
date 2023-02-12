using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinkyShopProject.Business.Entities;
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

            builder.HasOne(c => c.SanPham).WithMany(c => c.BienThes).HasForeignKey(c => c.IdSanPham).OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
