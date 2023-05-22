using ParcialVanessaAlfaro.DAL.Entities;
namespace ParcialVanessaAlfaro.DAL
{
    public class SeederDb
    {
        private readonly DataBaseContext _context;
        public SeederDb(DataBaseContext context)
        {
            _context = context;
        }

        public async Task SeederAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await PopulateTicketsAsync();
        }

        private async Task PopulateTicketsAsync()
        {
            if (!_context.Ticket.Any())
            {
                for (int i = 0; i < 50000; i++)
                {
                    var ticket = new Ticket {  IsUsed = false};
                    _context.Ticket.Add(ticket);
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
