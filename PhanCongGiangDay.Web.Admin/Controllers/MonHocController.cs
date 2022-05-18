using PhanCongGiangDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using System.IO;

namespace PhanCongGiangDay.Web.Admin.Controllers
{
    public class MonHocController : Controller
    {
        List<MonHoc> listMonHoc = new List<MonHoc>();
        // GET: MonHoc
        public async Task<ActionResult> Index(string search)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            HttpResponseMessage response = await MvcApplication.client.GetAsync("api/MonHoc");
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                listMonHoc = JsonConvert.DeserializeObject<List<MonHoc>>(res);
                var movies = from m in listMonHoc
                             select m;
                if (!String.IsNullOrEmpty(search))
                {
                    movies = listMonHoc.Where(s => s.MaMonHoc.Contains(search) || s.TenMonHoc.Contains(search));
                }
                return View(movies.ToList());
            }
            return View(listMonHoc);
        }
        [HttpGet]
        public async Task<ActionResult> CreateMonHoc()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateMonHoc(FormCollection f)
        {
            MonHoc mh = new MonHoc();
            mh.MaMonHoc = f["MaMonHoc"];
            mh.TenMonHoc = f["TenMonHoc"];
            mh.SoTinChi = int.Parse(f["SoTinChi"]);
            mh.SoTietLT = int.Parse(f["SoTietLT"]);
            mh.SoTietTH = int.Parse(f["SoTietTH"]);

            HttpResponseMessage response = await MvcApplication.client.PostAsJsonAsync("api/MonHoc", mh);

            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(GetMonHocByMaMonHoc),"MonHoc", new { maMonHoc = mh.MaMonHoc});
        }
        public async Task<ActionResult> GetMonHocByMaMonHoc(string maMonHoc)
        {
            HttpResponseMessage response = await MvcApplication.client.GetAsync($"api/MonHoc/{maMonHoc}");
            MonHoc mh = new MonHoc();
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                mh = JsonConvert.DeserializeObject<MonHoc>(res);
            }
            return View(mh);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateMonHoc(string maMonHoc)
        {
            HttpResponseMessage response = await MvcApplication.client.GetAsync($"api/MonHoc/{maMonHoc}");
            MonHoc mh = new MonHoc();
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                mh = JsonConvert.DeserializeObject<MonHoc>(res);

            }
            return View(mh);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateMonHoc(FormCollection f)
        {
            MonHoc mh = new MonHoc();
            mh.MaMonHoc = f["MaMonHoc"];
            mh.TenMonHoc = f["TenMonHoc"];
            mh.SoTinChi = int.Parse(f["SoTinChi"]);
            mh.SoTietLT = int.Parse(f["SoTietLT"]);
            mh.SoTietTH = int.Parse(f["SoTietTH"]);

            HttpResponseMessage response = await MvcApplication.client.PutAsJsonAsync($"api/MonHoc/{mh.MaMonHoc}", mh);

            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(GetMonHocByMaMonHoc), "MonHoc", new { maMonHoc = mh.MaMonHoc});
        }
        public async Task<ActionResult> DeleteMonHoc(string maMonHoc)
        {
            HttpResponseMessage response = await MvcApplication.client.DeleteAsync($"api/MonHoc/{maMonHoc}");
            response.EnsureSuccessStatusCode();
            return Redirect("Index");
        }
        public async Task<ActionResult> ExportDatabase()
        {
            HttpResponseMessage respone = await MvcApplication.client.GetAsync("api/MonHoc");
            if (respone.IsSuccessStatusCode)
            {
                var MonHocRespone = respone.Content.ReadAsStringAsync().Result;
                listMonHoc = JsonConvert.DeserializeObject<List<MonHoc>>(MonHocRespone);

            }
            var data = listMonHoc.ToList();

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=MonHoc.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return Redirect("Index");
        }
    }
}