using CSharpFunctionalExtensions;
using SeatReservation.Domain.Events;

namespace SeatReservation.Domain.Reservations
{
    public record ReservationId(Guid Value);
    /// <summary>
    /// Представляет доменную сущность "Бронирование" (Reservation) в системе
    /// Является агрегатом, управляющим процессом временного удержания мест на событие
    /// </summary>
    public class Reservation
    {
        private List<ReservationSeat> _reservedSeats;

        //EF Core
        private Reservation() { }
        public Reservation(ReservationId id, EventId eventId, Guid userId, IEnumerable<Guid> seatIds)
        {
            Id = id;
            EventId = eventId;
            UserId = userId;
            Status = ReservationStatus.Pending;
            CreatedAt = DateTime.UtcNow;

            // Создаем коллекцию забронированных мест на основе идентификаторов, переданных пользователем
            // Каждая запись ReservationSeat фиксирует факт бронирования конкретного места в рамках данного бронирования
            var reservedSeats = seatIds
                .Select(seatId => new ReservationSeat(new ReservationSeatId(Guid.NewGuid()), this, seatId))
                .ToList();

            _reservedSeats = reservedSeats;
        }

        public ReservationId Id { get; }

        /// <summary>
        /// Идентификатор события, на которое выполняется бронирование
        /// Связывает бронирование с конкретным мероприятием
        /// </summary>
        public EventId EventId { get; private set; }
        public Guid UserId { get; private set; }
        public ReservationStatus Status { get; private set; }
        public DateTime CreatedAt { get; }
        public IReadOnlyList<ReservationSeat> ReservedSeats => _reservedSeats;
    }
}
