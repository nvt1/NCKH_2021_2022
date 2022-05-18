using ExcelDataReader;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhanCongGiangDay.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Web.Admin.Controllers
{
    public class HomeController : Controller
    {
        List<NhomLop> listNhomLopX = new List<NhomLop>();
        List<Khoa> listKhoa = new List<Khoa>();
        List<MonHoc> listMonHoc = new List<MonHoc>();
        List<HocKy> listHocKy = new List<HocKy>();
        int x = 0;
        public async Task<IEnumerable<NhomLop>> TaoDanhSachNhomLop(DataTable dataTable)
        {


            List<NhomLop> listNhomLop = new List<NhomLop>();
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


           
 
            foreach (DataRow row in dataTable.Rows)
            {
                NhomLop nl = new NhomLop();

                nl.MaNhomLop = row["MaNhomLop"].ToString();
                nl.KhoaId = listKhoa.FirstOrDefault(n => n.MaKhoa == row["MaKhoa"].ToString()).KhoaId;
                nl.MonHocId = listMonHoc.FirstOrDefault(n => n.MaMonHoc == row["MaMonHoc"].ToString()).MonHocId;
                //nl.HocKyId = listHocKy.FirstOrDefault(n => n.NamHoc == int.Parse(row["NamHoc"].ToString())).HocKyId ;
                nl.HocKyId = listHocKy.FirstOrDefault(n => n.HocKyThu == row["HocKyThu"].ToString()).HocKyId;
                /*int HocKyThu = int.Parse((row["HocKyThu"]).ToString());
               (n.HocKyThu == row["HocKyThu"].ToString())*/
                nl.HocKyId = 1;
                listNhomLop.Add(nl);
                x++;

            }
           
           
            HttpResponseMessage response = await MvcApplication.client.PostAsJsonAsync("api/NhomLop/TaoDanhSachNhomLop", listNhomLop);

            return listNhomLop;
        }
        public ActionResult Index()
        {

            DataTable dt = new DataTable();

            //if ((String)Session["tmpdata"] != null)
            //{
            try
            {
                dt = (DataTable)Session["tmpdata"];
            }
            catch (Exception ex)
            {

            }

            //}


            return View(dt);
        }

        /* private IAuthenticationManager AuthenticationManager  //Sign out method
         {
             get { return HttpContext.GetOwinContext().Authentication; }
         }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {
                    // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                    // to get started. This is how we avoid dependencies on ACE or Interop:
                    Stream stream = upload.InputStream;
                    ViewBag.Stream = stream.ToString();

                    // We return the interface, so that
                    IExcelDataReader reader = null;


                    if (upload.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                    int fieldcount = reader.FieldCount;
                    int rowcount = reader.RowCount;


                    DataTable dt = new DataTable();
                    //dt.Columns.Add("UserName");
                    //dt.Columns.Add("Adddress");
                    DataRow row;


                    DataTable dt_ = new DataTable();
                    try
                    {

                        dt_ = reader.AsDataSet().Tables[0];

                        string ret = "";



                        for (int i = 0; i < dt_.Columns.Count; i++)
                        {
                            dt.Columns.Add(dt_.Rows[0][i].ToString());
                            string tmp = dt_.Rows[0][1].ToString();
                            ViewBag.Debug = tmp;
                            Console.WriteLine(tmp);
                        }

                        int rowcounter = 0;
                        for (int row_ = 1; row_ < dt_.Rows.Count; row_++)
                        {
                            row = dt.NewRow();

                            for (int col = 0; col < dt_.Columns.Count; col++)
                            {
                                row[col] = dt_.Rows[row_][col].ToString();
                                rowcounter++;
                            }
                            dt.Rows.Add(row);
                        }

                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("File", "Unable to Upload file!");
                        return View();
                    }

                    DataSet result = new DataSet();//reader.AsDataSet();
                    result.Tables.Add(dt);
                    string minutes_ID = "";



                    reader.Close();
                    reader.Dispose();
                    // return View();
                    // return View(result.Tables[0]);

                    DataTable ddd = result.Tables[0];

                    listNhomLopX =  (await TaoDanhSachNhomLop(ddd)).ToList();
                    ViewBag.XXX = x;
                    Session["tmpdata"] = ddd;

                    // return View(ddd);
                    return RedirectToAction("Index", "NhomLop");

                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }





    }
}