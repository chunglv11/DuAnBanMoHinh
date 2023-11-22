using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BanMoHinh.Client.Controllers
{
	public class OrderController : Controller
	{
		private readonly HttpClient _httpClient;

		public OrderController(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}
		public async Task<IActionResult> allOrder()
		{
			var identity = HttpContext.User.Identity as ClaimsIdentity;
			if (identity != null)
			{
				var userIdClaim = identity.FindFirst(ClaimTypes.Name);
				if (userIdClaim != null)
				{
					var userName = userIdClaim.Value;
					var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
					var listOrder = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
					var listorderbyUser = listOrder.Where(c => c.UserId == getUserbyName.Id);
					return View(listorderbyUser);
				}
				else
				{
					// ban chua dang nhap cho em no ra cho dang nhao anh oi
					return RedirectToAction("Login", "Authentication");
				}
			}
			else
			{
				// ban chua dang nhap cho em no ra cho dang nhao anh oi
				return RedirectToAction("Login", "Authentication");
			}

		}

        public async Task<IActionResult> Xacnhan()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.Name);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
                    var listOrder = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
                    var listorderbyUser = listOrder.Where(c => c.UserId == getUserbyName.Id&&c.OrderStatusId == Guid.Parse("426c1b3e-2e75-453a-a3c0-5523810252f4"));
                    return View(listorderbyUser);
                }
                else
                {
                    // ban chua dang nhap cho em no ra cho dang nhao anh oi
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                // ban chua dang nhap cho em no ra cho dang nhao anh oi
                return RedirectToAction("Login", "Authentication");
            }

        }
        public async Task<IActionResult> Danggiao()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.Name);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
                    var listOrder = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
                    var listorderbyUser = listOrder.Where(c => c.UserId == getUserbyName.Id && c.OrderStatusId == Guid.Parse("a3bed856-a9ca-426f-aa43-050ffdb8a117"));
                    return View(listorderbyUser);
                }
                else
                {
                    // ban chua dang nhap cho em no ra cho dang nhao anh oi
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                // ban chua dang nhap cho em no ra cho dang nhao anh oi
                return RedirectToAction("Login", "Authentication");
            }

        }
        public async Task<IActionResult> Cholayhang()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.Name);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
                    var listOrder = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
                    var listorderbyUser = listOrder.Where(c => c.UserId == getUserbyName.Id && c.OrderStatusId == Guid.Parse("1fb39266-3480-4fef-abba-06500d76ba04"));
                    return View(listorderbyUser);
                }
                else
                {
                    // ban chua dang nhap cho em no ra cho dang nhao anh oi
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                // ban chua dang nhap cho em no ra cho dang nhao anh oi
                return RedirectToAction("Login", "Authentication");
            }

        }
        public async Task<IActionResult> Hoanthanh()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.Name);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
                    var listOrder = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
                    var listorderbyUser = listOrder.Where(c => c.UserId == getUserbyName.Id && c.OrderStatusId == Guid.Parse("59eb300b-dc36-4166-9d84-595ecf039915"));
                    return View(listorderbyUser);
                }
                else
                {
                    // ban chua dang nhap cho em no ra cho dang nhao anh oi
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                // ban chua dang nhap cho em no ra cho dang nhao anh oi
                return RedirectToAction("Login", "Authentication");
            }

        }
        public async Task<IActionResult> Yeucautrahang()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.Name);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
                    var listOrder = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
                    var listorderbyUser = listOrder.Where(c => c.UserId == getUserbyName.Id && c.OrderStatusId == Guid.Parse("550139c8-f41b-4eb1-a1a2-8cdcce010851"));
                    return View(listorderbyUser);
                }
                else
                {
                    // ban chua dang nhap cho em no ra cho dang nhao anh oi
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                // ban chua dang nhap cho em no ra cho dang nhao anh oi
                return RedirectToAction("Login", "Authentication");
            }

        }
        public async Task<IActionResult> Xacnhantrahang()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.Name);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
                    var listOrder = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
                    var listorderbyUser = listOrder.Where(c => c.UserId == getUserbyName.Id && c.OrderStatusId == Guid.Parse("4b3cfbdd-4c68-463d-88aa-5bf0cdaf4bf4"));
                    return View(listorderbyUser);
                }
                else
                {
                    // ban chua dang nhap cho em no ra cho dang nhao anh oi
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                // ban chua dang nhap cho em no ra cho dang nhao anh oi
                return RedirectToAction("Login", "Authentication");
            }

        }
        public async Task<IActionResult> Huydon()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var userIdClaim = identity.FindFirst(ClaimTypes.Name);
                if (userIdClaim != null)
                {
                    var userName = userIdClaim.Value;
                    var getUserbyName = await _httpClient.GetFromJsonAsync<User>($"https://localhost:7007/api/users/get/{userName}");
                    var listOrder = await _httpClient.GetFromJsonAsync<List<Order>>("https://localhost:7007/api/order/getall");
                    var listorderbyUser = listOrder.Where(c => c.UserId == getUserbyName.Id && c.OrderStatusId == Guid.Parse("957e682c-4a56-49d6-bd6a-c6a51afae370"));
                    return View(listorderbyUser);
                }
                else
                {
                    // ban chua dang nhap cho em no ra cho dang nhao anh oi
                    return RedirectToAction("Login", "Authentication");
                }
            }
            else
            {
                // ban chua dang nhap cho em no ra cho dang nhao anh oi
                return RedirectToAction("Login", "Authentication");
            }

        }


    }
}
