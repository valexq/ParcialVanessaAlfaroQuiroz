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
        [Route("Edit/{id}")]
        public async Task<IActionResult> EditTicket(Ticket ticket)
        {
            var existingTicket = await _context.Ticket.FindAsync(ticket.Id);

            if (existingTicket != null)
            {
                if (!existingTicket.IsUsed)
                {
                    try
                    {
                        existingTicket.UseDate = DateTime.Now;
                        existingTicket.IsUsed = true;
                        existingTicket.EntranceGate = ticket.EntranceGate;
                        Random r= new Random();
                        int numberRandom = r.Next(1, 5);

                        string EntranceGate = "";

                        switch (numberRandom)
                        {
                            case 1:
                                EntranceGate = "North";
                                break;
                            case 2:
                                EntranceGate = "South";
                                break;
                            case 3:
                                EntranceGate = "West";
                                break;
                            case 4:
                                EntranceGate = "East";
                                break;
                            default:
                                break;
                        }

                        _context.Ticket.Update(existingTicket);
                        await _context.SaveChangesAsync();
                        return Ok("You can access to the concert");
                       
                    }
                    catch (Exception e)
                    {
                        return Conflict(e.Message);
                    }
                }
                else
                {
                    return Conflict($"Ticket was used. Date: {existingTicket.UseDate}, EntranceGate: {existingTicket.EntranceGate}");
                }
            }
            else
            {
                return Conflict("Ticket does not exist");
            }
        }
    }
}
