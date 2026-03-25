using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using SeatReservation.Application.Database;
using SeatReservation.Domain.Venues;
using Shared;

namespace SeatReservation.Infrastructure.Postgres.Repositories
{
    /// <summary>
    /// Реализация репозитория Venue с использованием Entity Framework Core.
    /// </summary>
    public class EfCoreVenuesRepository : IVenuesRepository
    {
        private readonly ReservationServiceDbContext _dbContext;
        private readonly ILogger<EfCoreVenuesRepository> _logger;

        public EfCoreVenuesRepository(
            ReservationServiceDbContext dbContext,
            ILogger<EfCoreVenuesRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Добавляет площадку с использованием Entity Framework Core.
        /// </summary>
        public async Task<Result<Guid, Error>> Add(Venue venue, CancellationToken cancellationToken)
        {
            try
            {
                // добавляем площадку в контекст 
                await _dbContext.Venues.AddAsync(venue, cancellationToken);

                // сохраняем все изменения в одной транзакции
                await _dbContext.SaveChangesAsync(cancellationToken);

                return venue.Id.Value;
            }
            catch (Exception ex)
            {
                //обработка уникальной ошибки
                _logger.LogError(ex, "Fail to insert venue");

                return Error.Failure("venue.insert", "Fail to insert venue");
            }
        }
    }
}
