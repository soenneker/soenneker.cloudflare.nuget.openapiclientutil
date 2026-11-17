using Soenneker.Cloudflare.NuGet.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Cloudflare.NuGet.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface ICloudflareNuGetOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>Gets the shared typed client using the configured authenticated HTTP client and feed origin.</summary>
    ValueTask<CloudflareNuGetOpenApiClient> Get(CancellationToken cancellationToken = default);
}
