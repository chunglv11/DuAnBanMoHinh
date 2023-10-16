using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly HttpClient _client;
        public CategoryController(HttpClient client)
        {
            _client = client;   
        }
        // GET: CategoryController
        public async Task<IActionResult> GetAllCategory()
        {
            var allCategory = await _client.GetFromJsonAsync<List<Category>>("https://localhost:7007/api/Category/get-all-Category");
            return View(allCategory);
        }

        // GET: CategoryController/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var getCategory = await _client.GetFromJsonAsync<Category>($"https://localhost:7007/api/Category/get-{id}");
            return View(getCategory);
        }

        // GET: CategoryController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category ct)
        {
            try
            {
                var createCategory = await _client.PostAsJsonAsync("https://localhost:7007/api/Category/create-Category", ct);
                return RedirectToAction("GetAllCategory");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var getCategory = await _client.GetFromJsonAsync<Category>($"https://localhost:7007/api/Category/get-{id}");
            return View(getCategory);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category ct)
        {
            try
            {
                var editCategory = await _client.PutAsJsonAsync($"https://localhost:7007/api/Category/update-Category-{ct.Id}", ct);
                return RedirectToAction("GetAllCategory");
            }
            catch
            {
                return View();
            }
        }

        // GET: CategoryController/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteCategory = await _client.DeleteAsync($"https://localhost:7007/api/Category/delete-Category-{id}");
            return RedirectToAction("GetAllCategory");
        }
    }
}
