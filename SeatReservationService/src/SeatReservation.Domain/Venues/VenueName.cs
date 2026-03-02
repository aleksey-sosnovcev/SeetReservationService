using CSharpFunctionalExtensions;
using Shared;

namespace SeetReservation.Domain.Venues
{
    public record VenueName
    {
        private VenueName(string name, string prefix)
        {
            Name = name;
            Prefix = prefix;
        }
        public string Name { get; }
        public string Prefix { get; }

        public override string ToString() => $"{Prefix}-{Name}";
       
        public static Result<VenueName, Error> Create(string name, string prefix)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(name))
            {
                return Error.Validation("venue.name", "Venue name cannot be empty or whitespace");
            }

            if(prefix.Length > LengthConstans.LENGTH50 || name.Length > LengthConstans.LENGTH500)
            {
                return Error.Validation("venue.name", "Venue name too long");
            }

            return new VenueName(name, prefix);
        }
    }
}
