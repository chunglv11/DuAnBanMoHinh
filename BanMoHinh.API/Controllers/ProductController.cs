using BanMoHinh.API.IServices;
using BanMoHinh.API.Services;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _iproductService;

        public ProductController(IProductService iproductService)
        {
            _iproductService = iproductService;
        }
        [HttpGet("get-all-product")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _iproductService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("get-all-productvm")]
        public async Task<IActionResult> GetAllVm()
        {
            try
            {
                return Ok(await _iproductService.GetAllVM());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("get-{id}")]
        public async Task<ActionResult<Product>> Get(Guid id)
        {
            try
            {
                return Ok(await _iproductService.GetItem(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("UpdateSLTheoSPCT")]
        public async Task<ActionResult<bool>> UpdateSLTheoSPCT()
        {
            try
            {
                return Ok(await _iproductService.UpdateSLTheoSPCT());
    }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
}
        }

        [HttpPost("create-product")]
        public async Task<ActionResult<ProductVM>> Post([FromBody] ProductVM obj)
        {
            var result = await _iproductService.Create(obj);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpPost("create-many-product")]
        public async Task<ActionResult<ProductVM>> PostMany([FromBody] List<ProductVM> obj)
        {
            var result = await _iproductService.CreateMany(obj);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpPut("update-product-{id}")]
        public async Task<ActionResult<ProductVM>> Put(Guid id, [FromBody] ProductVM obj)
        {
            var result = await _iproductService.Update(id, obj);
            if (result)
            {
                return Ok("Đã sửa thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpDelete("delete-product-{id}")]
        public async Task<ActionResult<ProductVM>> Delete(Guid id)
        {
            var result = await _iproductService.Delete(id);
            if (result)
            {
                return Ok("Đã xoá thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpGet("ChangeStatus")]
        public async Task<IActionResult> ChangeStatusAsync(Guid idsp, bool status)
        {
            var productdetail = await _iproductService.ChangeStatusAsync(idsp, status);
            var updateslProduct = await _iproductService.UpdateSLTheoSPCT();
            return Ok(productdetail);
        }
    }
}
