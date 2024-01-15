using BanMoHinh.Client.IServices;
using BanMoHinh.Client.Services;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using System.Security.Policy;
using System.Net.WebSockets;
using BanMoHinh.Share.Models;

namespace BanMoHinh.Client.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IServices.IAuthenticationService _authenticationService;
        private readonly HttpClient _httpClient;

        public AuthenticationController(IServices.IAuthenticationService authenticationService,HttpClient httpClient)
        {
            _authenticationService = authenticationService;
            _httpClient = httpClient;

        }
        public IActionResult Login()
        {
            var checkLogin = User.Identity.IsAuthenticated;
            if (checkLogin)
            {
                var user = User.Claims.FirstOrDefault(c=>c.Type == ClaimTypes.NameIdentifier).Value;
                var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                if (role== "Admin")
                {
                    return Redirect("Admin/Home/Index");
                }
                if (role == "User")
                {
                    return RedirectToAction("Index","Home");
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var url = "https://localhost:7007/api/authentication/login";
            var result = await _authenticationService.Login(model, url);
            if (result.IsSuccess)
            {
                var token = result.Token;
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name).Value));
                identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role).Value));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value));
                identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(principal);
                string role = jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role).Value;
                var ListCartItemFromSession = SessionServices.GetCartItemFromSession(HttpContext.Session, "Cart");
                if (ListCartItemFromSession.Count>0)
                {
                    var MyCart = await _httpClient.GetFromJsonAsync<Cart>($"https://localhost:7007/api/cart/get-item-Cart?userId={jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value}");
                    var ItemInMyCart = await _httpClient.GetFromJsonAsync<List<CartItem>>($"https://localhost:7007/api/cartitem/getcartitembycartid?cartid={MyCart.Id}"); // lấy item in cart

                    foreach (var item in ListCartItemFromSession)
                    {
                        if (ItemInMyCart.Any(c=>c.ProductDetail_ID==item.ProductDetail_ID))// nếu trùng thì check và công sl
                        {
                            var productDetailFromDb = await _httpClient.GetFromJsonAsync<ProductDetailVM>($"https://localhost:7007/api/productDetail/get/{item.ProductDetail_ID}");
                            var cartitem = ItemInMyCart.FirstOrDefault(c => c.ProductDetail_ID == item.ProductDetail_ID);
                            if (cartitem.Quantity+item.Quantity>productDetailFromDb.Quantity) // nếu tống sl sp + sl sp trong session > sl sp trong db ==> cho bằng kho
                            {
                                cartitem.Quantity = productDetailFromDb.Quantity;
                                
                            }
                            // nếu tống sl sp + sl sp trong session < sl sp trong db ==> tổng
                            cartitem.Quantity += item.Quantity;
                            var updateResponse = await _httpClient.PutAsJsonAsync($"https://localhost:7007/api/cartitem/Update-CartItem?id={cartitem.Id}", cartitem);
                            if (!updateResponse.IsSuccessStatusCode)
                            {
                                return BadRequest();
                            }
                        }
                        else // nếu không trùng thì thêm vào giỏ hàng
                        {
                            item.CartId = MyCart.Id;
                            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7007/api/cartitem/Insert-Cart-Item", item);
                            if (!response.IsSuccessStatusCode)
                            {
                                return BadRequest();
                            }
                        }
                    }
                    // xoa gio hang
                    for (int i = ListCartItemFromSession.Count - 1; i >= 0; i--)
                    {
                        ListCartItemFromSession.RemoveAt(i);
                    }
                    SessionServices.SetCartItemToSession(HttpContext.Session, "Cart", ListCartItemFromSession);
                    
                }
                if (role == "User") { return RedirectToAction("Index", "Home"); }
                else
                {
                    return Redirect("Admin/Home/Index");
                }
            }
            else
            {
                ViewBag.Message = result.Messages;
                return View();
            }
        }
        public async Task<IActionResult> LogOut()
        {
           var sd = await _authenticationService.Logout();
            if (sd.IsSuccess)
            {
                Response.Cookies.Delete("Cookie_Cua_Trung");
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Indexs", "Home");
        }
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var urlRegister = "https://localhost:7007/api/authentication/register";
            var result = await _authenticationService.Register(model, urlRegister);
            if (result.IsSuccess)
            {
                var LoginModel = new LoginViewModel()
                {
                    UserName = model.UserName,
                    Password = model.Password
                };
               return await Login(LoginModel);
            }
            else
            {
                ViewBag.Message = result.Messages;
                return View();
            }
        }
    }
}
