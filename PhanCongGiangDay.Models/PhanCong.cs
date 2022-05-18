using System;
using System.Collections.Generic;
using System.Text;

namespace PhanCongGiangDay.Models
{
    public class PhanCong
    {
        public int PhanCongId { get; set; }
        public int GiangVienId { get; set; } 
        public int NhomLopId { get; set; }
        public GiangVien GiangVien { get; set; }
        public NhomLop NhomLop { get; set; }
    }
}
