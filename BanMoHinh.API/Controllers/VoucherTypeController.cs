using BanMoHinh.API.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherTypeController : ControllerBase
    {
        private readonly IVoucherTypeServices _typeService;

        public VoucherTypeController(IVoucherTypeServices typeService)
        {
            _typeService = typeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _typeService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
    }
}
