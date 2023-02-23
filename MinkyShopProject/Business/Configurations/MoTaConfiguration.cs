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
    public class MoTaConfiguration : IEntityTypeConfiguration<MoTa>
    {
        public void Configure(EntityTypeBuilder<MoTa> builder)
        {
            builder.ToTable("MoTa");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).HasDefaultValueSql("(NEWID())");;

            builder.HasOne(c => c.SanPham).WithOne(c => c.MoTa).HasForeignKey<MoTa>(c => c.IdSanPham);
        }
    }
}
