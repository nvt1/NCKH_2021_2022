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
    public class MonHocController : ControllerBase
    {
        private IMonHocRepository monHocRepository;

        public MonHocController(IMonHocRepository monHocRepository)
        {
            this.monHocRepository = monHocRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonHoc>>> GetAllMonHoc()
        {
            try
            {
                return Ok(await monHocRepository.GetAllMonHoc());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpGet("{maMonHoc}")]
        public async Task<ActionResult<IEnumerable<MonHoc>>> GetMonHocByMaMonHoc(string maMonHoc)
        {
            try
            {
                var res = await monHocRepository.GetMonHocByMaMonHoc(maMonHoc);
                if(res == null)
                {
                    return NotFound("Khong tim thay mon hoc");
                }
                return Ok(res);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpPost]
        public async Task<ActionResult<MonHoc>> CreateMonHoc(MonHoc monHoc)
        {
            try
            {
                if(monHoc == null)
                {
                    return BadRequest("MonHoc object is null");
                }
                MonHoc res = await monHocRepository.GetMonHocByMaMonHoc(monHoc.MaMonHoc);
                if(res != null)
                {
                    return BadRequest("Mã môn học đã tồn tại trong Data");
                }
                MonHoc mh = await monHocRepository.CreateMonHoc(monHoc);
                return CreatedAtAction(nameof(GetMonHocByMaMonHoc), new { maMonHoc = mh.MaMonHoc }, mh);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpPut("{maMonHoc}")]
        public async Task<ActionResult<MonHoc>> UpdateMonHoc(string maMonHoc, MonHoc monHoc)
        {
            try
            {
                if (!maMonHoc.Equals(monHoc.MaMonHoc))
                {
                    return BadRequest("Mon Hoc ID mismatch");
                }
                MonHoc MonHocToUpdate = await monHocRepository.GetMonHocByMaMonHoc(maMonHoc);
                if (MonHocToUpdate == null)
                {
                    return NotFound("Khong tim thay ma mon hoc " + maMonHoc);
                }
                return await monHocRepository.UpdateMonHoc(monHoc);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpDelete("{maMonHoc}")]
        public async Task<ActionResult<MonHoc>> DeleteMonHoc(string maMonHoc)
        {
            try
            {
                MonHoc mh = await monHocRepository.GetMonHocByMaMonHoc(maMonHoc);
                if(mh == null)
                {
                    return BadRequest("Không tìm thấy môn học cần xóa");
                }
                return await monHocRepository.DeleteMonHoc(maMonHoc);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
    }
}
