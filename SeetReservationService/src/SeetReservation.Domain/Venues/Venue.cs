using CSharpFunctionalExtensions;
using Shared;

namespace SeetReservation.Domain.Venues
{
    /// <summary>
    /// Представляет доменную сущность "Площадка" (Venue) в системе бронирования
    /// Является агрегатом, управляющим конфигурацией мест и лимитами вместимости
    /// </summary>
    public class Venue
    {
        private List<Seat> _seats;

        public Venue(Guid id, string name, int maxSeatsCount, IEnumerable<Seat> seats)
        {
            Id = id;
            Name = name;
            SeatsLimit = maxSeatsCount;
            _seats = seats.ToList();
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public int SeatsLimit { get; private set; }
        public int SeatsCount => _seats.Count;
        public IReadOnlyList<Seat> Seats => _seats;

        /// <summary>
        /// Добавляет новое место на площадку
        /// </summary>
        /// <param name="seat">Добавляемое место</param>
        /// <returns>UnitResult.Success в случае успеха, или Error при превышении лимита мест</returns>
        public UnitResult<Error> AddSeat(Seat seat)
        {
            if (SeatsCount >= SeatsLimit)
            {
                return Error.Conflict("venue.seats.limit", "");
            }

            _seats.Add(seat);

            return UnitResult.Success<Error>();
        }

        /// <summary>
        /// Расширяет лимит мест на площадке
        /// Позволяет увеличить максимальное количество мест при расширении площадки
        /// </summary>
        /// <param name="newSeatsLimit">Новое значение максимального лимита мест</param>
        public void ExpandSeatsLimit(int newSeatsLimit) => SeatsLimit = newSeatsLimit;
    }
}
