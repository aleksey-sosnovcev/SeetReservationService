using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeetReservation.Domain.Events;
using SeetReservation.Domain.Reservations;

namespace SeetReservation.Infrastructure.Postgres.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("reservations");

            builder
                .HasKey(r => r.Id)
                .HasName("pk_reservations");

            builder
                .Property(r => r.Id)
                .HasConversion(r => r.Value, id => new ReservationId(id))
                .HasColumnName("id");

            builder
                .Property(r => r.EventId)
                .HasConversion(r => r.Value, id => new EventId(id))
                .HasColumnName("event_id");

            builder
                .Property(r => r.UserId)
                .HasColumnName("user_id");

            builder
                .Property(r => r.Status)
                .HasConversion<string>()
                .HasColumnName("status");

            builder
                .Property(r => r.CreatedAt)
                .HasColumnName("created_at");


        }
    }
}
