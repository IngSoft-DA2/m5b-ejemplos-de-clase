using Cine.BusinessLogic.Abstractions;
using Cine.Contracts;
using Cine.Controllers;
using Cine.Domain;
using Cine.Mappings;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Cine.Tests;

[TestClass]
public sealed class MoviesControllerTests
{
    [TestMethod]
    public void GetAll_ReturnsOkWithDtos()
    {
        var movies = new List<Movie>
        {
            new() { Id = 1, Title = "A", Stars = 4.5 },
            new() { Id = 2, Title = "B", Stars = 3 }
        };

        var movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
        movieServiceMock.Setup(s => s.GetAll()).Returns(movies);

        var controller = new MoviesController(movieServiceMock.Object);

        var response = controller.GetAll();

        var ok = response.Result as OkObjectResult;
        Assert.IsNotNull(ok);

        var dtos = (ok.Value as IEnumerable<MovieDto>)?.ToList();
        Assert.IsNotNull(dtos);
        Assert.AreEqual(2, dtos.Count);
        Assert.AreEqual(movies[0].ToDto(), dtos[0]);
        Assert.AreEqual(movies[1].ToDto(), dtos[1]);

        movieServiceMock.VerifyAll();
    }

    [TestMethod]
    public void GetById_WhenNotFound_ReturnsNotFound()
    {
        var movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
        movieServiceMock.Setup(s => s.GetById(10)).Returns((Movie?)null);

        var controller = new MoviesController(movieServiceMock.Object);

        var response = controller.GetById(10);

        Assert.IsInstanceOfType<NotFoundResult>(response.Result);
        movieServiceMock.VerifyAll();
    }

    [TestMethod]
    public void GetById_WhenFound_ReturnsOkWithDto()
    {
        var movie = new Movie { Id = 10, Title = "A", Stars = 4 };

        var movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
        movieServiceMock.Setup(s => s.GetById(10)).Returns(movie);

        var controller = new MoviesController(movieServiceMock.Object);

        var response = controller.GetById(10);

        var ok = response.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        Assert.AreEqual(movie.ToDto(), ok.Value as MovieDto);

        movieServiceMock.VerifyAll();
    }

    [TestMethod]
    public void Create_ReturnsCreatedAtActionWithDto()
    {
        var inputDto = new MovieDto { Id = 123, Title = "A", Stars = 4 };
        var created = new Movie { Id = 10, Title = "A", Stars = 4 };

        var movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
        movieServiceMock
            .Setup(s => s.Create(It.Is<Movie>(m => m.Id == 123 && m.Title == "A" && m.Stars == 4)))
            .Returns(created);

        var controller = new MoviesController(movieServiceMock.Object);

        var response = controller.Create(inputDto);

        var createdAt = response.Result as CreatedAtActionResult;
        Assert.IsNotNull(createdAt);
        Assert.AreEqual(nameof(MoviesController.GetById), createdAt.ActionName);
        Assert.AreEqual(10, createdAt.RouteValues?["id"]);
        Assert.AreEqual(created.ToDto(), createdAt.Value as MovieDto);

        movieServiceMock.VerifyAll();
    }

    [TestMethod]
    public void Update_WhenNotFound_ReturnsNotFound()
    {
        var inputDto = new MovieDto { Id = 123, Title = "A", Stars = 4 };

        var movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
        movieServiceMock
            .Setup(s => s.Update(10, It.Is<Movie>(m => m.Id == 123 && m.Title == "A" && m.Stars == 4)))
            .Returns(false);

        var controller = new MoviesController(movieServiceMock.Object);

        var response = controller.Update(10, inputDto);

        Assert.IsInstanceOfType<NotFoundResult>(response);
        movieServiceMock.VerifyAll();
    }

    [TestMethod]
    public void Update_WhenUpdated_ReturnsNoContent()
    {
        var inputDto = new MovieDto { Id = 123, Title = "A", Stars = 4 };

        var movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
        movieServiceMock
            .Setup(s => s.Update(10, It.Is<Movie>(m => m.Id == 123 && m.Title == "A" && m.Stars == 4)))
            .Returns(true);

        var controller = new MoviesController(movieServiceMock.Object);

        var response = controller.Update(10, inputDto);

        Assert.IsInstanceOfType<NoContentResult>(response);
        movieServiceMock.VerifyAll();
    }

    [TestMethod]
    public void Delete_WhenNotFound_ReturnsNotFound()
    {
        var movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
        movieServiceMock.Setup(s => s.Delete(10)).Returns(false);

        var controller = new MoviesController(movieServiceMock.Object);

        var response = controller.Delete(10);

        Assert.IsInstanceOfType<NotFoundResult>(response);
        movieServiceMock.VerifyAll();
    }

    [TestMethod]
    public void Delete_WhenDeleted_ReturnsNoContent()
    {
        var movieServiceMock = new Mock<IMovieService>(MockBehavior.Strict);
        movieServiceMock.Setup(s => s.Delete(10)).Returns(true);

        var controller = new MoviesController(movieServiceMock.Object);

        var response = controller.Delete(10);

        Assert.IsInstanceOfType<NoContentResult>(response);
        movieServiceMock.VerifyAll();
    }
}
