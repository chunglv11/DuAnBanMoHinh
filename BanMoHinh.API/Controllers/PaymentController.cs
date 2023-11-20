using BanMoHinh.API.IServices;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public IPaymentService _IpaymentService;
        public PaymentController(IPaymentService payment)
        {
            _IpaymentService = payment;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _IpaymentService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("get-{id}")]
        public async Task<ActionResult<OrderItem>> Get(Guid id)
        {
            try
            {
                return Ok(await _IpaymentService.GetItem(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpPost("create")]
        public async Task<ActionResult<Payment>> Post([FromBody] Payment payment)
        { 
            var result = await _IpaymentService.Create(payment);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }
        
        [HttpPut("update-product-{id}")]
        public async Task<ActionResult<Payment>> Put(Guid id, [FromBody] Payment obj)
        {
            var result = await _IpaymentService.Update(id, obj);
            if (result)
            {
                return Ok("Đã sửa thành công");
            }
            return Ok("Lỗi!");
        }
        [HttpDelete("delete-product-{id}")]
        public async Task<ActionResult<OrderItemVM>> Delete(Guid id)
        {
            var result = await _IpaymentService.Delete(id);
            if (result)
            {
                return Ok("Đã xoá thành công");
            }
            return Ok("Lỗi!");
        }
    }
}
