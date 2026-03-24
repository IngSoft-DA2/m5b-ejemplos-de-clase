using Cine.Domain;
using Cine.Repository;

namespace Cine.Tests;

[TestClass]
public sealed class InMemoryMovieRepositoryTests
{
    [TestMethod]
    public void GetAll_ReturnsInitialMovies()
    {
        var repository = new InMemoryMovieRepository();

        var movies = repository.GetAll();

        Assert.AreEqual(3, movies.Count);
        Assert.IsTrue(movies.Any(m => m.Id == 1 && m.Title == "Una batalla tras otra"));
        Assert.IsTrue(movies.Any(m => m.Id == 2 && m.Title == "Hoppers"));
        Assert.IsTrue(movies.Any(m => m.Id == 3 && m.Title == "Scream 7"));
    }

    [TestMethod]
    public void GetById_WhenExists_ReturnsMovie()
    {
        var repository = new InMemoryMovieRepository();

        var movie = repository.GetById(2);

        Assert.IsNotNull(movie);
        Assert.AreEqual(2, movie.Id);
        Assert.AreEqual("Hoppers", movie.Title);
    }

    [TestMethod]
    public void GetById_WhenNotExists_ReturnsNull()
    {
        var repository = new InMemoryMovieRepository();

        var movie = repository.GetById(999);

        Assert.IsNull(movie);
    }

    [TestMethod]
    public void Add_AssignsNextIdAndPersists()
    {
        var repository = new InMemoryMovieRepository();

        var created = repository.Add(new Movie { Id = 123, Title = "Nueva", Stars = 4 });

        Assert.AreEqual(4, created.Id);
        Assert.AreEqual("Nueva", created.Title);

        var fetched = repository.GetById(4);
        Assert.AreEqual(created, fetched);
    }

    [TestMethod]
    public void Update_WhenExists_UpdatesAndReturnsTrue()
    {
        var repository = new InMemoryMovieRepository();

        var updated = repository.Update(2, new Movie { Id = 999, Title = "Actualizada", Stars = 1 });

        Assert.IsTrue(updated);

        var fetched = repository.GetById(2);
        Assert.IsNotNull(fetched);
        Assert.AreEqual(2, fetched.Id);
        Assert.AreEqual("Actualizada", fetched.Title);
        Assert.AreEqual(1, fetched.Stars);
    }

    [TestMethod]
    public void Update_WhenNotExists_ReturnsFalse()
    {
        var repository = new InMemoryMovieRepository();

        var updated = repository.Update(999, new Movie { Id = 0, Title = "X", Stars = 1 });

        Assert.IsFalse(updated);
    }

    [TestMethod]
    public void Delete_WhenExists_RemovesAndReturnsTrue()
    {
        var repository = new InMemoryMovieRepository();

        var deleted = repository.Delete(2);

        Assert.IsTrue(deleted);
        Assert.IsNull(repository.GetById(2));
    }

    [TestMethod]
    public void Delete_WhenNotExists_ReturnsFalse()
    {
        var repository = new InMemoryMovieRepository();

        var deleted = repository.Delete(999);

        Assert.IsFalse(deleted);
    }
}
