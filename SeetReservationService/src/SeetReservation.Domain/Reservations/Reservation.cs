using CSharpFunctionalExtensions;

namespace SeetReservation.Domain.Reservations
{
    /// <summary>
    /// Представляет доменную сущность "Бронирование" (Reservation) в системе
    /// Является агрегатом, управляющим процессом временного удержания мест на событие
    /// </summary>
    public class Reservation
    {
        private List<ReservationSeat> _reservedSeats;

        public Reservation(Guid id, Guid eventId, Guid userId, IEnumerable<Guid> seatIds)
        {
            Id = id;
            EventId = eventId;
            UserId = userId;
            Status = ReservationStatus.Pending;
            CreatedAt = DateTime.UtcNow;

            // Создаем коллекцию забронированных мест на основе идентификаторов, переданных пользователем
            // Каждая запись ReservationSeat фиксирует факт бронирования конкретного места в рамках данного бронирования
            var reservedSeats = seatIds
                .Select(seatId => new ReservationSeat(Guid.NewGuid(), this, seatId))
                .ToList();

            _reservedSeats = reservedSeats;
        }

        public Guid Id { get; }

        /// <summary>
        /// Идентификатор события, на которое выполняется бронирование
        /// Связывает бронирование с конкретным мероприятием
        /// </summary>
        public Guid EventId { get; private set; }
        public Guid UserId { get; private set; }
        public ReservationStatus Status { get; private set; }
        public DateTime CreatedAt { get; }
        public IReadOnlyList<ReservationSeat> ReservedSeats => _reservedSeats;
    }
}
