using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.Client.Controllers
{
    public class OrderController : Controller
    {
        public OrderController() { 

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
