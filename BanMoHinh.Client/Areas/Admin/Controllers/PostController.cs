using BanMoHinh.Share.Models;
using BanMoHinh.Share.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

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
            var posts = await _httpClient.GetFromJsonAsync<List<PostVM>>($"https://localhost:7007/api/posts/get-posts");
            return View(posts);
        }
        public IActionResult CreatePost()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostVM post, IFormFile file, string edit)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    var userIdClaim = identity.FindFirst(ClaimTypes.NameIdentifier);
                    if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var userId))
                    {
                        // Xử lý dữ liệu và tệp nhận được từ action
                        post.CreateAt = DateTime.Now;
                        post.UpdateAt = DateTime.Now;
                        post.UserId = userId;
                        post.TittleImage = file.FileName;

                        // Gửi dữ liệu và tệp đến API
                        string apiurl = "https://localhost:7007/api/posts/create-post";
                        using (var client = new HttpClient())
                        using (var content = new MultipartFormDataContent())
                        {
                            //content.Add(new StringContent(post.Contents), "Contents");
                            //content.Add(new StringContent(post.Tittle), "Title");
                            content.Add(new StringContent(userId.ToString()), "UserId");
                            content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);

                            var response = await client.PostAsync(apiurl, content);
                            if (response.IsSuccessStatusCode)
                            {
                                // Xử lý khi yêu cầu thành công
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                // Xử lý khi yêu cầu không thành công
                                var errorMessage = await response.Content.ReadAsStringAsync();
                                // Trả về thông báo lỗi hoặc xử lý theo nhu cầu của bạn
                                return BadRequest(errorMessage);
                            }
                        }
                    }
                }
                // Xử lý khi có lỗi xảy ra
                return View();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View();
            }
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
            var result = await _httpClient.PostAsJsonAsync($"https://localhost:7007/api/posts/update-post/{post.Id}/{post.UserId}", post);
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
