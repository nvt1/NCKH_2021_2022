using System;
using System.Collections.Generic;
using System.Text;

namespace PhanCongGiangDay.Models
{
    public class GiangVien
    {
        public int GiangVienId { get; set; }
        public string MaGiangVien { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }    
        public string NgaySinh { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }
        public string DiaChi { get; set; }
        public string MatKhau { get; set; }
        public int Quyen { get; set; }

    }
}
