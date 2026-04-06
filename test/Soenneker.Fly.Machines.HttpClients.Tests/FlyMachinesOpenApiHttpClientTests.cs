using Soenneker.Fly.Machines.HttpClients.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Fly.Machines.HttpClients.Tests;

[Collection("Collection")]
public sealed class FlyMachinesOpenApiHttpClientTests : FixturedUnitTest
{
    private readonly IFlyMachinesOpenApiHttpClient _httpclient;

    public FlyMachinesOpenApiHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<IFlyMachinesOpenApiHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
