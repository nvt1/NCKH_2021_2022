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

namespace PhanCongGiangDay.Web.Admin.Controllers
{
    public class GiangVienController : Controller
    {
         List<GiangVien> listGiangVien = new List<GiangVien>();
        // GET: GiangVien
        public async Task<ActionResult> Index(string search)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            HttpResponseMessage response = await MvcApplication.client.GetAsync("api/GiangVien");

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                listGiangVien = JsonConvert.DeserializeObject<List<GiangVien>>(res);
                var movies = from m in listGiangVien
                             select m;

                if (!String.IsNullOrEmpty(search))
                {
                    movies = listGiangVien.Where(s => s.MaGiangVien.Contains(search) || s.HoTen.Contains(search));
                }

                return View(movies.ToList());

            }
            return View(listGiangVien);
        }
        
        [HttpGet]
        public async Task<ActionResult> CreateGiangVien()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateGiangVien(FormCollection f)
        {
            GiangVien gv = new GiangVien();
            gv.HoTen = f["HoTen"];
            gv.MaGiangVien = f["MaGiangVien"];
            gv.GioiTinh = f["GioiTinh"];
            gv.NgaySinh = f["NgaySinh"];
            gv.SoDienThoai = f["SoDienThoai"];
            gv.DiaChi = f["DiaChi"];
            gv.Email = f["Email"];
            gv.MatKhau = f["MatKhau"];
            gv.Quyen = int.Parse(f["Quyen"]);

            HttpResponseMessage response = await MvcApplication.client.PostAsJsonAsync("api/GiangVien", gv);

            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(GetGiangVienByMaGiangVien), "GiangVien",new { maGV = gv.MaGiangVien });
        }

        public async Task<ActionResult> GetGiangVienByMaGiangVien(string maGV)
        {
            HttpResponseMessage response = await MvcApplication.client.GetAsync($"api/GiangVien/{maGV}");
            GiangVien gv = new GiangVien();

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                gv = JsonConvert.DeserializeObject<GiangVien>(res);

            }
            return View(gv);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateGiangVien(string maGV)
        {
            HttpResponseMessage response = await MvcApplication.client.GetAsync($"api/GiangVien/{maGV}");
            GiangVien gv = new GiangVien();
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                gv = JsonConvert.DeserializeObject<GiangVien>(res);

            }
            return View(gv);
            
        }
        [HttpPost] 
        public async Task<ActionResult> UpdateGiangVien(FormCollection f)
        {
            GiangVien gv = new GiangVien();
            
            gv.HoTen = f["HoTen"];
            gv.MaGiangVien = f["MaGiangVien"];
            gv.GioiTinh = f["GioiTinh"];
            gv.NgaySinh = f["NgaySinh"];
            gv.SoDienThoai = f["SoDienThoai"];
            gv.DiaChi = f["DiaChi"];
            gv.Email = f["Email"];
            gv.MatKhau = f["MatKhau"];
            gv.Quyen = int.Parse(f["Quyen"]);

            HttpResponseMessage response = await MvcApplication.client.PutAsJsonAsync($"api/GiangVien/{gv.MaGiangVien}", gv);

            //response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(GetGiangVienByMaGiangVien), "GiangVien", new { maGV = gv.MaGiangVien });
        }
        public async Task<ActionResult> DeleteGiangVien(string maGV)
        {
            HttpResponseMessage response = await MvcApplication.client.DeleteAsync($"api/GiangVien/{maGV}");
            response.EnsureSuccessStatusCode();
            return Redirect("Index");
        }
        public async Task<ActionResult> ExportDatabase()
        {
            HttpResponseMessage respone = await MvcApplication.client.GetAsync("api/GiangVien");
            if (respone.IsSuccessStatusCode)
            {
                var GiangVienRespone = respone.Content.ReadAsStringAsync().Result;
                listGiangVien = JsonConvert.DeserializeObject<List<GiangVien>>(GiangVienRespone);

            }
            var data = listGiangVien.ToList();

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=GiangVienTable.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return Redirect("Index");
        }
    }
    
}