using Microsoft.EntityFrameworkCore;
using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public class HocKyRepository : IHocKyRepository
    {
        private readonly AppDbContext appDbContext;

        public HocKyRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<HocKy>> GetAllHocKy()
        {
            return await appDbContext.Hocky.ToListAsync();
        }

        public async Task<HocKy> GetHocKyById(int id)
        {
            return await appDbContext.Hocky.FirstOrDefaultAsync(n => n.HocKyId == id);
        }

        public async Task<HocKy> CreateHocKy(HocKy hocKy)
        {
            var res = await appDbContext.Hocky.AddAsync(hocKy);
            await appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<HocKy> DeleteHocKy(int id)
        {
            var res = await appDbContext.Hocky.FirstOrDefaultAsync(x => x.HocKyId == id);
            if (res != null)
            {
                appDbContext.Hocky.Remove(res);
                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }

        public async Task<HocKy> UpdateHocKy(HocKy hocKy)
        {
            var res = await appDbContext.Hocky.FirstOrDefaultAsync(e => e.HocKyId == hocKy.HocKyId);
            if (res != null)
            {
                res.NamHoc = hocKy.NamHoc;
                res.HocKyThu = hocKy.HocKyThu;

                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }
    }
}
