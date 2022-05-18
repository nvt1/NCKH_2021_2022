using Microsoft.EntityFrameworkCore;
using PhanCongGiangDay.Models;
namespace PhanCongGiangDay.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<GiangVien> GiangVien { get; set; }
        public DbSet<HocKy> Hocky { get; set; }
        public DbSet<MonHoc> MonHoc { get; set; }
        public DbSet<NhomLop> NhomLop { get; set; }
        public DbSet<Khoa> Khoa { get; set; }
        public DbSet<PhanCong> PhanCong { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GiangVien>().HasData(
                new GiangVien
                {
                    GiangVienId = 1,
                    MaGiangVien = "CNTT01",
                    HoTen = "Nguyễn Văn A",
                    GioiTinh = "Nam",
                    NgaySinh = "02/07/1990",
                    SoDienThoai = "0987654321",
                    Email = "cntt01@gmail.com",
                    DiaChi = "Bình Dương",
                    MatKhau = "123",
                    Quyen = 1
                });
            
            modelBuilder.Entity<GiangVien>().HasData(
                new GiangVien
                {
                    GiangVienId = 2,
                    MaGiangVien = "CNTT02",
                    HoTen = "Nguyễn Văn B",
                    GioiTinh = "Nam",
                    NgaySinh = "12/02/1991",
                    SoDienThoai = "0987654321",
                    Email = "cntt02@gmail.com",
                    DiaChi = "Bình Dương",
                    MatKhau = "123",
                    Quyen = 1
                });
            
            modelBuilder.Entity<GiangVien>().HasData(
                new GiangVien
                {
                    GiangVienId = 3,
                    MaGiangVien = "CNTT03",
                    HoTen = "Nguyễn Thị C",
                    GioiTinh = "Nữ",
                    NgaySinh = "10/05/1990",
                    SoDienThoai = "0987654321",
                    Email = "cntt03@gmail.com",
                    DiaChi = "Bình Dương",
                    MatKhau = "123",
                    Quyen = 1
                });
            
            modelBuilder.Entity<HocKy>().HasData(
                new HocKy
                {
                    HocKyId = 1,
                    NamHoc = 2022,
                    HocKyThu = "1"
                });
            
            modelBuilder.Entity<HocKy>().HasData(
                new HocKy
                {
                    HocKyId = 2,
                    NamHoc = 2022,
                    HocKyThu = "2"
                });
            modelBuilder.Entity<HocKy>().HasData(
                 new HocKy
                 {
                     HocKyId = 3,
                     NamHoc = 2022,
                     HocKyThu = "3"
                 });

            modelBuilder.Entity<MonHoc>().HasData(
                new MonHoc
                {
                    MonHocId = 1,
                    MaMonHoc = "LING123",
                    TenMonHoc = "Cơ sở lập trình",
                    SoTinChi = 3,
                    SoTietLT = 30,
                    SoTietTH = 15
                });
            
            modelBuilder.Entity<MonHoc>().HasData(
                new MonHoc
                {
                    MonHocId = 2,
                    MaMonHoc = "LING124",
                    TenMonHoc = "Kỹ thuật lập trình",
                    SoTinChi = 4,
                    SoTietLT = 30,
                    SoTietTH = 30
                });
            
            modelBuilder.Entity<MonHoc>().HasData(
                new MonHoc
                {
                    MonHocId = 3,
                    MaMonHoc = "LING125",
                    TenMonHoc = "Lập trình web",
                    SoTinChi = 4,
                    SoTietLT = 30,
                    SoTietTH = 30
                });
            
            modelBuilder.Entity<Khoa>().HasData(
                new Khoa
                {
                    KhoaId = 1,
                    MaKhoa = "CNTT",
                    TenKhoa = "Công nghệ thông tin"
                });
            
            modelBuilder.Entity<NhomLop>().HasData(
                new NhomLop
                {
                    NhomLopId = 1,
                    MaNhomLop = "D19PM01",
                    MonHocId = 1,
                    HocKyId = 1,
                    KhoaId = 1
                });
            
            modelBuilder.Entity<NhomLop>().HasData(
                new NhomLop
                {
                    NhomLopId = 2,
                    MaNhomLop = "D19PM02",
                    MonHocId = 2,
                    HocKyId = 1,
                    KhoaId = 1
                });

            modelBuilder.Entity<NhomLop>().HasData(
            new NhomLop
            {
                NhomLopId = 3,
                MaNhomLop = "D19PM02",
                MonHocId = 2,
                HocKyId = 1,
                KhoaId = 1
            });

            modelBuilder.Entity<NhomLop>().HasData(
            new NhomLop
            {
                NhomLopId = 4,
                MaNhomLop = "D19PM02",
                MonHocId = 2,
                HocKyId = 1,
                KhoaId = 1
            });

            modelBuilder.Entity<PhanCong>().HasData(
                new PhanCong
                {
                    PhanCongId = 1,
                    GiangVienId = 1,
                    NhomLopId = 1
                });
           
            modelBuilder.Entity<PhanCong>().HasData(
                new PhanCong
                {
                    PhanCongId = 2,
                    GiangVienId = 2,
                    NhomLopId = 2
                });
   
            modelBuilder.Entity<PhanCong>().HasData(
                new PhanCong
                {
                    PhanCongId = 3,
                    GiangVienId = 2,
                    NhomLopId = 3
                });            
        }
    }
}
