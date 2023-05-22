using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcialVanessaAlfaro.DAL;
using ParcialVanessaAlfaro.DAL.Entities;
using System.Net.Sockets;

namespace ParcialVanessaAlfaro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly DataBaseContext _context;



        public TicketsController(DataBaseContext context)
        {
            _context = context;
        }
        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Ticket>> GetTicketsById(Guid? id)
        {
            var Ticket = await _context.Ticket.FirstOrDefaultAsync(c => c.Id == id);
            if (Ticket == null) return NotFound();
            return Ok(Ticket);
        }



        [HttpPost]
        [Route("Edit")]
        public async Task<IActionResult> UpdateValidation(Guid ticketId)
        {
            var existingTicket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == ticketId);

            if (existingTicket != null)
            {
                if (!existingTicket.IsUsed)
                {
                    try
                    {
                        existingTicket.UseDate = DateTime.Now;
                        existingTicket.IsUsed = true;

                        _context.Ticket.Update(existingTicket);

                        Random r = new Random();
                        int number= r.Next(1, 5);

                        string Entrance = "";

                        switch (number)
                        {
                            case 1:
                                Entrance = "South";
                                break;
                            case 2:
                                Entrance = "North";
                                break;
                            case 3:
                                Entrance = "West";
                                break;
                            case 4:
                                Entrance = "East";
                                break;
                            default:
                                break;
                        }

                        existingTicket.EntranceGate = Entrance;
                        _context.Ticket.Update(existingTicket);
                        await _context.SaveChangesAsync();
                        return Conflict("Valid Ticket, You can access to the concert");
                    }

                    catch (Exception e)
                    {
                        return Conflict(e.Message);
                    }
                }
                else
                {
                    return Conflict($"The ticket was used. Date: {existingTicket.UseDate}. EntranceGate: {existingTicket.EntranceGate}");


                }
            }
            else
            {
                return Conflict("Ticket doesn´t exists");


            }
        }
    }
}
