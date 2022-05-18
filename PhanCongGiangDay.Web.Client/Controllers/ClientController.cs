using Newtonsoft.Json;
using PhanCongGiangDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PhanCongGiangDay.Web.Client.Controllers
{
    public class ClientController : Controller
    {
        List<GiangVien> listGiangVien = new List<GiangVien>();
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(FormCollection collection)
        {
            var sMaGiangVien = collection["MaGiangVien"];
            var sMatKhau = collection["MatKhau"];
            ViewBag.MaGiangVien = "";
            if (String.IsNullOrEmpty(sMaGiangVien))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
                return View();
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
                return View();
            }
            else
            {
                HttpResponseMessage response = await MvcApplication.client.GetAsync("api/GiangVien");

                if (response.IsSuccessStatusCode)
                {
                    var res = response.Content.ReadAsStringAsync().Result;
                    listGiangVien = JsonConvert.DeserializeObject<List<GiangVien>>(res);

                }
                var gv = listGiangVien.SingleOrDefault(n => n.MaGiangVien == sMaGiangVien && n.MatKhau == sMatKhau);
                if (gv != null)
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công  ";
                    Session["GiangVien"] = gv;
                    return RedirectToAction("Index", "PhanCong");

                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không chính xác";
                    return View();
                }
            }

        }
        public ActionResult DangXuat()
        {
            Session["GiangVien"] = null;
            return RedirectToAction("Login", "Client");
        }
    }
}