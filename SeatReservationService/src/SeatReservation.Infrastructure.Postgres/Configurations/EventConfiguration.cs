using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeetReservation.Domain.Events;

namespace SeetReservation.Infrastructure.Postgres.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("events");

            builder
                .HasKey(e => e.Id)
                .HasName("id");

            builder
                .Property(e => e.Id)
                .HasConversion(e => e.Value, id => new EventId(id))
                .HasColumnName("id");

            builder
                .Property(e => e.Name)
                .HasColumnName("name");
        }
    }
}
