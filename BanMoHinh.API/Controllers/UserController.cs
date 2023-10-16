using BanMoHinh.API.IServices;
using BanMoHinh.API.Services;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Get all User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _userService.GetAll());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                return Ok(await _userService.GetItem(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }

        /// <summary>
        /// Delete User by id
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                return Ok(await _userService.Delete(id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel item,string roleName) 
        {
            try
            {
                return Ok(await _userService.Create(item, roleName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(UserViewModel item)
        {
            try
            {
                return Ok(await _userService.Update(item));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("change-role/{userId}/{roleName}")]
        public async Task<IActionResult> ChangeRole(Guid userId, string roleName)
        {
            try
            {
                return Ok(await _userService.ChangeRole(userId, roleName));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("reset-password/{id}/{newPassword}")]
        public async Task<IActionResult> ResetPassword(Guid id, string newPassword)
        {
            try
            {
                return Ok(await _userService.ResetPassword(id, newPassword));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
        [HttpGet("change-password/{id}/{currentPassword}/{newPassword}")]
        public async Task<IActionResult> ChangePassword(string id, string currentPassword, string newPassword)
        {
            try
            {
                return Ok(await _userService.ChangePassword(id, currentPassword, newPassword));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Không lấy được dữ liệu");
            }
        }
    }
}
