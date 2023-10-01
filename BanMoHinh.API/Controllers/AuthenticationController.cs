using BanMoHinh.API.IServices;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _userAuthentication;


        public AuthenticationController(IAuthenticationService userAuthentication)
        {
            _userAuthentication = userAuthentication;
        }
        [HttpPost("login")]
        
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _userAuthentication.Login(model);
            return Ok(result);
        }
        [HttpPost("register")]
        
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _userAuthentication.Register(model);
            return Ok(result);
        }
    }
}
