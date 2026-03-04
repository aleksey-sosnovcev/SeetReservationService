using CSharpFunctionalExtensions;
using Shared;

namespace SeatReservation.Domain.Venues
{
    public record SeatId(Guid Value);

    /// <summary>
    /// Представляет доменную сущность "Место" (Seat) в конфигурации площадки
    /// Содержит информацию о расположении места в зале: ряд и номер
    /// </summary>
    public class Seat
    {
        //EF Core
        private Seat() { }
        private Seat(SeatId id, int rowNumber, int seatNumber)
        {
            Id = id;
            RowNumber = rowNumber;
            SeatNumber = seatNumber;
        }
        public SeatId Id { get; }
        public VenueId VenueId { get; private set; }
        public int RowNumber { get; private set; }
        public int SeatNumber { get; private set; }

        /// <summary>
        /// Фабричный метод для создания нового места с валидацией входных параметров
        /// </summary>
        /// <param name="rowNumber">Номер ряда (должен быть больше 0)</param>
        /// <param name="seatNumber">Номер места (должен быть больше 0)</param>
        /// <returns>
        /// Result успеха с экземпляром Seat при корректных параметрах, 
        /// или
        /// Result ошибки с детализированной информацией о нарушении бизнес-правил
        /// </returns>
        public static Result<Seat, Error> Create(int rowNumber, int seatNumber)
        {
            if (rowNumber <= 0)
            {
                return Error.Validation("seat.rowNumber", "Row number must be greater than zero");
            }

            if (seatNumber <= 0)
            {
                return Error.Validation("seat.seatNumber", "Seat number must be greater than zero");
            }

            return new Seat(new SeatId(Guid.NewGuid()), rowNumber, seatNumber);
        }
    }
}
