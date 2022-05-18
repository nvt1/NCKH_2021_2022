using Newtonsoft.Json;
using OfficeOpenXml;
using PhanCongGiangDay.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PhanCongGiangDay.Web.Admin.Controllers
{
    public class PhanCongController : Controller
    {
        static List<PhanCong> listPhanCong = new List<PhanCong>();
        static List<NhomLop> listNhomLop = new List<NhomLop>();
        static List<GiangVien> listGiangVien = new List<GiangVien>();
        static List<ThongKe> listThongKe = new List<ThongKe>();
        // GET: PhanCongGiangDays
        public async Task<ActionResult> Index(string search)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            HttpResponseMessage res = await MvcApplication.client.GetAsync("api/PhanCong");
            if (res.IsSuccessStatusCode)
            {
                var PhanCongRespone = res.Content.ReadAsStringAsync().Result;
                listPhanCong = JsonConvert.DeserializeObject<List<PhanCong>>(PhanCongRespone);
                var movies = from m in listPhanCong
                             select m;
                if (!string.IsNullOrEmpty(search))
                {
                    movies = listPhanCong.Where(s => s.GiangVien.MaGiangVien.Contains(search) || s.GiangVien.HoTen.Contains(search)
                                                || s.NhomLop.MaNhomLop.Contains(search) || s.NhomLop.MonHoc.MaMonHoc.Contains(search)
                                                || s.NhomLop.MonHoc.TenMonHoc.Contains(search));

                    search = search.ToUpper();
                    return View(movies.ToList());
                }
            }
            return View(listPhanCong);
        }
        public async Task<ActionResult> GetPhanCongByPhanCongId(int phanCongId)
        {
            HttpResponseMessage res = await MvcApplication.client.GetAsync($"api/PhanCong/{phanCongId}");
            PhanCong pc = new PhanCong();
            
            if (res.IsSuccessStatusCode)
            {
                var PhanCongRepository = res.Content.ReadAsStringAsync().Result;
                pc = JsonConvert.DeserializeObject<PhanCong>(PhanCongRepository);

            }
            return View(pc);
        }
        public async Task<ActionResult> AddPhanCong(FormCollection f)
        {
            PhanCong pc = new PhanCong();
            pc.NhomLopId = int.Parse(f["NhomLopId"]);
            pc.GiangVienId = int.Parse(f[$"MaGiangVien{pc.NhomLopId}"]);
            HttpResponseMessage res = await MvcApplication.client.PostAsJsonAsync("api/PhanCong", pc);
            return RedirectToAction(nameof(ChuaPhanCong));
        }
        public async Task<ActionResult> ChuaPhanCong()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            HttpResponseMessage res = await MvcApplication.client.GetAsync("api/NhomLop/ChuaPhanCong/Ok");
            if (res.IsSuccessStatusCode)
            {
                var PhanCongRespone = res.Content.ReadAsStringAsync().Result;
                listNhomLop = JsonConvert.DeserializeObject<List<NhomLop>>(PhanCongRespone);
            }

            res = await MvcApplication.client.GetAsync("api/GiangVien");
            if (res.IsSuccessStatusCode)
            {
                var GiangVienResponse = res.Content.ReadAsStringAsync().Result;
                listGiangVien = JsonConvert.DeserializeObject<List<GiangVien>>(GiangVienResponse);
                ViewBag.ListGiangVien = listGiangVien;
            }
            return View(listNhomLop);
        }
        public async Task<ActionResult> DeletePhanCong(int phanCongId)
        {
            HttpResponseMessage response = await MvcApplication.client.DeleteAsync($"api/PhanCong/{phanCongId}");
            response.EnsureSuccessStatusCode();
            return Redirect("Index");
        }
        public async Task<ActionResult> ExportDatabase()
        {
            HttpResponseMessage respone = await MvcApplication.client.GetAsync("api/PhanCong");
            if (respone.IsSuccessStatusCode)
            {
                var PhanCongRespone = respone.Content.ReadAsStringAsync().Result;
                listPhanCong = JsonConvert.DeserializeObject<List<PhanCong>>(PhanCongRespone);

            }

            var data = from s in listPhanCong
                       select new
                       {
                           PhanCongId = s.PhanCongId,
                           MaGiangVien = s.GiangVien.MaGiangVien,
                           HoTen = s.GiangVien.HoTen,
                           MaNhomLop = s.NhomLop.MaNhomLop,
                           MaMonHoc = s.NhomLop.MonHoc.MaMonHoc,
                           TenMonHoc = s.NhomLop.MonHoc.TenMonHoc,

                       };


            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);


            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=PhanCong.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return Redirect("Index");
        }
        
        
        public async Task<ActionResult> ThongKe(string search, string HocKyThu = "0", string NamHoc = "0")
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            if (string.IsNullOrEmpty(search))
            {
                listThongKe.Clear();
                HttpResponseMessage res = await MvcApplication.client.GetAsync("api/PhanCong");
                if (res.IsSuccessStatusCode)
                {
                    var PhanCongRespone = res.Content.ReadAsStringAsync().Result;
                    listPhanCong = JsonConvert.DeserializeObject<List<PhanCong>>(PhanCongRespone);
                }

                List<string> listMaGiangVien = listPhanCong.Select(n => n.GiangVien.MaGiangVien).Distinct().ToList();
                List<int> listNamHoc = listPhanCong.Select(n => n.NhomLop.HocKy.NamHoc).Distinct().ToList();
                List<string> listHocKy = listPhanCong.Select(n => n.NhomLop.HocKy.HocKyThu).Distinct().ToList();
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
                                listThongKe.Add(tk);
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