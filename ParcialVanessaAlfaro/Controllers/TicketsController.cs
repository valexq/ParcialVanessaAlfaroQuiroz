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
        public async Task<ActionResult<Ticket>> GetTicketById(Guid? Id)
        {
            var ticket = await _context.Ticket.FirstOrDefaultAsync(t => t.Id == Id); //Select * from Ticket where Id
            if (ticket == null) return NotFound("Invalid Ticket");
            return Ok(ticket);
        }

        //[HttpGet, ActionName("Create")]
        //[Route("Create")]
        //
        //public async Task<ActionResult> CreateTicket(Ticket ticket)
        //{
        //   
        //}
        //



    }
}
