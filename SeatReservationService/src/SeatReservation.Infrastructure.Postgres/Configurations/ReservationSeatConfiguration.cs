using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeetReservation.Domain.Reservations;

namespace SeetReservation.Infrastructure.Postgres.Configurations
{
    public class ReservationSeatConfiguration : IEntityTypeConfiguration<ReservationSeat>
    {
        public void Configure(EntityTypeBuilder<ReservationSeat> builder)
        {
            builder.ToTable("reservation_seats");

            builder
                .HasKey(r => r.Id)
                .HasName("pk_reservation_seats");

            builder
                .Property(r => r.Id)
                .HasConversion(r => r.Value, id => new ReservationSeatId(id))
                .HasColumnName("id");


        }
    }
}
