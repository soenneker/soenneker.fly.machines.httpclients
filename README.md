[![](https://img.shields.io/nuget/v/soenneker.fly.machines.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.fly.machines.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.fly.machines.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.fly.machines.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.fly.machines.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.fly.machines.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.fly.machines.httpclients/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.fly.machines.httpclients/actions/workflows/codeql.yml)

# Soenneker.Fly.Machines.HttpClients

Provides a cached `HttpClient` configured for the Fly Machines API.

## Installation

```bash
dotnet add package Soenneker.Fly.Machines.HttpClients
```

## Configuration

```json
{
  "Fly": {
    "ApiKey": "your-fly-api-token"
  }
}
```

Requests use bearer authentication and `https://api.machines.dev` by default. `Machines:AuthHeaderName`, `Machines:AuthHeaderValueTemplate`, and `Machines:ClientBaseUrl` can override the transport settings.

## Registration and usage

```csharp
using Soenneker.Fly.Machines.HttpClients.Abstract;
using Soenneker.Fly.Machines.HttpClients.Registrars;

services.AddFlyMachinesOpenApiHttpClientAsSingleton();

public sealed class FlyMachinesRequestSender(IFlyMachinesOpenApiHttpClient clients)
{
    public async Task<HttpResponseMessage> GetApps(CancellationToken cancellationToken)
    {
        HttpClient client = await clients.Get(cancellationToken);
        return await client.GetAsync("v1/apps", cancellationToken);
    }
}
```

Callers borrow the returned client and must not dispose it. The provider owns its cache entry and removes that exact client when disposed; scoped provider instances cannot tear down another scope's transport.
