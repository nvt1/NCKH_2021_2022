using ClosedXML.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhanCongGiangDay.Api.Models;
using PhanCongGiangDay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaController : ControllerBase
    {
        private readonly IKhoaRepository khoaRepository;
        List<Khoa> khoas = new List<Khoa>();

        public KhoaController(IKhoaRepository khoaRepository)
        {
            this.khoaRepository = khoaRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Khoa>>> GetAllKhoa()
        {
            try
            {
                return Ok(await khoaRepository.GetAllKhoa());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpGet("{maKhoa}")]
        public async Task<ActionResult<Khoa>> GetKhoaByMaKhoa(string maKhoa)
        {
            try
            {
                Khoa res = await khoaRepository.GetKhoaByMaKhoa(maKhoa);
                if (res == null)
                {
                    return NotFound("Khong tim thay Khoa");
                }
                return Ok(res);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Khoa>> CreateKhoa (Khoa khoa)
        {
            try
            {
                if (khoa == null)
                {
                    return BadRequest("Khoa object is null");
                }
                Khoa res = await khoaRepository.GetKhoaByMaKhoa(khoa.MaKhoa);
                if (res != null)
                {
                    return BadRequest("Khoa ID already existed");
                }
                Khoa kh = await khoaRepository.CreateKhoa(khoa);
                return CreatedAtAction(nameof(GetKhoaByMaKhoa), new { maKhoa = khoa.MaKhoa }, khoa);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpPut("{maKhoa}")]
        public async Task<ActionResult<Khoa>> UpdateKhoa(string maKhoa, Khoa khoa)
        {
            try
            {
                if (!maKhoa.Equals(khoa.MaKhoa))
                {
                    return BadRequest("Khoa ID mismatch");
                }
                Khoa  khoaToUpdate = await khoaRepository.GetKhoaByMaKhoa(maKhoa);
                if (khoaToUpdate == null)
                {
                    return NotFound("Khong tim thay ma khoa = " + maKhoa);
                }
                return await khoaRepository.UpdateKhoa(khoa);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpDelete("{maKhoa}")]
        public async Task<ActionResult<Khoa>> DeleteKhoa(string maKhoa)
        {
            try
            {
                Khoa kh = await khoaRepository.GetKhoaByMaKhoa(maKhoa);
                if (kh == null)
                {
                    return NotFound("Khong tim thay Khoa can xoa");
                }
                return await khoaRepository.DeleteKhoa(maKhoa);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }


      
    }
}
