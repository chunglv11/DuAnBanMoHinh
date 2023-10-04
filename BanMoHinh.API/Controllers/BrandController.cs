using BanMoHinh.API.IServices;
using BanMoHinh.API.Services;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/brand")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly  IBrandService _ibrandService;
        public BrandController(IBrandService brandService)
        {
           _ibrandService = brandService;
        }
        [HttpGet("getall")]
        public async Task<IActionResult> GetAlll()
        {
           
            var pro = await _ibrandService.GetAll();
            return Ok(pro);
        }
        [HttpGet("get{id}")]
        public async Task<ActionResult<Brand>> Get(Guid id)
        {
            try
            {
                return Ok(await _ibrandService.GetItem(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpPost("create")]
        public async Task<ActionResult<BrandVM>> Post([FromBody] BrandVM brand)
        {
            var result = await _ibrandService.Create(brand);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }

        [HttpPut("update{id}")]
        public async Task<ActionResult<BrandVM>> Put(Guid id, BrandVM  brand)
        {
            var result = await _ibrandService.Update(id, brand);
            if (result)
            {
                return Ok("Đã sửa thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpDelete("delete{id}")]
        public async Task<ActionResult<BrandVM>> Delete(Guid id)
        {
            var result = await _ibrandService.Delete(id);
            if (result)
            {
                return Ok("Đã xoá thành công");
            }
            return Ok("Lỗi!");
        }
    }
}
