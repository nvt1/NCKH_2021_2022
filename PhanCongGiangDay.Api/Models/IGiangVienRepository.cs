 using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public interface IGiangVienRepository
    {
        Task<IEnumerable<GiangVien>> GetAllGiangVien();
        Task<GiangVien> GetGiangVienByMaGiangVien(string maGiangVien);
        Task<GiangVien> GetGiangVienByEmailGiangVien(string emailGiangVien);
        Task<GiangVien> CreateGiangVien(GiangVien giangVien);
        Task<GiangVien> UpdateGiangVien(GiangVien giangVien);
        Task<GiangVien> DeleteGiangVien(string maGiangVien);
        //Task<bool> Login(string MaGiangVien, string MatKhau);

    }
}
