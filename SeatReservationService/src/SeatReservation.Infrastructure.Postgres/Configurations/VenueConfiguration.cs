using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain;
using SeatReservation.Domain.Venues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatReservation.Infrastructure.Postgres.Configurations
{
    public class VenueConfiguration : IEntityTypeConfiguration<Venue>
    {
        public void Configure(EntityTypeBuilder<Venue> builder)
        {
            builder.ToTable("venues");

            builder
                .HasKey(v => v.Id)
                .HasName("pk_venues");

            builder
                .Property(v => v.Id)
                .HasConversion(v => v.Value, id => new VenueId(id))
                .HasColumnName("id");

            

            builder.ComplexProperty(v => v.Name, nb =>
            {
                nb
                .Property(v => v.Prefix)
                .IsRequired()
                .HasMaxLength(LengthConstans.LENGTH50)
                .HasColumnName("prefix");

                nb
                .Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(LengthConstans.LENGTH500)
                .HasColumnName("name");
            });
            
            builder
                .HasMany(v => v.Seats)
                .WithOne()
                .HasForeignKey(s => s.VenueId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
