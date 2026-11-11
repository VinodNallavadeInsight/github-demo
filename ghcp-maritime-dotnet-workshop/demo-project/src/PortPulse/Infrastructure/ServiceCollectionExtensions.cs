using Microsoft.Extensions.DependencyInjection;

namespace PortPulse.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPortPulseCore(this IServiceCollection services)
    {
        services.AddSingleton<TimeProvider>(TimeProvider.System);
        return services;
    }
}
