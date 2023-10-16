using BanMoHinh.API.IServices;
using BanMoHinh.API.Services;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/ranks")]
    [ApiController]
    public class RanksController : ControllerBase
    {
        private readonly IRankService _rankService;

        public RanksController(IRankService rankService)
        {
            _rankService = rankService;
        }
        [HttpGet("get-ranks")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _rankService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            try
            {
                return Ok(await _rankService.GetItem(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpPost("create-rank")]
        public async Task<ActionResult<Rank>> Post([FromBody] Rank rank)
        {
            var result = await _rankService.Create(rank);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }

        [HttpPut("update-rank/{id}")]
        public async Task<ActionResult<Post>> Put(Guid id, Rank rank)
        {
            var result = await _rankService.Update(id, rank);
            if (result)
            {
                return Ok("Đã sửa thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpDelete("delete-rank/{id}")]
        public async Task<ActionResult<ProductVM>> Delete(Guid id)
        {
            var result = await _rankService.Delete(id);
            if (result)
            {
                return Ok("Đã xoá thành công");
            }
            return Ok("Lỗi!");
        }
    }
}
