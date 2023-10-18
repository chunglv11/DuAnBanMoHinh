using BanMoHinh.API.IServices;
using BanMoHinh.API.Services;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public IOrderService _iorderService;
        public OrderController(IOrderService orderService) {
            _iorderService = orderService;
        }
        [Route("getall")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _iorderService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [Route("details/{id}")]
        [HttpGet]
        public async Task<ActionResult<Order>> Get(Guid id)
        {
            try
            {
                return Ok(await _iorderService.GetItem(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [Route("add")]
        [HttpPost]
        public async Task<ActionResult<OrderVM>> Post([FromBody] OrderVM obj)
        {
            var result = await _iorderService.Create(obj);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }
        [Route("update/{id}")]
        [HttpPut]
        public async Task<ActionResult<OrderVM>> Put(Guid id, Guid userid,[FromBody] OrderVM obj)
        {
            var result = await _iorderService.Update(id,userid, obj);
            if (result)
            {
                return Ok("Đã sửa thành công");
            }
            return Ok("Lỗi!");
        }
        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<ActionResult<OrderVM>> Delete(Guid id)
        {
            var result = await _iorderService.Delete(id);
            if (result)
            {
                return Ok("Đã xoá thành công");
            }
            return Ok("Lỗi!");
        }
    }
}
