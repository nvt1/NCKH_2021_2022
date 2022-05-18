using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public interface IKhoaRepository
    {
        Task<IEnumerable<Khoa>> GetAllKhoa();
        Task<Khoa> GetKhoaByMaKhoa(string maKhoa);
        Task<Khoa> CreateKhoa(Khoa khoa);
        Task<Khoa> UpdateKhoa(Khoa khoa);
        Task<Khoa> DeleteKhoa(string maKhoa);
     
    }
}
