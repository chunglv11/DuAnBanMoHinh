using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.Client.Areas.User.Controllers
{
    [Area("User")]
    public class ProductController : Controller
    {
        public IActionResult ListProduct()
        {
            return View();
        }
        public IActionResult ProductDetail()
        {
            return View();
        }

    }
}
