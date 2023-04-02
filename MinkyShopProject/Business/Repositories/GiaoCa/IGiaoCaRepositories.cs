using MinkyShopProject.Business.Entities;
using MinkyShopProject.Common;
using MinkyShopProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinkyShopProject.Business.Repositories.GiaoCa
{
    public interface IGiaoCaRepositories
    {
        Task<Response> KhaiBaoDauCa(GiaoCaModels.GiaoCaCreateModel model);
        Task<Response> GetCaHienThai(Guid Id,DateTime Time);
        Task<Response> KetCa(Guid Id,DateTime Time,GiaoCaModels.GiaoCaEditModel model);
        Task<Response<int>> GetHoaDonCa(Guid IdNhanVien, DateTime Time);
    }
}
