using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Fly.Machines.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Fly.Machines.HttpClients;

public sealed class FlyMachinesOpenApiHttpClient : IFlyMachinesOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;
    private readonly string _cacheKey = $"{nameof(FlyMachinesOpenApiHttpClient)}:{Guid.NewGuid():N}";

    private const string _prodBaseUrl = "https://api.machines.dev";

    public FlyMachinesOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_cacheKey, (config: _config, baseUrl: _config["Machines:ClientBaseUrl"] ?? _prodBaseUrl), static state =>
        {
            var apiKey = state.config.GetValueStrict<string>("Fly:ApiKey");
            string authHeaderName = state.config["Machines:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = state.config["Machines:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            return new HttpClientOptions
            {
                BaseAddress = new Uri(state.baseUrl),
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {authHeaderName, authHeaderValue},
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_cacheKey);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_cacheKey);
    }
}
