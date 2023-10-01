using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;

namespace BanMoHinh.API.IServices
{
    public interface IAuthenticationService
    {
        public Task<Response> Login(LoginViewModel model);
        public Task<Response> Register(RegisterViewModel model);
        public Task Logout();

    }
}
