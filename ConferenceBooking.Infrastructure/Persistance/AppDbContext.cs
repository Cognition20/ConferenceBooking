using ConferenceBooking.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceBooking.Infrastructure.Persistance;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ConferenceRoom>  ConferenceRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}