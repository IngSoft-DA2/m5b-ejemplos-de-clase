using Cine.BusinessLogic;
using Cine.Domain;
using Cine.Repository.Abstractions;
using Moq;

namespace Cine.Tests;

[TestClass]
public sealed class MovieServiceTests
{
    private Mock<IMovieRepository> _repositoryMock = null!;
    private MovieService _service = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _repositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);
        _service = new MovieService(_repositoryMock.Object);
    }

    [TestMethod]
    public void GetAll_DelegatesToRepository()
    {
        var movies = new List<Movie>
        {
            new() { Id = 1, Title = "A", Stars = 4.5 },
            new() { Id = 2, Title = "B", Stars = 3 }
        };

        _repositoryMock.Setup(r => r.GetAll()).Returns(movies);

        var result = _service.GetAll();

        Assert.AreSame(movies, result);
        _repositoryMock.VerifyAll();
    }

    [TestMethod]
    public void GetById_DelegatesToRepository()
    {
        var movie = new Movie { Id = 10, Title = "A", Stars = 4 };

        _repositoryMock.Setup(r => r.GetById(10)).Returns(movie);

        var result = _service.GetById(10);

        Assert.AreEqual(movie, result);
        _repositoryMock.VerifyAll();
    }

    [TestMethod]
    public void Create_WhenTitleIsBlank_ThrowsArgumentException()
    {
        Assert.ThrowsException<ArgumentException>(() => _service.Create(new Movie { Id = 1, Title = " ", Stars = 4 }));

        _repositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void Create_WhenStarsBelowZero_ThrowsArgumentOutOfRangeException()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _service.Create(new Movie { Id = 1, Title = "A", Stars = -0.1 }));

        _repositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void Create_WhenStarsAboveFive_ThrowsArgumentOutOfRangeException()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => _service.Create(new Movie { Id = 1, Title = "A", Stars = 5.1 }));

        _repositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void Create_ResetsIdToZeroBeforeAdding()
    {
        var input = new Movie { Id = 123, Title = "A", Stars = 4 };
        var created = new Movie { Id = 1, Title = "A", Stars = 4 };

        _repositoryMock
            .Setup(r => r.Add(It.Is<Movie>(m => m.Id == 0 && m.Title == "A" && m.Stars == 4)))
            .Returns(created);

        var result = _service.Create(input);

        Assert.AreEqual(created, result);
        _repositoryMock.VerifyAll();
    }

    [TestMethod]
    public void Update_WhenValid_DelegatesToRepository()
    {
        _repositoryMock.Setup(r => r.Update(10, It.Is<Movie>(m => m.Title == "A" && m.Stars == 4))).Returns(true);

        var result = _service.Update(10, new Movie { Id = 999, Title = "A", Stars = 4 });

        Assert.IsTrue(result);
        _repositoryMock.VerifyAll();
    }

    [TestMethod]
    public void Delete_DelegatesToRepository()
    {
        _repositoryMock.Setup(r => r.Delete(10)).Returns(true);

        var result = _service.Delete(10);

        Assert.IsTrue(result);
        _repositoryMock.VerifyAll();
    }
}
