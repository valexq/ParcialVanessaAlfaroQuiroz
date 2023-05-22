using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParcialVanessaAlfaro.DAL.Entities;
using System.Diagnostics;
using System.Net.Http;
using WebPages.Models;

namespace WebPages.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        //private readonly ILogger<HomeController> _logger;
        public HomeController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        //
        public IActionResult Index()
        {
            return View("Index");
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateValidation(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("Invalid Ticket, try again");
            }

            var url = $"https://localhost:7030/api/Tickets/Edit/{id}";
            var response = await _httpClient.CreateClient().GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Ticket does not exist");
            }

            var json = await response.Content.ReadAsStringAsync();
            var ticket = JsonConvert.DeserializeObject<Ticket>(json);

            return View("Index", ticket);
        }
    }

}
