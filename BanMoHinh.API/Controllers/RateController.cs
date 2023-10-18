using BanMoHinh.API.IServices;
using BanMoHinh.API.Services;
using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/rate")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private IRateService _rateService;
            public IOrderService _iorderService;
        public RateController(IRateService rateService, IOrderService orderService)
        {
            _rateService = rateService;
            _iorderService = orderService;
        }
        [Route("getall")]

        [HttpGet]
        public async Task<IActionResult> GetAllRate()
        {
            try
            {
                return Ok(await _rateService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không tìm được dữ liệu");
            }

        }
        [Route("details/{id}")]

        [HttpGet]
        public async Task<ActionResult<Rate>> Get(Guid id)
        {
            try
            {
                return Ok(await _rateService.GetItem(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }

        [Route("add")]

        [HttpPost]
        public async Task<ActionResult<Rate>> CreateRate([FromBody] Rate rate)
        {
            var result = await _rateService.Create(rate);
            if (result)
            {
                return Ok("Đã thêm thành công");
            }
            return Ok("Lỗi!");
        }
        [Route("update/{id}")]

        [HttpPut]
        public async Task<ActionResult<Rate>> Put(Guid id, Rate rate, Guid orderid)
        {
            var result = await _rateService.Update(id, orderid, rate);
            if (result)
            {
                return Ok("Đã sửa thành công");
            }
            return Ok("Lỗi!");
        }
        [Route("delete/{id}")]

        [HttpDelete]
        public async Task<ActionResult<Rate>> Delete(Guid id, Guid orderid)
        {
            var rate = await _rateService.GetItem(id);
            if (rate != null)
            {
                orderid = rate.OrderItemId;

            }
            var result = await _rateService.Delete(id, orderid);
            if (result)
            {
                return Ok("Đã xoá thành công");
            }
            return Ok("Lỗi!");
        }
    }
}
