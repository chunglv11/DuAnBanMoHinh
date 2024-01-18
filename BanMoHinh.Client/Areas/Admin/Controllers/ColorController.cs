﻿using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Security.Claims;
using System.Text;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class ColorController : Controller
    {
        private HttpClient _httpClient;
        Uri Url = new Uri("https://localhost:7007/api/color");
        public ColorController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Show()
        {
            var response = await _httpClient.GetAsync(Url + "/get-all-Color");
            // Lấy dữ liệu Json trả về từ Api được call dạng string
            string apiData = await response.Content.ReadAsStringAsync();
            // Lấy kqua trả về từ API
            // Đọc từ string Json vừa thu được sang List<T>
            var colors = JsonConvert.DeserializeObject<List<Colors>>(apiData);
            return View(colors);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Colors colors)
        {
            var response = await _httpClient.PostAsJsonAsync(Url + "/create-Color", colors);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = errorMessage;
                return View();
            }

        }
        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            var response = await _httpClient.GetAsync(Url + $"/get-{id}");
            if (response.IsSuccessStatusCode)
            {
                var apiData = await response.Content.ReadAsStringAsync();
                var co = JsonConvert.DeserializeObject<Colors>(apiData);
                return View(co);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = errorMessage;
                return View();
            }
        }

        public async Task<IActionResult> Update(Guid id, Colors create)
        {
            try
            {

                var response = await _httpClient.PutAsJsonAsync(Url + $"/update-Color-{id}", create);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Show");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ViewBag.ErrorMessage = errorMessage;
                    return View();
                }

            }
            catch (Exception)
            {
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid Id)
        {
            var response = await _httpClient.GetAsync(Url + $"/get-{Id}");

            if (response.IsSuccessStatusCode)
            {
                var apiData = await response.Content.ReadAsStringAsync();
                var fo = JsonConvert.DeserializeObject<Colors>(apiData);
                return View(fo);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                ViewBag.ErrorMessage = errorMessage;
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Remove(Guid Id)
        {
            var response = await _httpClient.DeleteAsync(Url + $"/delete-Color-{Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Show");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
