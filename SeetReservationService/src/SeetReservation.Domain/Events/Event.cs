namespace SeetReservation.Domain.Events
{
    /// <summary>
    /// Представляет доменную сущность "Событие" (Event) в системе бронирования
    /// Содержит основную информацию о мероприятии и является агрегатом в контексте DDD
    /// </summary>
    public class Event
    {
        public Event(Guid id, Guid venueId, string name, DateTime eventDate, EventDetails details)
        {
            Id = id;
            VenueId = venueId;
            Name = name;
            EventDate = eventDate;
            Details = details;
        }

        public Guid Id { get; }

        /// <summary>
        /// Идентификатор площадки проведения (внешний ключ к агрегату Venue)
        /// Указывает, где именно проводится данное мероприятие
        /// </summary>
        public Guid VenueId { get; private set; }
        public string Name { get; private set; }
        public DateTime EventDate { get; private set; }
        public EventDetails Details { get; private set; }
    }
}
