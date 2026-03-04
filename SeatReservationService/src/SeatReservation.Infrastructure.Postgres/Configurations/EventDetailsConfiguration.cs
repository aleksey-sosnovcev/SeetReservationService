using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain.Events;

namespace SeatReservation.Infrastructure.Postgres.Configurations
{
    public class EventDetailsConfiguration : IEntityTypeConfiguration<EventDetails>
    {
        public void Configure(EntityTypeBuilder<EventDetails> builder)
        {
            builder.ToTable("events_details");

            builder
                .HasKey(e => e.EventId)
                .HasName("pk_event_details");

            builder
                .Property(e => e.EventId)
                .HasConversion(e => e.Value, id => new EventId(id))
                .HasColumnName("event_id");

            builder
                .Property(e => e.Capacity)
                .HasColumnName("capacity");

            builder
                .Property(e => e.Description)
                .HasColumnName("description");

            builder
                .HasOne<Event>()
                .WithOne(e => e.Details)
                .HasForeignKey<EventDetails>(ed => ed.EventId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
