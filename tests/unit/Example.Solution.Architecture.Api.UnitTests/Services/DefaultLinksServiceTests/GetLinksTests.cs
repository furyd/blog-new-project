using Example.Solution.Architecture.Api.Constants;
using Example.Solution.Architecture.Api.Services.Implementation;
using Example.Solution.Architecture.Domain.Repositories.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Example.Solution.Architecture.Api.UnitTests.Services.DefaultLinksServiceTests;

public class GetLinksTests
{
    private readonly Mock<IPagination> _pagination = new();

    [Fact]
    public void WhenHttpRequestIsNull_ThenShouldReturnEmptyCollection()
    {
        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor.Setup(accessor => accessor.HttpContext).Returns((HttpContext?)null);

        var service = new DefaultLinksService(httpContextAccessor.Object);

        var sut = service.GetLinks(_pagination.Object, 0);

        sut.Should().BeEmpty();
    }

    [Fact]
    public void WhenPageCountIsLessThanTwo_ThenShouldReturnCollectionWithJustItemLink()
    {
        const string scheme = "a";
        const string host = "b";
        const string path = "/c";

        var context = new DefaultHttpContext
        {
            Request =
            {
                Scheme = scheme,
                Host = new HostString(host),
                Path = new PathString(path)
            }
        };

        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor.Setup(accessor => accessor.HttpContext).Returns(context);

        _pagination.Setup(pagination => pagination.PageSize).Returns(10);

        var service = new DefaultLinksService(httpContextAccessor.Object);

        var sut = service.GetLinks(_pagination.Object, 0).ToList();

        sut.Should().HaveCount(1);
        sut.Should().Contain($"<{scheme}://{host}{path}/{{id}}>;rel={LinkRelations.Item}");
    }

    [Fact]
    public void WhenCurrentPageIsGreaterThanOneAndThereAreMoreThanOnePage_ThenShouldReturnCollectionWithFirstAndPreviousLinks()
    {
        const string scheme = "a";
        const string host = "b";
        const string path = "/c";

        var context = new DefaultHttpContext
        {
            Request =
            {
                Scheme = scheme,
                Host = new HostString(host),
                Path = new PathString(path)
            }
        };

        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor.Setup(accessor => accessor.HttpContext).Returns(context);

        _pagination.Setup(pagination => pagination.PageSize).Returns(10);
        _pagination.Setup(pagination => pagination.CurrentPage).Returns(2);

        var service = new DefaultLinksService(httpContextAccessor.Object);

        var sut = service.GetLinks(_pagination.Object, 20).ToList();

        sut.Should().HaveCount(3);
        sut.Should().Contain($"<{scheme}://{host}{path}/{{id}}>;rel={LinkRelations.Item}");
        sut.Should().Contain($"<{scheme}://{host}{path}?{QueryStrings.PageNumber}=1&{QueryStrings.PageSize}=10>;rel={LinkRelations.First}");
        sut.Should().Contain($"<{scheme}://{host}{path}?{QueryStrings.PageNumber}=1&{QueryStrings.PageSize}=10>;rel={LinkRelations.Previous}");
    }

    [Fact]
    public void WhenCurrentPageIsEqualToPageCount_ThenShouldReturnCollectionWithoutNextAndLastLinks()
    {
        const string scheme = "a";
        const string host = "b";
        const string path = "/c";

        var context = new DefaultHttpContext
        {
            Request =
            {
                Scheme = scheme,
                Host = new HostString(host),
                Path = new PathString(path)
            }
        };

        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor.Setup(accessor => accessor.HttpContext).Returns(context);

        _pagination.Setup(pagination => pagination.PageSize).Returns(10);
        _pagination.Setup(pagination => pagination.CurrentPage).Returns(2);

        var service = new DefaultLinksService(httpContextAccessor.Object);

        var sut = service.GetLinks(_pagination.Object, 20).ToList();

        sut.Should().HaveCount(3);
        sut.Should().Contain($"<{scheme}://{host}{path}/{{id}}>;rel={LinkRelations.Item}");
        sut.Should().Contain($"<{scheme}://{host}{path}?{QueryStrings.PageNumber}=1&{QueryStrings.PageSize}=10>;rel={LinkRelations.First}");
        sut.Should().Contain($"<{scheme}://{host}{path}?{QueryStrings.PageNumber}=1&{QueryStrings.PageSize}=10>;rel={LinkRelations.Previous}");
    }

    [Fact]
    public void WhenCurrentPageIsGreaterThanOneAndLowerThanPageCount_ThenShouldReturnAllPagingLinks()
    {
        const string scheme = "a";
        const string host = "b";
        const string path = "/c";

        var context = new DefaultHttpContext
        {
            Request =
            {
                Scheme = scheme,
                Host = new HostString(host),
                Path = new PathString(path)
            }
        };

        var httpContextAccessor = new Mock<IHttpContextAccessor>();
        httpContextAccessor.Setup(accessor => accessor.HttpContext).Returns(context);

        _pagination.Setup(pagination => pagination.PageSize).Returns(10);
        _pagination.Setup(pagination => pagination.CurrentPage).Returns(2);

        var service = new DefaultLinksService(httpContextAccessor.Object);

        var sut = service.GetLinks(_pagination.Object, 100).ToList();

        sut.Should().HaveCount(5);
        sut.Should().Contain($"<{scheme}://{host}{path}/{{id}}>;rel={LinkRelations.Item}");
        sut.Should().Contain($"<{scheme}://{host}{path}?{QueryStrings.PageNumber}=1&{QueryStrings.PageSize}=10>;rel={LinkRelations.First}");
        sut.Should().Contain($"<{scheme}://{host}{path}?{QueryStrings.PageNumber}=1&{QueryStrings.PageSize}=10>;rel={LinkRelations.Previous}");
        sut.Should().Contain($"<{scheme}://{host}{path}?{QueryStrings.PageNumber}=3&{QueryStrings.PageSize}=10>;rel={LinkRelations.Next}");
        sut.Should().Contain($"<{scheme}://{host}{path}?{QueryStrings.PageNumber}=10&{QueryStrings.PageSize}=10>;rel={LinkRelations.Last}");
    }
}