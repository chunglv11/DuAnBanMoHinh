using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private HttpClient _httpClient;

        public PostController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var posts = await _httpClient.GetFromJsonAsync<List<Post>>($"https://localhost:7007/api/posts/get-posts");
            return View(posts);
        }
        public IActionResult CreatePost()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePostAsync(Post post, string edit)
        {
            post.CreateAt = DateTime.Now;
            post.UpdateAt = DateTime.Now;
            post.Contents = edit;
            post.UserId  = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _httpClient.PostAsJsonAsync($"https://localhost:7007/api/posts/create-post",post);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<IActionResult> EditPostAsync(Guid id)
        {
            var post = await _httpClient.GetFromJsonAsync<Post>($"https://localhost:7007/api/posts/get/{id}");
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> EditPostAsync(Post post, string edit)
        {
            post.UpdateAt = DateTime.Now;
            post.Contents = edit;
            var result = await _httpClient.PostAsJsonAsync($"https://localhost:7007/api/posts/update-post/{post.Id}/{post.UserId}",post);
            if (result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DetailPost(Guid id)
        {
            var post = await _httpClient.GetFromJsonAsync<Post>($"https://localhost:7007/api/posts/get/{id}");
            return View(post);
        }
    }
}
