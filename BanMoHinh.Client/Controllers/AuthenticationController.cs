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
        public IActionResult DemoLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> DemoLogin(LoginViewModel model)
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
                var check = User.Identity.IsAuthenticated;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Message = result.Messages;
                return View();
            }

            //var url = "https://localhost:7007/api/authentication/login";
            //var response = await _httpClient.PostAsJsonAsync(url, model);
            //if (response.IsSuccessStatusCode)
            //{
            //    var token = await response.Content.ReadAsStringAsync();
            //    var handler = new JwtSecurityTokenHandler();
            //    var jwt = handler.ReadJwtToken(token);
            //    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //    identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Name).Value));
            //    identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role).Value));
            //    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.NameIdentifier).Value));
            //    identity.AddClaim(new Claim(ClaimTypes.Email, jwt.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Email).Value));
            //    var principal = new ClaimsPrincipal(identity);
            //    await HttpContext.SignInAsync(principal);
            //    var check = User.Identity.IsAuthenticated;
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    return View();
            //}
        }
    }
}
