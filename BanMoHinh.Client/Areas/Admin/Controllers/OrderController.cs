using BanMoHinh.Share.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BanMoHinh.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        HttpClient _client;

        public OrderController(HttpClient httpClient)
        {
            _client = httpClient;
        }
        [HttpGet]
        public async Task<IActionResult> ShowList()
        {
            string apiurl = "https://localhost:7007/api/order/getall";
            var response = await _client.GetAsync(apiurl);
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<Order>>(data);
            return View(result);

        }

        [HttpGet]
        public async Task<IActionResult> DetailsOrder(Guid id)
        {

            string apiurl = $"https://localhost:7007/api/order/details/{id}" ;
            var response = await _client.GetAsync(apiurl);
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Order>(data);
            return View(result);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {

           
            string apiurl = "https://localhost:7007/api/order/add";
            var data = JsonConvert.SerializeObject(order);
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

            string apiurl = $"https://localhost:7007/api/order/details/{id}";
            var response = await _client.GetAsync(apiurl );
            var data = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Order>(data);
            return View(result);


        }
        [HttpPost]
        public async Task<IActionResult> Edit(Order order)
        {

            string apiurl = $"https://localhost:7007/api/order/update/{order.Id} ";
            var data = JsonConvert.SerializeObject(order);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(apiurl , stringContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowList");
            }
            return View();
        }

        public async Task<IActionResult> DeleteC(Guid id )
        {

            string apiurl = $"https://localhost:7007/api/order/delete/{id}";

            var response = await _client.DeleteAsync(apiurl);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ShowList");
            }
            return View();
        }
    }
}
