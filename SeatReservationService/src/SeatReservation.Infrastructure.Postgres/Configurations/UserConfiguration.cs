using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SeatReservation.Domain;

namespace SeatReservation.Infrastructure.Postgres.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder
                .HasKey(v => v.Id)
                .HasName("pk_users");

            builder
                .Property(v => v.Id)
                .HasColumnName("id");

            builder.OwnsOne(u => u.Details, db =>
            {
                db.ToJson("details");

                db
                .Property(u => u.FIO)
                .IsRequired()
                .HasMaxLength(LengthConstans.LENGTH500)
                .HasColumnName("fio");

                db.OwnsMany(d => d.Socials, sb =>
                {
                    sb
                    .Property(u => u.Name)
                    .IsRequired()
                    .HasMaxLength(LengthConstans.LENGTH50)
                    .HasColumnName("name");

                    sb
                    .Property(u => u.Link)
                    .IsRequired()
                    .HasMaxLength(LengthConstans.LENGTH500)
                    .HasColumnName("link");
                });

                db
                .Property(u => u.Description)
                .IsRequired()
                .HasMaxLength(LengthConstans.LENGTH500)
                .HasColumnName("description");
            });
        }
    }
}
