using Soenneker.Fly.Machines.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Fly.Machines.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class FlyMachinesOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IFlyMachinesOpenApiHttpClient _httpclient;

    public FlyMachinesOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IFlyMachinesOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
