using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Solution.Architecture.Api.UnitTests.Factories;

internal class MockLinkGenerator : LinkGenerator
{
    public override string GetPathByAddress<TAddress>(HttpContext httpContext, TAddress address, RouteValueDictionary values,
        RouteValueDictionary? ambientValues = null, PathString? pathBase = null,
        FragmentString fragment = new(), LinkOptions? options = null)
    {
        throw new NotImplementedException();
    }

    public override string GetPathByAddress<TAddress>(TAddress address, RouteValueDictionary values,
        PathString pathBase = new(), FragmentString fragment = new(),
        LinkOptions? options = null)
    {
        throw new NotImplementedException();
    }

    public override string GetUriByAddress<TAddress>(HttpContext httpContext, TAddress address, RouteValueDictionary values,
        RouteValueDictionary? ambientValues = null, string? scheme = null, HostString? host = null,
        PathString? pathBase = null, FragmentString fragment = new(), LinkOptions? options = null)
    {
        return "A";
    }

    public override string GetUriByAddress<TAddress>(TAddress address, RouteValueDictionary values, string scheme, HostString host,
        PathString pathBase = new(), FragmentString fragment = new(),
        LinkOptions? options = null)
    {
        throw new NotImplementedException();
    }
}

public static class HttpContextFactory
{
    public static HttpContext Create()
    {
        var serviceCollection = new ServiceCollection();

        var featureCollection = new FeatureCollection();

        serviceCollection.AddLogging();
        serviceCollection.AddRouting();
        serviceCollection.AddSingleton<LinkGenerator, MockLinkGenerator>();

        featureCollection.Set<IHttpResponseFeature>(new HttpResponseFeature());
        featureCollection.Set<IHttpResponseBodyFeature>(new StreamResponseBodyFeature(new MemoryStream()));

        return new DefaultHttpContext(featureCollection)
        {
            RequestServices = serviceCollection.BuildServiceProvider()
        };
    }
}