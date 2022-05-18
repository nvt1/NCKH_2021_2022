using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public interface IMonHocRepository
    {
        Task<IEnumerable<MonHoc>> GetAllMonHoc();
        Task<MonHoc> GetMonHocByMaMonHoc(string maMonhoc);
        Task<IEnumerable<MonHoc>> GetMonHocByTenMonHoc(string tenMonHoc);
        Task<MonHoc> CreateMonHoc(MonHoc monHoc);
        Task<MonHoc> UpdateMonHoc(MonHoc monHoc);
        Task<MonHoc> DeleteMonHoc(string maMonHoc);
    }
}
