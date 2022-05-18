using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public interface IHocKyRepository
    {
        Task<IEnumerable<HocKy>> GetAllHocKy();
        Task<HocKy> GetHocKyById(int id);
        Task<HocKy> CreateHocKy(HocKy hocKy);
        Task<HocKy> UpdateHocKy(HocKy hocKy);
        Task<HocKy> DeleteHocKy(int id);
    }
}
