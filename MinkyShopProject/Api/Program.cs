using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Business.Repositories.BienThe;
using MinkyShopProject.Business.Repositories.NhanVien;
using MinkyShopProject.Business.Repositories.KhachHang;
using MinkyShopProject.Business.Repositories.SanPham;
using MinkyShopProject.Business.Repositories.ThuocTinh;
using MinkyShopProject.Business.Repositories.ViDiem;

var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MinkyShopDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IThuocTinhRepository, ThuocTinhRepository>();

builder.Services.AddScoped<IBienTheRepository, BienTheRepository>();

builder.Services.AddScoped<ISanPhamRepository, SanPhamRepository>();

builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();

builder.Services.AddScoped<IViDiemRepository, ViDiemRepository>();

builder.Services.AddScoped<IKhachHangRepository, KhachHangRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
