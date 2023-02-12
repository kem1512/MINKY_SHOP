using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MinkyShopProject.Business.Context;
using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.ViewModels;

namespace MinkyShopProject.Business.Repositories.SanPham
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private readonly MinkyShopDbContext _context;
        private readonly IMapper _mapper;

        public SanPhamRepository(MinkyShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SanPhamModel>> GetSanPhams()
        {
            var entities = await _context.SanPham.ToListAsync();
            var data = _mapper.Map<IEnumerable<Entities.SanPham>, IEnumerable<SanPhamModel>>(entities);
            return data;
        }
    }
}
