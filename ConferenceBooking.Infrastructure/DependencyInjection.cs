using ConferenceBooking.Application.Interfaces.Repositories;
using ConferenceBooking.Infrastructure.Persistance;
using ConferenceBooking.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDb(configuration);
        
        return services;
    }
    private static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IConferenceRoomRepository, ConferenceRoomRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }

}