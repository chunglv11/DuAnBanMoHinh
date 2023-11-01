using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.Client.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Introduct()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

    }
}
