using ConferenceBooking.Application.Interfaces.Services;
using ConferenceBooking.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConferenceBooking.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IConferenceRoomService, ConferenceRoomService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IRoomPriceService, RoomPriceService>();
        
        return services;
    }
    
}