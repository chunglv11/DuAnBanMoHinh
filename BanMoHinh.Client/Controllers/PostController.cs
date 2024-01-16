using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace BanMoHinh.Client.Controllers
{
    public class PostController : Controller
    {
        private HttpClient _httpClient;

        public PostController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> ListPostAsync()
        {
            var Post = await _httpClient.GetFromJsonAsync<List<Post>>("https://localhost:7007/api/posts/get-posts");
            return View(Post);
        }
        [HttpGet]
        public async Task<IActionResult> DetailPost(Guid id)
        {
            var post = await _httpClient.GetFromJsonAsync<Post>($"https://localhost:7007/api/posts/get/{id}");
            var lstUser = await _httpClient.GetFromJsonAsync<List<User>>($"https://localhost:7007/api/users/getall");
            ViewBag.user = lstUser;
            return View(post);
        }
    }
}
