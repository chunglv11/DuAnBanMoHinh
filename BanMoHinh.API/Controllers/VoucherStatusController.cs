using BanMoHinh.API.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherStatusController : ControllerBase
    {
        private readonly IVoucherStatusService _statusService;

        public VoucherStatusController(IVoucherStatusService statusService)
        {
            _statusService = statusService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _statusService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
    }
}
