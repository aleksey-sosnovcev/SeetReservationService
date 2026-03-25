using CSharpFunctionalExtensions;
using Shared;

namespace SeatReservation.Domain.Venues
{

    public record VenueId(Guid Value);

    /// <summary>
    /// Представляет доменную сущность "Площадка" (Venue) в системе бронирования
    /// Является агрегатом, управляющим конфигурацией мест и лимитами вместимости
    /// </summary>
    public class Venue
    {
        private List<Seat> _seats = [];

        //EF Core
        private Venue() { }
        public Venue(VenueId id, VenueName name, int maxSeatsCount)
        {
            Id = id;
            Name = name;
            SeatsLimit = maxSeatsCount;
        }

        public VenueId Id { get; }
        public VenueName Name { get; private set; }
        public int SeatsLimit { get; private set; }
        public int SeatsCount => _seats.Count;
        public IReadOnlyList<Seat> Seats => _seats;

        public void AddSeats(IEnumerable<Seat> seats) => _seats.AddRange(seats);

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

        public static Result<Venue,Error> Create(
            string prefix,
            string name,
            int seatsLimit,
            VenueId? venueId = null)
        {
            if(seatsLimit <= 0)
            {
                return Error.Validation("venue.seatsLimit", "Seats limit must be greater than zero");
            }

            var venueNameResult = VenueName.Create(prefix, name);
            if(venueNameResult.IsFailure)
            {
                return venueNameResult.Error;
            }

            //var venueSeats = seats.ToList();

            //if(venueSeats.Count < 1)
            //{
            //    return Error.Validation("venue.seats", "Number of  seats can nit be zero");
            //}

            //if(venueSeats.Count > seatsLimit)
            //{
            //    return Error.Validation("venue.seats", "Number of seats exceeds the venue's seat limit");
            //}

            return new Venue(venueId ?? new VenueId(Guid.NewGuid()), venueNameResult.Value, seatsLimit);
        }
    }
}
