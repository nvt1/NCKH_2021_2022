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
    public class NhomLopController : ControllerBase
    {
        private readonly INhomLopRepository nhomLopRepository;
        public NhomLopController(INhomLopRepository nhomLopRepository)
        {
            this.nhomLopRepository = nhomLopRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhomLop>>> GetAllNhomLop()
        {
            try
            {
                return Ok(await nhomLopRepository.GetAllNhomLop());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [Route("ChuaPhanCong/Ok")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NhomLop>>> GetNhomLopChuaPhanCong()
        {
            try
            {
                return Ok(await nhomLopRepository.GetNhomLopChuaPhanCong());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }

        [HttpGet("{nhomLopId}")]
        public async Task<ActionResult<NhomLop>> GetNhomLopByIdNhomLop(int nhomLopId)
        {
            try
            {
                NhomLop res = await nhomLopRepository.GetNhomLopByIdNhomLop(nhomLopId);
                if (res == null)
                {
                    return NotFound("Khong tim thay nhom lop");
                }
                return Ok(res);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }

        [HttpPost]
        public async Task<ActionResult<NhomLop>> CreateNhomLop(NhomLop nhomLop)
        {
            try
            {
                if (nhomLop == null)
                {
                    return BadRequest("Nhom lop object is null");
                }
                NhomLop res = await nhomLopRepository.GetNhomLopByIdNhomLop(nhomLop.NhomLopId);
                if (res != null)
                {
                    return BadRequest("Giang vien ID already existed");
                }
                NhomLop nl = await nhomLopRepository.CreateNhomLop(nhomLop);
                return CreatedAtAction(nameof(GetNhomLopByIdNhomLop), new { nhomLopId = nl.NhomLopId }, nl);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }

        [Route("TaoDanhSachNhomLop")]
        [HttpPost]
        public async Task<ActionResult<NhomLop>> CreateListNhomLop(List<NhomLop> listNhomLop)
        {
            try
            {
                if (listNhomLop == null)
                {
                    return BadRequest("Nhom lop object is null");
                }
                
  
                IEnumerable<NhomLop> listCreatedNhomLop = await nhomLopRepository.CreateListNhomLop(listNhomLop);
                

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
            return RedirectToAction(nameof(GetAllNhomLop));
        }
        [HttpPut("{nhomLopId}")]
        public async Task<ActionResult<NhomLop>> UpdateNhomLop(int nhomLopId, NhomLop nhomLop)
        {
            try
            {
                if (!nhomLopId.Equals(nhomLop.NhomLopId))
                {
                    return BadRequest("Nhom lop ID mismatch");
                }
                NhomLop nhomLopToUpdate = await nhomLopRepository.GetNhomLopByIdNhomLop(nhomLopId);
                if (nhomLopRepository == null)
                {
                    return NotFound("Khong tim thay id nhom lop = " + nhomLopId);
                }
                return await nhomLopRepository.UpdateNhomLop(nhomLop);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
        [HttpDelete("{nhomLopId}")]
        public async Task<ActionResult<NhomLop>> DeleteNhomLop(int nhomLopId)
        {
            try
            {
                NhomLop nl = await nhomLopRepository.GetNhomLopByIdNhomLop(nhomLopId);
                if (nl == null)
                {
                    return NotFound("Khong tim thay Nhom lop can xoa");
                }
                return await nhomLopRepository.DeleteNhomLop(nhomLopId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }
        }
    }
}
