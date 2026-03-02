namespace SeetReservation.Domain.Reservations
{
    public record ReservationSeatId(Guid Value);

    /// <summary>
    /// Представляет сущность "Забронированное место" (Reservation Seat) в системе
    /// Является связующей сущностью между бронированием и конкретным местом
    /// </summary>
    public class ReservationSeat
    {
        //EF Core
        private ReservationSeat() { }
        public ReservationSeat(ReservationSeatId id, Reservation reservation, Guid seatId)
        {
            Id = id;
            Reservation = reservation;
            SeatId = seatId;
            ReservedAt = DateTime.UtcNow;
        }

        public ReservationSeatId Id { get; }

        /// <summary>
        /// Навигационное свойство для доступа к родительскому бронированию
        /// Обеспечивает двунаправленную связь между Reservation и ReservationSeat
        /// </summary>
        public Reservation Reservation { get; private set; }
        public Guid SeatId { get; private set; }
        public DateTime ReservedAt { get; }
    }
}
