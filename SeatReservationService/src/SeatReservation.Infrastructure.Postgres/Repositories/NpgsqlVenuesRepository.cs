using SeatReservation.Domain.Venues;
using Dapper;
using SeatReservation.Infrastructure.Postgres.Database;
using SeatReservation.Application.Database;
using Shared;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;


namespace SeatReservation.Infrastructure.Postgres.Repositories
{
    /// <summary>
    /// Реализация репозитория Venue с использованием Npgsql и Dapper(микро-ORM).
    /// </summary>
    public class NpgsqlVenuesRepository : IVenuesRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly ILogger<NpgsqlVenuesRepository> _logger;

        public NpgsqlVenuesRepository(
            IDbConnectionFactory connectionFactory,
            ILogger<NpgsqlVenuesRepository> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }

        /// <summary>
        /// Добавляет площадку с использованием "сырого" SQL через Dapper.
        /// </summary>
        public async Task<Result<Guid, Error>> Add(Venue venue, CancellationToken cancellationToken)
        {
            // Создаем и открываем соединение с базой данных PostgreSQL.
            // using гарантирует, что соединение будет корректно закрыто и освобождено
            using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);

            // Начинаем транзакцию базы данных.
            // using гарантирует, что транзакция будет корректно освобождена    
            using var transaction = connection.BeginTransaction();

            try
            {
                // 1. Вставка основной информации о площадке
                const string venueInsertSql = """
                                          INSERT INTO venues (id, prefix, name, seats_limit)
                                          VALUES (@Id, @Prefix, @Name, @SeatsLimit)
                                          """;

                var venueInsertParams = new
                {
                    Id = venue.Id.Value,
                    Prefix = venue.Name.Prefix,
                    Name = venue.Name.Name,
                    SeatsLimit = venue.SeatsLimit
                };

                await connection.ExecuteAsync(venueInsertSql, venueInsertParams);

                // 2. Если нет мест, завершаем транзакцию
                if (!venue.Seats.Any())
                {
                    transaction.Commit();

                    return venue.Id.Value;
                }

                // 3. Массовая вставка мест
                const string seatsInsertSql = """
                                          INSERT INTO seats (id, row_number, seat_number, venue_id)
                                          VALUES (@Id, @RowNumber, @SeatNumber, @VenueId)
                                          """;

                var seatsInsertParams = venue.Seats.Select(s => new
                {
                    Id = s.Id.Value,
                    RowNumber = s.RowNumber,
                    SeatNumber = s.SeatNumber,
                    VenueId = venue.Id.Value
                });

                await connection.ExecuteAsync(seatsInsertSql, seatsInsertParams);

                transaction.Commit();

                return venue.Id.Value;
            }
            catch (Exception ex)
            {
                //обработка уникальной ошибки

                //отменяем все изменения в текущей транзакции, если произошла ошибка
                transaction.Rollback();

                _logger.LogError(ex, "Fail to insert venue");

                return Error.Failure("venue.insert", "Fail to insert venue");
            }

            
        }
    }
}
