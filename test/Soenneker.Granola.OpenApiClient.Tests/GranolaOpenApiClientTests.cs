using Soenneker.Tests.HostedUnit;

namespace Soenneker.Granola.OpenApiClient.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class GranolaOpenApiClientTests : HostedUnitTest
{
    public GranolaOpenApiClientTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
