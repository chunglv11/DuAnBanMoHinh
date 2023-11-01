using BanMoHinh.Share.Models;
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
    }
}
