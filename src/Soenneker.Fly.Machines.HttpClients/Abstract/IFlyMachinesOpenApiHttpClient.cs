using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Fly.Machines.HttpClients.Abstract;

/// <summary>
/// Provides the configured HTTP client used to call the Fly Machines API.
/// </summary>
public interface IFlyMachinesOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the Fly Machines HTTP client owned by this provider.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The configured HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
