namespace SeetReservation.Domain.Reservations
{
    /// <summary>
    /// Определяет возможные состояния бронирования в системе
    /// </summary>
    public enum ReservationStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
}
