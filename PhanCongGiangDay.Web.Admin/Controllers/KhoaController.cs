using Newtonsoft.Json;
using OfficeOpenXml;
using PhanCongGiangDay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PhanCongGiangDay.Web.Admin.Controllers
{
    public class KhoaController : Controller
    {
        List<Khoa> listKhoa = new List<Khoa>();
        // GET: Khoa
        public async Task<ActionResult> Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            HttpResponseMessage response = await MvcApplication.client.GetAsync("api/Khoa");

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                listKhoa = JsonConvert.DeserializeObject<List<Khoa>>(res);

            }
            return View(listKhoa);
        }
        [HttpGet]
        public async Task<ActionResult> CreateKhoa()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateKhoa(FormCollection f)
        {
            Khoa kh = new Khoa();
            kh.MaKhoa = f["MaKhoa"];
            kh.TenKhoa = f["TenKhoa"];
            
            HttpResponseMessage response = await MvcApplication.client.PostAsJsonAsync("api/Khoa", kh);

            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(GetKhoaByMaKhoa), "Khoa", new { maKhoa = kh.MaKhoa });
        }
        public async Task<ActionResult> GetKhoaByMaKhoa(string maKhoa)
        {
            HttpResponseMessage response = await MvcApplication.client.GetAsync($"api/Khoa/{maKhoa}");
            Khoa kh = new Khoa();

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                kh = JsonConvert.DeserializeObject<Khoa>(res);

            }
            return View(kh);
        }
        [HttpGet]
        public async Task<ActionResult> UpdateKhoa(string maKhoa)
        {
            HttpResponseMessage response = await MvcApplication.client.GetAsync($"api/Khoa/{maKhoa}");
            Khoa kh = new Khoa();
            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                kh = JsonConvert.DeserializeObject<Khoa>(res);

            }
            return View(kh);
        }
        [HttpPost]
        public async Task<ActionResult> UpdateKhoa(FormCollection f)
        {
            Khoa kh = new Khoa();
            kh.MaKhoa = f["MaKhoa"];
            kh.TenKhoa = f["TenKhoa"];
                                        

            HttpResponseMessage response = await MvcApplication.client.PutAsJsonAsync($"api/Khoa/{kh.MaKhoa}", kh);

            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(GetKhoaByMaKhoa), new { maKhoa = kh.MaKhoa });
        }
        public async Task<ActionResult> DeleteKhoa(string maKhoa)
        {
            HttpResponseMessage response = await MvcApplication.client.DeleteAsync($"api/Khoa/{maKhoa}");
            response.EnsureSuccessStatusCode();
            return Redirect("Index");
        }
        /* public void WriteTsv<T>(IEnumerable<T> data, TextWriter output)
         {
             PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
             foreach (PropertyDescriptor prop in props)
             {
                 output.Write(prop.DisplayName); // header
                 output.Write("\t");
             }
             output.WriteLine();
             foreach (T item in data)
             {
                 foreach (PropertyDescriptor prop in props)
                 {
                     output.Write(prop.Converter.ConvertToString(
                          prop.GetValue(item)));
                     output.Write("\t");
                 }
                 output.WriteLine();
             }
         }
         public async Task<ActionResult> ExportListFromTsv()
         {
             HttpResponseMessage respone = await MvcApplication.client.GetAsync("api/Khoa");
             if (respone.IsSuccessStatusCode)
             {
                 var KhoaRespone = respone.Content.ReadAsStringAsync().Result;
                 listKhoa = JsonConvert.DeserializeObject<List<Khoa>>(KhoaRespone);

             }
             var data = listKhoa;

             Response.ClearContent();
             Response.AddHeader("content-disposition", "attachment;filename=Contact.xls");
             Response.AddHeader("Content-Type", "application/vnd.ms-excel");
             WriteTsv(data, Response.Output);
             Response.End();
             return View();
         }*/

        public async Task<ActionResult> ExportDatabase()
        {
            HttpResponseMessage respone = await MvcApplication.client.GetAsync("api/Khoa");
            if (respone.IsSuccessStatusCode)
            {
                var KhoaRespone = respone.Content.ReadAsStringAsync().Result;
                listKhoa = JsonConvert.DeserializeObject<List<Khoa>>(KhoaRespone);

            }
            var data = listKhoa.ToList();

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells[1, 1].LoadFromCollection(data, true);
            using (var memoryStream = new MemoryStream())
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=KhoaTable.xlsx");
                excel.SaveAs(memoryStream);
                memoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
            return Redirect("Index");
        }

    }
}