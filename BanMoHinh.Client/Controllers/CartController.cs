using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.Client.Controllers
{
    public class CartController : Controller
    {
        public async Task<IActionResult> Cart()
        {
            return View();
        }
    }
}
