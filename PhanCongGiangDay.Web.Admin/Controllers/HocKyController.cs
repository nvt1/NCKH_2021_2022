using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhanCongGiangDay.Models;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace PhanCongGiangDay.Web.Admin.Controllers
{
    public class HocKyController : Controller
    {
        List<HocKy> listHocKy = new List<HocKy>();
        // GET: HocKy
        public async Task<ActionResult> Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");

            }
            HttpResponseMessage response = await MvcApplication.client.GetAsync("api/HocKy");

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                listHocKy = JsonConvert.DeserializeObject<List<HocKy>>(res);

            }
            return View(listHocKy);
        }
        [HttpGet]
        public async Task<ActionResult> CreateHocKy()
        {
            HttpResponseMessage res = await MvcApplication.client.GetAsync("api/HocKy");
            if (res.IsSuccessStatusCode)
            {
                var HocKyRepository = res.Content.ReadAsStringAsync().Result;
                listHocKy = JsonConvert.DeserializeObject<List<HocKy>>(HocKyRepository);
                ViewBag.ListKhoa = listHocKy.ToList().OrderBy(n => n.HocKyThu);
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateHocKy(FormCollection f)
        {
            
            HocKy hk = new HocKy();
            hk.NamHoc = int.Parse(f["NamHoc"]);
            
            // Ở ĐÂY NÈ, KHÓ QUÁ            

            HttpResponseMessage response = await MvcApplication.client.PostAsJsonAsync("api/HocKy", hk);

            response.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(GetHocKyById), "HocKy", new { id = hk.HocKyId });
        }
        public async Task<ActionResult> GetHocKyById(int id)
        {
            HttpResponseMessage response = await MvcApplication.client.GetAsync($"api/HocKy/{id}");
            HocKy hk = new HocKy();

            if (response.IsSuccessStatusCode)
            {
                var res = response.Content.ReadAsStringAsync().Result;
                hk = JsonConvert.DeserializeObject<HocKy>(res);

            }
            return View(hk);
        }

    }
}