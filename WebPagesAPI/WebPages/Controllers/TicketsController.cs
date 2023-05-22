using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParcialVanessaAlfaro.DAL.Entities;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace WebPages.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public TicketsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateValidation(Guid? id)
        {
           
            var url = $"https://localhost:7030/api/Tickets/Edit/{id}";
            var response = await _httpClient.CreateClient().GetAsync(url);

            var json = await response.Content.ReadAsStringAsync();
            var ticket = JsonConvert.DeserializeObject<Ticket>(json);

            return View("Index", ticket);
        }
    }
    }
