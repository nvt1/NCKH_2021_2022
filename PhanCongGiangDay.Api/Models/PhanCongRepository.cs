using Microsoft.EntityFrameworkCore;
using PhanCongGiangDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public class PhanCongRepository : IPhanCongRepository
    {
        private readonly AppDbContext appDbContext;

        public PhanCongRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<PhanCong> AddPhanCong(PhanCong phanCong)
        {
            var res = await appDbContext.PhanCong.AddAsync(phanCong);
            await appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<PhanCong> DeletePhanCong(int phanCongId)
        {
            var res = await appDbContext.PhanCong.FirstOrDefaultAsync(n => n.PhanCongId == phanCongId);
            if (res != null)
            {
                appDbContext.PhanCong.Remove(res);
                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }

        public async Task<IEnumerable<PhanCong>> GetAllPhanCong()
        {
            return await appDbContext.PhanCong.Include(u => u.GiangVien)
                .Include(u => u.NhomLop).ThenInclude(u => u.HocKy).Include(n => n.NhomLop).ThenInclude(n => n.MonHoc)
                .ToListAsync();
        }

        public async Task<PhanCong> GetPhanCongById(int phanCongId)
        {
            return await appDbContext.PhanCong.Include(u => u.GiangVien)
                .Include(u => u.NhomLop).ThenInclude(u => u.HocKy).Include(n => n.NhomLop).ThenInclude(n => n.MonHoc)
                .FirstOrDefaultAsync(pc => pc.PhanCongId == phanCongId);
        }
    }
}
