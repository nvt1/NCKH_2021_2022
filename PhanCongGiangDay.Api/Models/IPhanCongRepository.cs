using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public interface IPhanCongRepository
    {
        Task<IEnumerable<PhanCong>> GetAllPhanCong();
        Task<PhanCong> GetPhanCongById(int phanCongId);
        Task<PhanCong> AddPhanCong(PhanCong phanCong);
        Task<PhanCong> DeletePhanCong(int phanCongId);
    }
}
