using Microsoft.EntityFrameworkCore;
using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public class KhoaRepository : IKhoaRepository
    {
        private readonly AppDbContext appDbContext;

        public KhoaRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Khoa>> GetAllKhoa()
        {
           return await appDbContext.Khoa.ToListAsync();
        }
        public async Task<Khoa> GetKhoaByMaKhoa(string maKhoa)
        {
            return await appDbContext.Khoa.FirstOrDefaultAsync(n => n.MaKhoa == maKhoa);
        }
        public async Task<Khoa> CreateKhoa(Khoa khoa)
        {
            var res = await appDbContext.Khoa.AddAsync(khoa);
            await appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<Khoa> UpdateKhoa(Khoa khoa)
        {
            var res = await appDbContext.Khoa.FirstOrDefaultAsync(n => n.MaKhoa == khoa.MaKhoa);
            if(res != null)
            {
                res.MaKhoa = khoa.MaKhoa;
                res.TenKhoa = khoa.TenKhoa;

                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }

        public async Task<Khoa> DeleteKhoa(string maKhoa)
        {
            var res = await appDbContext.Khoa.FirstOrDefaultAsync(n => n.MaKhoa == maKhoa);
            if(res != null)
            {
                appDbContext.Khoa.Remove(res);
                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }

    
    }
}
