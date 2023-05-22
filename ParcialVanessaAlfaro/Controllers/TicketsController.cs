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
        [HttpPost]
        [Route ("Edit")]
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
