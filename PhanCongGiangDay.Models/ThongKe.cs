using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Models
{
    public class ThongKe
    {
        public string MaGiangVien { get; set; }
        public string HoTen { get; set; }
        public int TongNhomLop { get; set; } = 0;
        public int TongTinChi { get; set; } = 0;
        public string NamHoc { get; set; }
        public string Ky { get; set; }
        public int TongTietLT { get; set; } = 0;
        public int TongTietTH { get; set; } = 0;
        public int TongSoTiet { get; set; } = 0;
        public string TenLop { get; set; }
        public string TenMonHoc { get; set; }
    }
}
