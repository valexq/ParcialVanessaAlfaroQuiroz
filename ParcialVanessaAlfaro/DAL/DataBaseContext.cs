using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using ParcialVanessaAlfaro.DAL.Entities;

namespace ParcialVanessaAlfaro.DAL
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) :base(options)
        {

        }
        public DbSet<Ticket> Ticket { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Tickets>().HasIndex(c=>c.Id).IsUnique();
        }
    }
}
