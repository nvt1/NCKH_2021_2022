using System;
using System.Collections.Generic;
using System.Text;

namespace PhanCongGiangDay.Models
{
    public class MonHoc
    {
        public int MonHocId { get; set; }
        public string MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public int SoTinChi { get; set; }
        public int SoTietLT { get; set; }
        public int SoTietTH { get; set; }
    }
}
