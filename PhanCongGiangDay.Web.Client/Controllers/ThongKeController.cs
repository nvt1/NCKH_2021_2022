using Newtonsoft.Json;
using OfficeOpenXml;
using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PhanCongGiangDay.Web.Client.Controllers
{
    public class ThongKeController : Controller
    {
        static List<PhanCong> listPhanCong = new List<PhanCong>();
        static List<NhomLop> listNhomLop = new List<NhomLop>();
        static List<GiangVien> listGiangVien = new List<GiangVien>();
        static List<ThongKe> listThongKe = new List<ThongKe>();
        // GET: ThongKe
        public async Task<ActionResult> ThongKe(string search, string HocKyThu = "0", string NamHoc = "0")
        {
            GiangVien gv = Session["GiangVien"] as GiangVien;
            if (string.IsNullOrEmpty(search))
            {
                listThongKe.Clear();
                HttpResponseMessage res = await MvcApplication.client.GetAsync("api/PhanCong");
                if (res.IsSuccessStatusCode)
                {
                    var PhanCongRespone = res.Content.ReadAsStringAsync().Result;
                    listPhanCong = JsonConvert.DeserializeObject<List<PhanCong>>(PhanCongRespone);
                    var tmp = listPhanCong;
                    listPhanCong = tmp.Where(n => n.GiangVien.MaGiangVien == gv.MaGiangVien).ToList();
                }

                List<string> listMaGiangVien = listPhanCong.Select(n => n.GiangVien.MaGiangVien).Distinct().ToList();
                List<int> listNamHoc = listPhanCong.Select(n => n.NhomLop.HocKy.NamHoc).Distinct().ToList();
                List<string> listHocKy = listPhanCong.Select(n => n.NhomLop.HocKy.HocKyThu).Distinct().ToList();
                int tongTietLT = 0;
                int tongTietTH = 0;
                int tongTinChi = 0;
                int tongSoTiet = 0;
                foreach (int namHoc in listNamHoc)
                {
                    foreach (string hocKy in listHocKy)
                    {
                        foreach (string maGiangVien in listMaGiangVien)
                        {
                            ThongKe tk = new ThongKe();
                           
                            tk.MaGiangVien = maGiangVien;
                            bool flag = false;
                            foreach (PhanCong phanCong in listPhanCong)
                            {
                                if (phanCong.GiangVien.MaGiangVien == maGiangVien && phanCong.NhomLop.HocKy.NamHoc == namHoc &&
                                     phanCong.NhomLop.HocKy.HocKyThu == hocKy)
                                {
                                    tk.TongTietLT += phanCong.NhomLop.MonHoc.SoTietLT;
                                    tk.TongTietTH += phanCong.NhomLop.MonHoc.SoTietTH;
                                    tk.TongSoTiet = tk.TongTietLT + tk.TongTietTH;
                                    tk.TongTinChi += phanCong.NhomLop.MonHoc.SoTinChi;
                                    tk.TongNhomLop++;
                                    tk.HoTen = phanCong.GiangVien.HoTen;
                                    tk.NamHoc = phanCong.NhomLop.HocKy.NamHoc.ToString();
                                    tk.Ky = phanCong.NhomLop.HocKy.HocKyThu;
                                    tk.TenLop += "\n\r\n" + phanCong.NhomLop.MaNhomLop;
                                    tk.TenMonHoc += "\n\r\n" + phanCong.NhomLop.MonHoc.TenMonHoc;
                                    flag = true;
                                }
                               
                            }
                            
                            if (flag)
                            {
                                listThongKe.Add(tk);
                                
                            }
                            tongTietLT += tk.TongTietLT;
                            tongTietTH += tk.TongTietTH;
                            tongSoTiet += tk.TongSoTiet;
                            tongTinChi += tk.TongTinChi;
                        }
                        
                     

                    }
                }
                if (!HocKyThu.Equals("0"))
                {
                    listThongKe = listThongKe.Select(n => n).Where(n => n.Ky == HocKyThu).ToList();
                }
                if (!NamHoc.Equals("0"))
                {
                    listThongKe = listThongKe.Select(n => n).Where(n => n.NamHoc == NamHoc).ToList();
                }
                ViewBag.TongLT = tongTietLT;
                ViewBag.TongTH = tongTietTH;
                ViewBag.TongTC = tongTinChi;
                ViewBag.TongT = tongSoTiet;
                return View(listThongKe);

            }
            else
            {
                search = search.ToUpper();
                List<ThongKe> listThai = listThongKe.Select(n => n).Where(n => n.MaGiangVien == search).ToList();
                if (!HocKyThu.Equals("0"))
                {
                    listThai = listThai.Select(n => n).Where(n => n.Ky == HocKyThu).ToList();
                }
                return View(listThai);
            }

        }
    }
}