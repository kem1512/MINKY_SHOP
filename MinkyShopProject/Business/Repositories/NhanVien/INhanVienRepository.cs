using MinkyShopProject.Data.Models;
using MinkyShopProject.Data.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.NhanVien
{
    public interface INhanVienRepository
    {
        Task<PaginationResponse> Get(int perPage,int currentPage,int status,string? keyword);
        Task<Entities.NhanVien> GetById(Guid Id);
        Task<string> Post(Entities.NhanVien NhanVien);
        Task<string> Put(Entities.NhanVien NhanVien);
        Task<string> Delete(Guid Id);
    }
}
