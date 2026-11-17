using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Cloudflare.NuGet.HttpClients.Registrars;
using Soenneker.Cloudflare.NuGet.OpenApiClientUtil.Abstract;

namespace Soenneker.Cloudflare.NuGet.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class CloudflareNuGetOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="CloudflareNuGetOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddCloudflareNuGetOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddCloudflareNuGetOpenApiHttpClientAsSingleton()
                .TryAddSingleton<ICloudflareNuGetOpenApiClientUtil, CloudflareNuGetOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="CloudflareNuGetOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddCloudflareNuGetOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddCloudflareNuGetOpenApiHttpClientAsSingleton()
                .TryAddScoped<ICloudflareNuGetOpenApiClientUtil, CloudflareNuGetOpenApiClientUtil>();

        return services;
    }
}
