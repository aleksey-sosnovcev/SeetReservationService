namespace SeetReservation.Domain.Reservations
{
    /// <summary>
    /// Представляет сущность "Забронированное место" (Reservation Seat) в системе
    /// Является связующей сущностью между бронированием и конкретным местом
    /// </summary>
    public class ReservationSeat
    {
        public ReservationSeat(Guid id, Reservation reservation, Guid seatId)
        {
            Id = id;
            Reservation = reservation;
            SeatId = seatId;
            ReservedAt = DateTime.UtcNow;
        }

        public Guid Id { get; }

        /// <summary>
        /// Навигационное свойство для доступа к родительскому бронированию
        /// Обеспечивает двунаправленную связь между Reservation и ReservationSeat
        /// </summary>
        public Reservation Reservation { get; private set; }
        public Guid SeatId { get; private set; }
        public DateTime ReservedAt { get; }
    }
}
