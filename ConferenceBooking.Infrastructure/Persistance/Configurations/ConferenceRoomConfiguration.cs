using ConferenceBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceBooking.Infrastructure.Persistance.Configurations;

public class ConferenceRoomConfiguration : IEntityTypeConfiguration<ConferenceRoom>
{
    public void Configure(EntityTypeBuilder<ConferenceRoom> builder)
    {
        builder.ToTable("ConferenceRooms");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Capacity).IsRequired();
        builder.Property(c => c.PricePerHour).IsRequired().HasColumnType("decimal(18,2)");
        builder.HasMany(c => c.Services)
            .WithMany(c => c.Rooms);


    }
}