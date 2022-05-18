using Newtonsoft.Json;
using OfficeOpenXml;
using PhanCongGiangDay.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PhanCongGiangDay.Web.Admin.Controllers
{
    public class NhomLopController : Controller
    {
        List<NhomLop> listNhomLop = new List<NhomLop>();
        List<Khoa> listKhoa = new List<Khoa>();
        List<MonHoc> listMonHoc = new List<MonHoc>();
         List<HocKy> listHocKy = new List<HocKy>();
        // GET: NhomLop
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            HttpResponseMessage res = await MvcApplication.client.GetAsync("api/NhomLop");

            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                listNhomLop = JsonConvert.DeserializeObject<List<NhomLop>>(result);
                
                res = await MvcApplication.client.GetAsync("api/HocKy");
                if (res.IsSuccessStatusCode)
                {
                    var HocKyRepository = res.Content.ReadAsStringAsync().Result;
                    listHocKy = JsonConvert.DeserializeObject<List<HocKy>>(HocKyRepository);
                    ViewBag.ListHocKy = listHocKy;
                }
              
                res = await MvcApplication.client.GetAsync("api/NhomLop");
                if (res.IsSuccessStatusCode)
                {
                    var HocKyRepository = res.Content.ReadAsStringAsync().Result;
                    listNhomLop = JsonConvert.DeserializeObject<List<NhomLop>>(HocKyRepository);
                    ViewBag.ListNhomLop = listNhomLop;
                }
            }
            return View(listNhomLop);
        }
        [HttpPost]
        public async Task<ActionResult> Index(string HocKyThu="0",string NamHoc="0")
        {
            HttpResponseMessage res = await MvcApplication.client.GetAsync("api/NhomLop");

                var result = res.Content.ReadAsStringAsync().Result;
                listNhomLop = JsonConvert.DeserializeObject<List<NhomLop>>(result);

            if (!HocKyThu.Equals("0"))
            {
                listNhomLop= listNhomLop.Select(n => n).Where(n => n.HocKy.HocKyThu == HocKyThu).ToList();
            }
            if (!NamHoc.Equals("0"))
            {
                listNhomLop = listNhomLop.Select(n => n).Where(n => n.HocKy.NamHoc.ToString() == NamHoc).ToList();
            }


            return View(listNhomLop);
        }
        public async Task<ActionResult> GetNhomLopByIdNhomLop(int nhomLopId)
        {
            HttpResponseMessage response = await MvcApplication.client.GetAsync($"api/NhomLop/{nhomLopId}");
            NhomLop nl = new NhomLop();

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                nl = JsonConvert.DeserializeObject<NhomLop>(res);

            }
            return View(nl);
        }
        [HttpGet]
        public async Task<ActionResult> CreateNhomLop()
        {
            
            NhomLop nl = new NhomLop();
           
            HttpResponseMessage res = await MvcApplication.client.GetAsync("api/Khoa");
            if (res.IsSuccessStatusCode)
            {
                var KhoaRepository = res.Content.ReadAsStringAsync().Result;
                listKhoa = JsonConvert.DeserializeObject<List<Khoa>>(KhoaRepository);
                ViewBag.ListKhoa = listKhoa;
            }
            res = await MvcApplication.client.GetAsync("api/MonHoc");
            if (res.IsSuccessStatusCode)
            {
                var MonHocRepository = res.Content.ReadAsStringAsync().Result;
                listMonHoc = JsonConvert.DeserializeObject<List<MonHoc>>(MonHocRepository);
                ViewBag.ListMonHoc = listMonHoc;
            }
            res = await MvcApplication.client.GetAsync("api/HocKy");
            if (res.IsSuccessStatusCode)
            {
                var HocKyRepository = res.Content.ReadAsStringAsync().Result;
                listHocKy = JsonConvert.DeserializeObject<List<HocKy>>(HocKyRepository);
                ViewBag.ListHocKy = listHocKy;
            }


            return View(nl) ;
        }

        [HttpPost]
        public async Task<ActionResult> CreateNhomLop(FormCollection f)
        {
            NhomLop nl = new NhomLop();
            
            nl.MaNhomLop = f["MaNhomLop"];
            nl.HocKyId = int.Parse(f["HocKyId"]);
            nl.KhoaId = int.Parse(f["KhoaId"]);
            nl.MonHocId = int.Parse(f["MonHocId"]);

            HttpResponseMessage response = await MvcApplication.client.PostAsJsonAsync("api/NhomLop", nl);
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> UpdateNhomLop(int nhomLopId)
        {
            HttpResponseMessage response = await MvcApplication.client.GetAsync($"api/NhomLop/{nhomLopId}");
            NhomLop nl = new NhomLop();
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                nl = JsonConvert.DeserializeObject<NhomLop>(res);

            }
            response = await MvcApplication.client.GetAsync("api/Khoa");
            if (response.IsSuccessStatusCode)
            {
                var KhoaRepository = response.Content.ReadAsStringAsync().Result;
                listKhoa = JsonConvert.DeserializeObject<List<Khoa>>(KhoaRepository);
                ViewBag.ListKhoa = listKhoa.ToList().OrderBy(n => n.KhoaId) ;
            }
            response = await MvcApplication.client.GetAsync("api/MonHoc");
            if (response.IsSuccessStatusCode)
            {
                var MonHocRepository = response.Content.ReadAsStringAsync().Result;
                listMonHoc = JsonConvert.DeserializeObject<List<MonHoc>>(MonHocRepository);
                ViewBag.ListMonHoc = listMonHoc.ToList().OrderBy(n => n.MonHocId);
            }
            response = await MvcApplication.client.GetAsync("api/HocKy");
            if (response.IsSuccessStatusCode)
            {
                var HocKyRepository = response.Content.ReadAsStringAsync().Result;
                listHocKy = JsonConvert.DeserializeObject<List<HocKy>>(HocKyRepository);
                ViewBag.ListHocKy = listHocKy.ToList().OrderBy(n => n.HocKyId);
            }
            return View(nl);

        }
        [HttpPost]
        public async Task<ActionResult> UpdateNhomLop(FormCollection f)
        {
            NhomLop nl = new NhomLop();
            nl.NhomLopId = int.Parse(f["NhomLopId"]);
            nl.MaNhomLop = f["MaNhomLop"];
            nl.KhoaId = int.Parse(f["KhoaId"]);
            nl.HocKyId = int.Parse(f["HocKyId"]);
            nl.MonHocId = int.Parse(f["MonHocId"]);

            HttpResponseMessage response = await MvcApplication.client.PutAsJsonAsync($"api/NhomLop/{nl.NhomLopId}", nl);

            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(GetNhomLopByIdNhomLop), "NhomLop", new { nhomLopId = nl.NhomLopId });
        }
        public async Task<ActionResult> DeleteNhomLop(int nhomLopId)
        {
            HttpResponseMessage response = await MvcApplication.client.DeleteAsync($"api/NhomLop/{nhomLopId}");
            response.EnsureSuccessStatusCode();
            return Redirect("Index");
        }
        public async Task<ActionResult> ExportDatabase()
        {
            HttpResponseMessage respone = await MvcApplication.client.GetAsync("api/NhomLop");
            if (respone.IsSuccessStatusCode)
            {
                var NhomLopRespone = respone.Content.ReadAsStringAsync().Result;
                listNhomLop = JsonConvert.DeserializeObject<List<NhomLop>>(NhomLopRespone);

            }

            var data = from s in listNhomLop select new
            {               
                MaNhomLop = s.MaNhomLop,
                MaKhoa = s.Khoa.MaKhoa,
                TenKhoa = s.Khoa.TenKhoa,
                NamHoc = s.HocKy.NamHoc,
                HocKyThu = s.HocKy.HocKyThu,
                MaMonHoc = s.MonHoc.MaMonHoc,
                TenMonHoc = s.MonHoc.TenMonHoc,
                SoTinChi = s.MonHoc.SoTinChi,
                SoTietLT = s.MonHoc.SoTietLT,
                SoTietTH = s.MonHoc.SoTietTH
            };
           

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            

            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=NhomLop.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return Redirect("Index");
        }
    }
}