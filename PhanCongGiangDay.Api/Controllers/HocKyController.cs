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
    public class HocKyController : ControllerBase
    {
        private readonly IHocKyRepository hocKyRepository;

        public HocKyController(IHocKyRepository hocKyRepository)
        {
            this.hocKyRepository = hocKyRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HocKy>>> GetAllHocKy()
        {
            try
            {
                return Ok(await hocKyRepository.GetAllHocKy());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GiangVien>> GetHocKyById(int id)
        {
            try
            {
                HocKy res = await hocKyRepository.GetHocKyById(id);
                if (res == null)
                {
                    return NotFound("Khong tim thay Học Kỳ");
                }
                return Ok(res);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpPost]
        public async Task<ActionResult<HocKy>> CreateHocKy(HocKy hocKy)
        {
            try
            {
                if (hocKy == null)
                {
                    return BadRequest("Hoc ky object is null");
                }
                HocKy res = await hocKyRepository.GetHocKyById(hocKy.HocKyId);
                if (res != null)
                {
                    return BadRequest("Hoc Ky ID already existed");
                }
                HocKy hk = await hocKyRepository.CreateHocKy(hocKy);
                return CreatedAtAction(nameof(GetHocKyById), new { id = hocKy.HocKyId }, hocKy);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<HocKy>> UpdateHocKy(int id, HocKy hocKy)
        {
            try
            {
                if (!id.Equals(hocKy.HocKyId))
                {
                    return BadRequest("Hoc Ky ID mismatch");
                }
                HocKy hocKyToUpdate = await hocKyRepository.GetHocKyById(id);
                if (hocKyToUpdate == null)
                {
                    return NotFound("Khong tim thay id hoc ky = " + id);
                }
                return await hocKyRepository.UpdateHocKy(hocKy);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<HocKy>> DeleteHocKy(int id)
        {
            try
            {
                HocKy hk = await hocKyRepository.GetHocKyById(id);
                if (hk == null)
                {
                    return NotFound("Khong tim thay Hoc Ky can xoa");
                }
                return await hocKyRepository.DeleteHocKy(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
    }
}
