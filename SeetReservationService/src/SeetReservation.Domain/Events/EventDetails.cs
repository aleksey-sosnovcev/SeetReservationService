namespace SeetReservation.Domain.Events
{
    /// <summary>
    /// Представляет объект-значение (Value Object) с детальной информацией о событии
    /// Является неизменяемым (immutable) компонентом агрегата Event в соответствии с DDD
    /// </summary>
    public class EventDetails
    {
        public EventDetails(int capacity, string description)
        {
            Capacity = capacity;
            Description = description;
        }

        public Guid EventId { get; } = Guid.Empty;
        public int Capacity { get; private set; }
        public string Description { get; private set; }
    }
}
