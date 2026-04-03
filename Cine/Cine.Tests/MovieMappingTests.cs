using Cine.Contracts;
using Cine.Domain;
using Cine.Mappings;

namespace Cine.Tests;

[TestClass]
public sealed class MovieMappingTests
{
    private Movie _movie = null!;
    private MovieDto _dto = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _movie = new Movie { Id = 1, Title = "A", Stars = 4.5 };
        _dto = new MovieDto { Id = 1, Title = "A", Stars = 4.5 };
    }

    [TestMethod]
    public void ToDto_MapsAllProperties()
    {
        var dto = _movie.ToDto();

        Assert.AreEqual(1, dto.Id);
        Assert.AreEqual("A", dto.Title);
        Assert.AreEqual(4.5, dto.Stars);
    }

    [TestMethod]
    public void ToDomain_MapsAllProperties()
    {
        var movie = _dto.ToDomain();

        Assert.AreEqual(1, movie.Id);
        Assert.AreEqual("A", movie.Title);
        Assert.AreEqual(4.5, movie.Stars);
    }
}
