using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ParcialVanessaAlfaro.DAL.Entities;
using System.Text.Json.Serialization;

namespace WebPages.Controllers
{
    public class TicketsController : Controller
    {
        private readonly IHttpClientFactory _httpClient;

        public TicketsController(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

   
        
        [HttpPut]
        public async Task<IActionResult> UpdateValidation(Guid? id)
        {

                var url = $"https://localhost:7030/api/Tickets/Edit";
                var json = await _httpClient.CreateClient().GetStringAsync(url);
                var ticket = JsonConvert.DeserializeObject<Ticket>(json);

                return View("Index", ticket);
            
        }
    }
    }
