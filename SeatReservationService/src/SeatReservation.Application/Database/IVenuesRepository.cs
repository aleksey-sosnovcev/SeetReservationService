
using CSharpFunctionalExtensions;
using SeatReservation.Domain.Venues;
using Shared;

namespace SeatReservation.Application.Database
{
    /// <summary>
    /// Репозиторий для работы с сущностью Venue
    /// <para>
    /// Этот интерфейс является абстракцией, расположенной в слое Application, 
    /// что следует принципу инверсии зависимостей (DIP) из SOLID.
    /// 
    /// Слой Application не должен знать о конкретных технологиях доступа к данным,
    /// поэтому все зависимости направлены от Infrastructure к Application,
    /// а не наоборот.
    /// </para>
    /// </summary>
    public interface IVenuesRepository
    {
        /// <summary>
        /// Добавляет новую площадку с ее местами в единой транзакции.
        /// </summary>
        /// <param name="venue">Агрегат Venue, содержащий данные площадки и коллекцию мест.</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>
        /// В случае успеха создания содержит ID созданной площадки
        /// Иначе содержит детали ошибки
        /// </returns>
        Task<Result<Guid, Error>> Add(Venue venue, CancellationToken cancellationToken = default);
    }
}
