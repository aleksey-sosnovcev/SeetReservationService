using Microsoft.EntityFrameworkCore;
using SeetReservation.Domain.Venues;

namespace SeetReservation.Infrastructure.Postgres
{
    public class ReservationServiceDbContext : DbContext
    {
        private readonly string _connectionString;

        public ReservationServiceDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

        public DbSet<Venue> Venues => Set<Venue>();
    }
}
