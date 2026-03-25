using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SeatReservation.Application.Database;
using SeatReservation.Domain.Venues;

namespace SeatReservation.Infrastructure.Postgres
{
    // Контекст базы данных Entity Framework Core для сервиса бронирования мест.
    public class ReservationServiceDbContext : DbContext
    {
        private readonly string _connectionString;

        public ReservationServiceDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Настраивает параметры подключения и логирования
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);

            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseLoggerFactory(CreateLoggerFactory());
        }

        // Настраивает модели сущностей и их отображение на таблицы базы данных.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Автоматически применяет все конфигурации из текущей сборки,
            // реализующие интерфейс IEntityTypeConfiguration<T>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationServiceDbContext).Assembly);
        }

        // Набор сущностей Venue для выполнения запросов к таблице venues.
        public DbSet<Venue> Venues => Set<Venue>();

        // Создает фабрику логгеров
        private ILoggerFactory CreateLoggerFactory() =>
            LoggerFactory.Create(builer => { builer.AddConsole(); });
    }
}
