using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Fly.Machines.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Fly.Machines.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class FlyMachinesOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="FlyMachinesOpenApiHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddFlyMachinesOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IFlyMachinesOpenApiHttpClient, FlyMachinesOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="FlyMachinesOpenApiHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddFlyMachinesOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IFlyMachinesOpenApiHttpClient, FlyMachinesOpenApiHttpClient>();

        return services;
    }
}
