using Bogus;
using MinkyShopProject.Business.Context;

namespace MinkyShopProject.Business.Repositories.SeedingData
{
    public class SeendingData
    {
        private readonly MinkyShopDbContext _Context;

        public SeendingData(MinkyShopDbContext Context)
        {
            _Context = Context;
        }
        private List<Guid> ListGuid(int n)
        {
            var Guids = new List<Guid>();
            while (Guids.Count < n)
            {
                var newGuid = Guid.NewGuid();
                if (!Guids.Contains(newGuid))
                    Guids.Add(newGuid);
            }
            return Guids;
        }
        public Faker faker = new Faker();
        public async Task SeendingviDiem()
        {
            int soluong = 30;
            var ListId = ListGuid(soluong);
            for (int i = 0; i < soluong; i++)
            {
                _Context.ViDiem.Add(new Entities.ViDiem()
                {
                    Id = ListId[i],
                    SoDiemDaCong = faker.Random.Int(min: 100, max: 1000),
                    SoDiemDaDung = faker.Random.Int(min: 100, max: 1000),
                    TongDiem = faker.Random.Int(min: 100, max: 1000),
                    TrangThai = faker.Random.Int(min: 0, max: 1),
                }
                );
            }
            await _Context.SaveChangesAsync();
        }
        public async Task SeeddingNhanVien()
        {
            var startYear = new DateTime(2000, 1, 1);
            var endYear = DateTime.Now;
            var range = (endYear - startYear).Days;
            int soluong = 30;
            var ListId = ListGuid(soluong);
            for (int i = 0; i < soluong; i++)
            {
                _Context.NhanVien.Add(new Entities.NhanVien()
                {
                    Id = ListId[i],
                    NgaySinh = startYear.AddDays(faker.Random.Int(0, range)),
                    Sdt = faker.Random.Number(0, 100000).ToString().PadLeft(11, '0'),
                    GioiTinh = faker.Random.Bool(),
                    TrangThai = faker.Random.Int(min: 0, max: 1),
                    Email = faker.Internet.Email(),
                    Anh = faker.Image.LoremFlickrUrl(),
                    MatKhau = faker.Internet.Password(5),
                    DiaChi = faker.Address.FullAddress(),
                    Ma = (i + 1).ToString().PadLeft(2, '0'),
                    Ten = faker.Name.FirstName(),
                    NgayTao = faker.Date.Between(DateTime.Today.AddMonths(-6), DateTime.Now),
                    VaiTro = faker.Random.Int(min: 0, max: 1),
                }
                );
            }
            await _Context.SaveChangesAsync();
        }
        public async Task SeeddingHoaDon()
        {
            int soluong = 1000;
            var ListId = ListGuid(soluong);
            var IdKhachHang = _Context.KhachHang.Select(x => x.Id).ToList();
            var IdNhanVien = _Context.NhanVien.Select(x => x.Id).ToList();
            var startYear = new DateTime(2000, 1, 1);
            var endYear = DateTime.Now;
            var range = (endYear - startYear).Days;
            for (int i = 0; i < soluong; i++)
            {
                _Context.HoaDon.Add(new Entities.HoaDon()
                {
                    Id = ListId[i],
                    IdKhachHang = faker.PickRandom(IdKhachHang),
                    IdNhanVien = faker.PickRandom(IdNhanVien),
                    Ma = (i + 1).ToString().PadLeft(4, '0'),
                    Sdt = faker.Random.Number(0, 980000000).ToString().PadLeft(11, '0'),
                    DiaChi = faker.Address.FullAddress(),
                    NgayNhan = faker.Date.Between(DateTime.Today.AddMonths(-1), DateTime.Now),
                    NgayGiaoHang = faker.Date.Between(DateTime.Today.AddMonths(-1), DateTime.Now),
                    NgayThanhToan = faker.Date.Between(DateTime.Today.AddMonths(-1), DateTime.Now),
                    GhiChu = faker.Lorem.Sentence(),
                    TienShip = faker.Random.Float(min: 5000f, max: 15000f),
                    TongTien = faker.Random.Float(min: 100000f, max: 100000000f),
                    TrangThai = faker.Random.Int(min: 0, max: 1),
                    LoaiDonHang = faker.Random.Int(min: 0, max: 1),
                    TenNguoiNhan = faker.Name.FirstName(),
                    NgayTao = faker.Date.Between(DateTime.Today.AddMonths(-6), DateTime.Now),
                });
            }
            await _Context.SaveChangesAsync();
        }
        public async Task SeeddingKhachHang()
        {
            int soluong = 30;
            var ListId = ListGuid(soluong);
            var IdVidiem = ListGuid(soluong);
            var startYear = new DateTime(2000, 1, 1);
            var endYear = DateTime.Now;
            var range = (endYear - startYear).Days;
            for (int i = 0; i < soluong; i++)
            {
                _Context.KhachHang.Add(new Entities.KhachHang()
                {
                    Id = ListId[i],
                    // IdViDiem = IdVidiem[i],
                    Ma = (i + 1).ToString().PadLeft(2, '0'),
                    Ten = faker.Name.FirstName(),
                    NgaySinh = startYear.AddDays(faker.Random.Int(0, range)),
                    Sdt = faker.Random.Number(0, 100000).ToString().PadLeft(11, '0'),
                    DiaChi = faker.Address.FullAddress(),
                    GioiTinh = faker.Random.Bool(),
                    Email = faker.Internet.Email(),
                    Anh = faker.Image.LoremFlickrUrl(),
                    MatKhau = faker.Internet.Password(5),
                    TrangThai = faker.Random.Int(min: 0, max: 1),
                    NgayTao = faker.Date.Between(DateTime.Today.AddMonths(-6), DateTime.Now),
                });
            }
            await _Context.SaveChangesAsync();
        }
    }
}
