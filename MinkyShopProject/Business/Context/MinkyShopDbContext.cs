using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Entities;

namespace MinkyShopProject.Business.Context
{
    public class MinkyShopDbContext : DbContext
    {
        public MinkyShopDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public DbSet<SanPham> SanPham { get; set; } = null!;

        public DbSet<BienThe> BienThe { get; set; } = null!;

        public DbSet<ThuocTinhSanPham> ThuocTinhSanPham { get; set; } = null!;

        public DbSet<BienTheChiTiet> BienTheChiTiet { get; set; } = null!;

        public DbSet<GiaTri> GiaTri { get; set; } = null!;

        public DbSet<ThuocTinh> ThuocTinh { get; set; } = null!;
    }
}
