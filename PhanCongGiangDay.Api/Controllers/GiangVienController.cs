using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhanCongGiangDay.Api.Models;
using PhanCongGiangDay.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiangVienController : ControllerBase
    {
        private IGiangVienRepository giangVienRepository;
        public GiangVienController(IGiangVienRepository giangVienRepository)
        {
            this.giangVienRepository = giangVienRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiangVien>>> GetAllGiangVien()
        {
            try
            {
                return Ok(await giangVienRepository.GetAllGiangVien());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpGet("{maGV}")]
        public async Task<ActionResult<GiangVien>> GetGiangVienByMaGiangVien(string maGV)
        {
            try
            {
                GiangVien res = await giangVienRepository.GetGiangVienByMaGiangVien(maGV);
                if (res == null)
                {
                    return NotFound("Khong tim thay GV");
                }
                return Ok(res);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpGet("email")]
        public async Task<ActionResult<GiangVien>> GetGiangVienByEmailGiangVien(string emailGiangVien)
        {
            try
            {
                GiangVien res = await giangVienRepository.GetGiangVienByEmailGiangVien(emailGiangVien);
                if (res == null)
                {
                    return NotFound("Khong tim thay GV");
                }
                return Ok(res);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpPost]
        public async Task<ActionResult<GiangVien>> CreateGiangVien(GiangVien giangVien)
        {
            try
            {
                if (giangVien == null)
                {
                    return BadRequest("GiangVien object is null");
                }
                GiangVien res = await giangVienRepository.GetGiangVienByMaGiangVien(giangVien.MaGiangVien);
                if(res != null)
                {
                    return BadRequest("Giang Vien ID already existed");
                }
                GiangVien resEmail = await giangVienRepository.GetGiangVienByEmailGiangVien(giangVien.Email);
                if (resEmail != null)
                {
                    return BadRequest("Giang Vien Email already existed");
                }

                GiangVien gv = await giangVienRepository.CreateGiangVien(giangVien);
                return CreatedAtAction(nameof(GetGiangVienByMaGiangVien), new { maGV = gv.MaGiangVien }, gv);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }

        }
        [HttpPut("{maGV}")]
        public async Task<ActionResult<GiangVien>> UpdateGiangVien(string maGV, GiangVien giangVien)
        {
            try
            {
                if(!maGV.Equals(giangVien.MaGiangVien))
                {
                    return BadRequest("Giang Vien ID mismatch");
                }
                GiangVien giangVienToUpdate = await giangVienRepository.GetGiangVienByMaGiangVien(maGV);
                if(giangVienToUpdate == null)
                {
                    return NotFound("Khong tim thay ma giang vien = " + maGV);
                }
                return await giangVienRepository.UpdateGiangVien(giangVien);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpDelete("{maGV}")]
        public async Task<ActionResult<GiangVien>> DeleteGiangVien(string maGV)
        {
            try
            {
                GiangVien gv = await giangVienRepository.GetGiangVienByMaGiangVien(maGV);
                if (gv == null)
                {
                    return NotFound("Khong tim thay GV can xoa");
                }
                return await giangVienRepository.DeleteGiangVien(maGV);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        
    }
}
