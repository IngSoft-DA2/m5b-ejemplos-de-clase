using Cine.BusinessLogic;
using Cine.Domain;
using Cine.Repository.Abstractions;
using Moq;

namespace Cine.Tests;

[TestClass]
public sealed class MovieServiceTests
{
    [TestMethod]
    public void GetAll_DelegatesToRepository()
    {
        var movies = new List<Movie>
        {
            new() { Id = 1, Title = "A", Stars = 4.5 },
            new() { Id = 2, Title = "B", Stars = 3 }
        };

        var repositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);
        repositoryMock.Setup(r => r.GetAll()).Returns(movies);

        var service = new MovieService(repositoryMock.Object);

        var result = service.GetAll();

        Assert.AreSame(movies, result);
        repositoryMock.VerifyAll();
    }

    [TestMethod]
    public void GetById_DelegatesToRepository()
    {
        var movie = new Movie { Id = 10, Title = "A", Stars = 4 };

        var repositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);
        repositoryMock.Setup(r => r.GetById(10)).Returns(movie);

        var service = new MovieService(repositoryMock.Object);

        var result = service.GetById(10);

        Assert.AreEqual(movie, result);
        repositoryMock.VerifyAll();
    }

    [TestMethod]
    public void Create_WhenTitleIsBlank_ThrowsArgumentException()
    {
        var repositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);
        var service = new MovieService(repositoryMock.Object);

        Assert.ThrowsException<ArgumentException>(() => service.Create(new Movie { Id = 1, Title = " ", Stars = 4 }));

        repositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void Create_WhenStarsBelowZero_ThrowsArgumentOutOfRangeException()
    {
        var repositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);
        var service = new MovieService(repositoryMock.Object);

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => service.Create(new Movie { Id = 1, Title = "A", Stars = -0.1 }));

        repositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void Create_WhenStarsAboveFive_ThrowsArgumentOutOfRangeException()
    {
        var repositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);
        var service = new MovieService(repositoryMock.Object);

        Assert.ThrowsException<ArgumentOutOfRangeException>(() => service.Create(new Movie { Id = 1, Title = "A", Stars = 5.1 }));

        repositoryMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void Create_ResetsIdToZeroBeforeAdding()
    {
        var input = new Movie { Id = 123, Title = "A", Stars = 4 };
        var created = new Movie { Id = 1, Title = "A", Stars = 4 };

        var repositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);
        repositoryMock
            .Setup(r => r.Add(It.Is<Movie>(m => m.Id == 0 && m.Title == "A" && m.Stars == 4)))
            .Returns(created);

        var service = new MovieService(repositoryMock.Object);

        var result = service.Create(input);

        Assert.AreEqual(created, result);
        repositoryMock.VerifyAll();
    }

    [TestMethod]
    public void Update_WhenValid_DelegatesToRepository()
    {
        var repositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);
        repositoryMock.Setup(r => r.Update(10, It.Is<Movie>(m => m.Title == "A" && m.Stars == 4))).Returns(true);

        var service = new MovieService(repositoryMock.Object);

        var result = service.Update(10, new Movie { Id = 999, Title = "A", Stars = 4 });

        Assert.IsTrue(result);
        repositoryMock.VerifyAll();
    }

    [TestMethod]
    public void Delete_DelegatesToRepository()
    {
        var repositoryMock = new Mock<IMovieRepository>(MockBehavior.Strict);
        repositoryMock.Setup(r => r.Delete(10)).Returns(true);

        var service = new MovieService(repositoryMock.Object);

        var result = service.Delete(10);

        Assert.IsTrue(result);
        repositoryMock.VerifyAll();
    }
}
