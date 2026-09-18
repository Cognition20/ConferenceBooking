using ConferenceBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceBooking.Infrastructure.Persistance.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.StartAtUtc).IsRequired();
        builder.Property(b => b.EndAtUtc).IsRequired();
        builder.Property(b => b.TotalPrice).HasColumnType("decimal(18,2)");
        builder.Property(b => b.IsCancelled).HasDefaultValue(false);
        
        builder.HasOne(b => b.ConferenceRoom)
            .WithMany(r => r.Bookings)         
            .HasForeignKey(b => b.ConferenceRoomId)  
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(b => b.Services)
            .WithMany();                  
    }
}