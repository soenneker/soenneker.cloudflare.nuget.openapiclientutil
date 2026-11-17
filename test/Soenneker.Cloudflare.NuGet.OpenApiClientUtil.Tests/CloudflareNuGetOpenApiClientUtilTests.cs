using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Cloudflare.NuGet.OpenApiClientUtil.Abstract;
using Soenneker.Cloudflare.NuGet.OpenApiClientUtil.Registrars;

namespace Soenneker.Cloudflare.NuGet.OpenApiClientUtil.Tests;

public sealed class CloudflareNuGetOpenApiClientUtilTests
{
    [Test]
    public async Task Uses_configured_origin_and_reuses_client()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton<IConfiguration>(new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
        {
            ["Cloudflare:NuGet:BaseUrl"] = "https://private-feed.test/",
            ["Cloudflare:NuGet:ApiKey"] = "test-key"
        }).Build());
        services.AddCloudflareNuGetOpenApiClientUtilAsSingleton();
        await using var provider = services.BuildServiceProvider();
        var utility = provider.GetRequiredService<ICloudflareNuGetOpenApiClientUtil>();
        var client = await utility.Get();
        await Assert.That(ReferenceEquals(client, await utility.Get())).IsTrue();
        await Assert.That(client.V3.IndexJson.ToGetRequestInformation().URI.AbsoluteUri).IsEqualTo("https://private-feed.test/v3/index.json");
    }
}
