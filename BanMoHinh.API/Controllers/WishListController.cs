using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private IWishListService _wishListService;

        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }
        [HttpGet("get-posts")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _wishListService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("get-{id}")]
        public async Task<ActionResult<WishList>> Get(Guid id)
        {
            try
            {
                return Ok(await _wishListService.GetItem(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpPost("create-wishlist")]
        public async Task<ActionResult<WishList>> Post(Guid UserId, Guid ProductDetailId)
        {   
            var result = await _wishListService.Create(UserId,ProductDetailId);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpDelete("delete-{id}")]
        public async Task<ActionResult<ProductVM>> Delete(Guid id)
        {
            var result = await _wishListService.Delete(id);
            if (result)
            {
                return Ok("Đã xoá thành công");
            }
            return Ok("Lỗi!");
        }
    }
}
