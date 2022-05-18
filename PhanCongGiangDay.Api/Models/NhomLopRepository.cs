using Microsoft.EntityFrameworkCore;
using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public class NhomLopRepository : INhomLopRepository
    {
        private readonly AppDbContext appDbContext;

        public NhomLopRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<NhomLop>> GetAllNhomLop()
        {
            return await appDbContext.NhomLop
                .Include(n => n.Khoa)
                .Include(n => n.MonHoc)
                .Include(n => n.HocKy) 
                .ToListAsync();
        }

        public async Task<NhomLop> GetNhomLopByIdNhomLop(int nhomLopId)
        {
            return await appDbContext.NhomLop
                .Include(n => n.Khoa)
                .Include(n => n.HocKy)
                .Include(n => n.MonHoc)
                .FirstOrDefaultAsync(n => n.NhomLopId == nhomLopId);
        }

        public async Task<NhomLop> CreateNhomLop(NhomLop nhomLop)
        {
            var res = await appDbContext.NhomLop.AddAsync(nhomLop);
            await appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<NhomLop> DeleteNhomLop(int nhomLopId)
        {
            var res = await appDbContext.NhomLop.FirstOrDefaultAsync(x => x.NhomLopId == nhomLopId);
            if (res != null)
            {
                appDbContext.NhomLop.Remove(res);
                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }


        public async Task<NhomLop> UpdateNhomLop(NhomLop nhomLop)
        {

            var res = await appDbContext.NhomLop.FirstOrDefaultAsync(e => e.NhomLopId == nhomLop.NhomLopId);
            if (res != null)
            {
                res.MaNhomLop = nhomLop.MaNhomLop;
                res.KhoaId = nhomLop.KhoaId;
                res.HocKyId = nhomLop.HocKyId;
                res.MonHocId = nhomLop.MonHocId;

                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }

        public async Task<IEnumerable<NhomLop>> GetNhomLopChuaPhanCong()
        {
            var query = (from nl in appDbContext.NhomLop select nl.NhomLopId)
                .Except(from pc in appDbContext.PhanCong select pc.NhomLopId);

            var res = (from nl in appDbContext.NhomLop
                       join id in query on nl.NhomLopId equals id
                       select nl).Include(n => n.MonHoc).Include(n => n.HocKy);
            return await  res.ToListAsync();
        }

        public async Task<IEnumerable<NhomLop>> CreateListNhomLop(List<NhomLop> listNhomLop)
        {
            List<NhomLop> listCreatedNhomLop = new List<NhomLop>();
            foreach(NhomLop nl in listNhomLop)
            {

                appDbContext.NhomLop.Add(nl);
            }
            await appDbContext.SaveChangesAsync();
            return listCreatedNhomLop.ToList() ;
        }
    }
}
