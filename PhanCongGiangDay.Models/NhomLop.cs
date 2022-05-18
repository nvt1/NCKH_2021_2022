using System;
using System.Collections.Generic;
using System.Text;

namespace PhanCongGiangDay.Models
{
    public class NhomLop
    {
        public int NhomLopId { get; set; }
        public string MaNhomLop { get; set; }
        public int KhoaId { get; set; }
        public int HocKyId { get; set; }
        public int MonHocId { get; set; }
        public HocKy HocKy { get; set; }
        public Khoa Khoa { get; set; }
        public MonHoc MonHoc { get; set; }
    }
}
