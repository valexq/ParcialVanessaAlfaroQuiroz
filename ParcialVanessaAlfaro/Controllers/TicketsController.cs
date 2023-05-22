using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParcialVanessaAlfaro.DAL;
using ParcialVanessaAlfaro.DAL.Entities;

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

        [HttpGet]
        [Route("GetTicketsInformation")]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetTickets()
        {
            var tickets = await _context.Ticket.ToListAsync();
            if (tickets == null) return NotFound();
            return tickets;
        }


        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]
        public async Task<ActionResult<Ticket>> GetTicketsById(Guid? id)
        {
            var Ticket = await _context.Ticket.FirstOrDefaultAsync(c => c.Id == id);
            if (Ticket == null) return NotFound();
            return Ok(Ticket);
        }

        //Si la boleta existe y aún no ha sido usada, entonces debe mostrar el mensaje de “Boleta válida,
        //puede ingresar al concierto”. En esta vista, deberá actualizar los siguientes campos de esa boleta:
        //la fecha UseDate porque es la fecha que entra al concierto, actualizar el campo IsUsed en True porque
        //ya indica que la boleta está usada, y deberá guardar un valor (string) en el campo EntranceGate para
        //indicar la portería por donde ingresó la persona, por ejemplo “Portería Sur”.
        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<Ticket>> EditTicket(Guid? id, String entranceGate)
        {
            var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == id);

            if (ticket != null)
            {
               
                if (ticket.IsUsed == false)
                {
                    try
                    {
                        ticket.UseDate = DateTime.Now;
                        ticket.IsUsed = true;
                        ticket.EntranceGate = entranceGate;
                        _context.Ticket.Update(ticket);
                        await _context.SaveChangesAsync();
                        return Ok("Ticket is valid. You can access to the concert");
                    }
                    
                    catch (Exception e)
                    {
                        return Conflict(e.Message);
                    }

                    return Ok(ticket);

                }
                return Conflict($"Ticket was used. Date {ticket.UseDate}, EntranceGate {ticket.EntranceGate}");


            }
            else
            {
                return Conflict("Ticket does not exist");
            }

            return NotFound();

        }


    }
}
