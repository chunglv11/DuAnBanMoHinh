using BanMoHinh.API.IServices;
using BanMoHinh.API.Services;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/productDetail")]
    [ApiController]
    public class ProductDetailController : ControllerBase
    {
        private IProductDetailService _iproductDetailService;

        public ProductDetailController(IProductDetailService iproductDetailService)
        {
            _iproductDetailService = iproductDetailService;
        }
        [HttpGet("get-all-productdetail")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _iproductDetailService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("get-{id}")]
        public async Task<ActionResult<ProductDetail>> Get(Guid id)
        {
            try
            {
                return Ok(await _iproductDetailService.GetItem(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpPost("create-productdetail")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductDetailVM>> Post([FromForm] ProductDetailVM obj)
        {
            var result = await _iproductDetailService.Create(obj);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpPost("create-many-productdetail")]
        public async Task<ActionResult<ProductDetailVM>> PostMany([FromBody] List<ProductDetailVM> obj)
        {
            var result = await _iproductDetailService.CreateMany(obj);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpPut("update-productdetail-{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ProductDetailVM>> Put(Guid id, [FromForm] ProductDetailVM obj)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            obj.Id = id;
            var result = await _iproductDetailService.Update(obj);
            if (result)
            {
                return Ok("Đã sửa thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpDelete("delete-productdetail-{id}")]
        public async Task<ActionResult<ProductDetailVM>> Delete(Guid id)
        {
            var result = await _iproductDetailService.Delete(id);
            if (result)
            {
                return Ok("Đã xoá thành công");
            }
            return Ok("Lỗi!");
        }
    }
}
