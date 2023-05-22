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


        [HttpPut]
        public async Task<IActionResult> UpdateValidation(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("Invalid Ticket, try again");
            }

            var url = $"https://localhost:7030/api/Tickets/Edit/";
            var response = await _httpClient.CreateClient().GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound("Ticket does not exists");
            }

            var json = await response.Content.ReadAsStringAsync();
            var ticket = JsonConvert.DeserializeObject<Ticket>(json);

            return View("Index", ticket);
        }
    }
    }
