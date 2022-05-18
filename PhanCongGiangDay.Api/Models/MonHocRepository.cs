using Microsoft.EntityFrameworkCore;
using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public class MonHocRepository : IMonHocRepository
    {
        private readonly AppDbContext appDbContext;

        public MonHocRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IEnumerable<MonHoc>> GetAllMonHoc()
        {
            return await appDbContext.MonHoc.ToListAsync();
        }
        public async Task<MonHoc> GetMonHocByMaMonHoc(string maMonhoc)
        {
            return await appDbContext.MonHoc.FirstOrDefaultAsync(n => n.MaMonHoc == maMonhoc);
        }

        public Task<IEnumerable<MonHoc>> GetMonHocByTenMonHoc(string tenMonHoc)
        {
            throw new System.NotImplementedException();
        }

        public async Task<MonHoc> CreateMonHoc(MonHoc monHoc)
        {
            var res = await appDbContext.MonHoc.AddAsync(monHoc);
            await appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<MonHoc> UpdateMonHoc(MonHoc monHoc)
        {
            var res = await appDbContext.MonHoc.FirstOrDefaultAsync(n => n.MaMonHoc == monHoc.MaMonHoc);
            if(res != null)
            {
                res.MaMonHoc = monHoc.MaMonHoc;
                res.TenMonHoc = monHoc.TenMonHoc;
                res.SoTinChi = monHoc.SoTinChi;
                res.SoTietLT = monHoc.SoTietLT;
                res.SoTietTH = monHoc.SoTietTH;

                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }

        public async Task<MonHoc> DeleteMonHoc(string maMonHoc)
        {
            var res = await appDbContext.MonHoc.FirstOrDefaultAsync(n => n.MaMonHoc == maMonHoc);
            if(res != null)
            {
                appDbContext.MonHoc.Remove(res);
                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }

    }
}
