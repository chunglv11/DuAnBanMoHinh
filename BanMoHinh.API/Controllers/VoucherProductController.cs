using BanMoHinh.API.IServices;
using BanMoHinh.API.Services;
using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/voucherproduct")]
    [ApiController]
    public class VoucherProductController : ControllerBase
    {
        private IVoucherProductService _voucherProductService;
        public VoucherProductController(IVoucherProductService voucherProductService)
        {
            _voucherProductService = voucherProductService;
        }

        [HttpGet("get-voucherproduct")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _voucherProductService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("get-{id}")]
        public async Task<ActionResult<VoucherProduct>> Get(Guid id)
        {
            try
            {
                return Ok(await _voucherProductService.GetItem(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpPost("create-voucherproduct")]
        public async Task<ActionResult<VoucherProduct>> Post([FromBody] VoucherProduct userVoucher)
        {
            var result = await _voucherProductService.Create(userVoucher);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpPut("update-voucherproduct-{id}")]
        public async Task<ActionResult<VoucherProduct>> Put(Guid id, VoucherProduct userVoucher)
        {
            var result = await _voucherProductService.Update(id, userVoucher);
            if (result)
            {
                return Ok("Đã sửa thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpDelete("delete-voucherproduct-{id}")]
        public async Task<ActionResult<VoucherProduct>> Delete(Guid id)
        {
            var result = await _voucherProductService.Delete(id);
            if (result)
            {
                return Ok("Đã xoá thành công");
            }
            return Ok("Lỗi!");
        }
    }
}
