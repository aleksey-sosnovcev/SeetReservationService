namespace SeetReservation.Domain.Events
{
    /// <summary>
    /// Представляет дополнительную информацию о событии (детали события)
    /// Является частью агрегата Event и имеет связь "один-к-одному" с основной сущностью Event
    /// </summary>
    public class EventDetails
    {
        //EF Core
        private EventDetails() { }
        public EventDetails(int capacity, string description)
        {
            Capacity = capacity;
            Description = description;
        }

        public EventId EventId { get; }
        public int Capacity { get; private set; }
        public string Description { get; private set; }
    }
}
