using BanMoHinh.API.IServices;
using BanMoHinh.API.Services;
using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RateController : Controller
    {
        HttpClient _client;
        private IRateService _rateService;

        public RateController(HttpClient httpClient, IRateService rateService)
        {
            _client = httpClient;
            _rateService = rateService;
        }
        [HttpGet]
        public async Task<IActionResult> ShowList()
        {
            string apiurl = "https://localhost:7007/api/rate/getall";
            var response = await _client.GetAsync(apiurl);
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Rate>>(data);
            return View(result);

        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {

            string apiurl = "https://localhost:7007/api/rate/details/";
            var response = await _client.GetAsync(apiurl + id);
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Rate>(data);
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Rate rate)
        {

           
            string apiurl = "https://localhost:7007/api/rate/add";
            var data = JsonConvert.SerializeObject(rate);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync(apiurl, stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowList");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {

            string apiurl = "https://localhost:7007/api/rate/";
            var response = await _client.GetAsync(apiurl + id);
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Rate>(data);
            return View(result);


        }
        [HttpPost]
        public async Task<IActionResult> Edit(Rate rate)
        {

            string apiurl = "https://localhost:7007/api/rate/update/";
            var data = JsonConvert.SerializeObject(rate);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(apiurl + rate.Id, stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowList");
            }
            return View();
        }

        
        public async Task<IActionResult> DeleteC(Guid id, Guid orderid)
        {
            var rate = await _rateService.GetItem(id);
            if (rate != null)
            {
                orderid = rate.OrderItemId;

            }
            string apiurl = $"https://localhost:7007/api/rate/delete?id={id}&orderid={orderid}";

            var response = await _client.DeleteAsync(apiurl);


            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowList");
            }
            return View();
        }
    }
}
