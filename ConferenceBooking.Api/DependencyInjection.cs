namespace ConferenceBooking.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddProblemDetailsConfiguration();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
    
    private static IServiceCollection AddProblemDetailsConfiguration(this IServiceCollection services)
    {

        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = (context) =>
            { 
                context.HttpContext.Response.ContentType = "application/problem+json";
                context.ProblemDetails.Extensions["TraceId"] = context.HttpContext.TraceIdentifier;
            };
        });
        return services;
    }
}