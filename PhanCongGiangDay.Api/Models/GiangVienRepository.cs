using Microsoft.EntityFrameworkCore;
using PhanCongGiangDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Models
{
    public class GiangVienRepository : IGiangVienRepository
    {
        private readonly AppDbContext appDbContext;
 
        public GiangVienRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<IEnumerable<GiangVien>> GetAllGiangVien()
        {
            return await appDbContext.GiangVien.ToListAsync();
        }


        public async Task<GiangVien> GetGiangVienByMaGiangVien(string maGiangVien)
        {
            return await appDbContext.GiangVien.FirstOrDefaultAsync(gv => gv.MaGiangVien == maGiangVien);
        }

        public async Task<GiangVien> GetGiangVienByEmailGiangVien(string emailGiangVien)
        {
            return await appDbContext.GiangVien.FirstOrDefaultAsync(gv => gv.Email == emailGiangVien);
        }
        public async Task<GiangVien> CreateGiangVien(GiangVien giangVien)
        {
            var res = await appDbContext.GiangVien.AddAsync(giangVien);
            await appDbContext.SaveChangesAsync();
            return res.Entity;
        }

        public async Task<GiangVien> UpdateGiangVien(GiangVien giangVien)
        {
           
            var res = await appDbContext.GiangVien.FirstOrDefaultAsync(e => e.MaGiangVien == giangVien.MaGiangVien);
            if(res != null)
            {
                res.HoTen = giangVien.HoTen;
                res.GioiTinh = giangVien.GioiTinh;
                res.NgaySinh = giangVien.NgaySinh;
                res.SoDienThoai = giangVien.SoDienThoai;
                res.DiaChi = giangVien.DiaChi;
                res.MatKhau = giangVien.MatKhau;
                res.Quyen = giangVien.Quyen;

                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }

        public async Task<GiangVien> DeleteGiangVien(string maGiangVien)
        {
            var res = await appDbContext.GiangVien.FirstOrDefaultAsync(x => x.MaGiangVien == maGiangVien);
            if (res != null)
            {
                appDbContext.GiangVien.Remove(res);
                await appDbContext.SaveChangesAsync();
                return res;
            }
            return null;
        }
    }
}
