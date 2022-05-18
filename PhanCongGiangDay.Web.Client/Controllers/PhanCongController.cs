using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhanCongGiangDay.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.IO;

namespace PhanCongGiangDay.Web.Client.Controllers
{
    public class PhanCongController : Controller
    {
        static List<PhanCong> listPhanCong = new List<PhanCong>();
        static List<NhomLop> listNhomLop = new List<NhomLop>();
        static List<GiangVien> listGiangVien = new List<GiangVien>();
        
        // GET: PhanCong
        public async Task<ActionResult> Index()
        {
            if (Session["GiangVien"] == null)
            {
                return RedirectToAction("Login", "Client");

            }
            HttpResponseMessage res = await MvcApplication.client.GetAsync("api/PhanCong");
            if (res.IsSuccessStatusCode)
            {
                var PhanCongRespone = res.Content.ReadAsStringAsync().Result;
                listPhanCong = JsonConvert.DeserializeObject<List<PhanCong>>(PhanCongRespone);
            }
            GiangVien gv = Session["GiangVien"] as GiangVien;

            return View(listPhanCong.Where(x => x.GiangVien.MaGiangVien == gv.MaGiangVien));

        }
        [HttpPost]
        public async Task<ActionResult> Index(string HocKyThu = "0", string NamHoc = "0")
        {
            GiangVien gv = Session["GiangVien"] as GiangVien;
            HttpResponseMessage res = await MvcApplication.client.GetAsync("api/PhanCong");

            var result = res.Content.ReadAsStringAsync().Result;
            listPhanCong = JsonConvert.DeserializeObject<List<PhanCong>>(result);
            var tmp = listPhanCong;
            listPhanCong = tmp.Where(n => n.GiangVien.MaGiangVien == gv.MaGiangVien).ToList();
            if (!HocKyThu.Equals("0"))
            {
                listPhanCong = listPhanCong.Select(n => n).Where(n => n.NhomLop.HocKy.HocKyThu == HocKyThu).ToList();
            }
            if (!NamHoc.Equals("0"))
            {
                listPhanCong = listPhanCong.Select(n => n).Where(n => n.NhomLop.HocKy.NamHoc.ToString() == NamHoc).ToList();
            }

            return View(listPhanCong);
        }
        public async Task<ActionResult> ExportDatabase()
        {
            GiangVien gv = Session["GiangVien"] as GiangVien;
            HttpResponseMessage respone = await MvcApplication.client.GetAsync("api/PhanCong");
            if (respone.IsSuccessStatusCode)
            {
                var PhanCongRespone = respone.Content.ReadAsStringAsync().Result;
                listPhanCong = JsonConvert.DeserializeObject<List<PhanCong>>(PhanCongRespone);

            }
            

            var data = from s in listPhanCong where(s.GiangVien.MaGiangVien == gv.MaGiangVien)
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
                Response.AddHeader("content-disposition", "attachment;  filename=PhanCongGiangDay.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return Redirect("Index");
        }
    }
}