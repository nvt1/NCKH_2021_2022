using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhanCongGiangDay.Api.Models;
using PhanCongGiangDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhanCongGiangDay.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhanCongController : ControllerBase
    {
        private readonly IPhanCongRepository phanCongRepository;

        public PhanCongController(IPhanCongRepository phanCongRepository)
        {
            this.phanCongRepository = phanCongRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhanCong>>> GetAllPhanCong()
        {
            try
            {
                return Ok(await phanCongRepository.GetAllPhanCong());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpGet("{phanCongId:int}")]
        public async Task<ActionResult<PhanCong>> GetPhanCongById(int phanCongId)
        {
            try
            {
                PhanCong res = await phanCongRepository.GetPhanCongById(phanCongId);
                if (res == null)
                {
                    return NotFound("Khong tim thay Phân công");
                }
                return Ok(res);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpPost]
        public async Task<ActionResult<PhanCong>> AddPhanCong(PhanCong phanCong)
        {
            try
            {
                if (phanCong == null)
                {
                    return BadRequest();
                }
                PhanCong res = await phanCongRepository.AddPhanCong(phanCong);
                return CreatedAtAction(nameof(GetPhanCongById), new { id = res.PhanCongId }, res);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");

            }
        }
        [HttpDelete("{phanCongId:int}")]
        public async Task<ActionResult<PhanCong>> DeletePhanCong(int phanCongId)
        {
            try
            {
                PhanCong pc = await phanCongRepository.GetPhanCongById(phanCongId);
                if (pc == null)
                {
                    return NotFound("Khong tim thay Khoa can xoa");
                }
                return await phanCongRepository.DeletePhanCong(phanCongId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
    }
}
