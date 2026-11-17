using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Cloudflare.NuGet.HttpClients.Abstract;
using Soenneker.Cloudflare.NuGet.OpenApiClientUtil.Abstract;
using Soenneker.Cloudflare.NuGet.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Cloudflare.NuGet.OpenApiClientUtil;

public sealed class CloudflareNuGetOpenApiClientUtil : ICloudflareNuGetOpenApiClientUtil
{
    private readonly AsyncSingleton<CloudflareNuGetOpenApiClient> _client;

    public CloudflareNuGetOpenApiClientUtil(ICloudflareNuGetOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<CloudflareNuGetOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            // Credentials are attached only by the configured HTTP client; avoid a second authentication provider.
            var requestAdapter = new HttpClientRequestAdapter(new Microsoft.Kiota.Abstractions.Authentication.AnonymousAuthenticationProvider(),
                httpClient: httpClient) { BaseUrl = httpClient.BaseAddress!.AbsoluteUri.TrimEnd('/') };

            return new CloudflareNuGetOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<CloudflareNuGetOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
