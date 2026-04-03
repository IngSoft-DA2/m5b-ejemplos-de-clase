using Cine.Domain;
using Cine.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace Cine.Tests;

[TestClass]
public sealed class EfMovieRepositoryInMemoryDbTests
{
    private CineDbContext _context = null!;
    private EfMovieRepository _repository = null!;
    private SqliteConnection _connection = null!;

    [TestInitialize]
    public void TestInitialize()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<CineDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new CineDbContext(options);
        _context.Database.EnsureCreated();

        _repository = new EfMovieRepository(_context);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _context.Dispose();
        _connection.Close();
        _connection.Dispose();
    }

    [TestMethod]
    public void GetAll_ReturnsMoviesFromDatabase()
    {
        _context.Movies.AddRange(
            new Movie { Title = "Una batalla tras otra", Stars = 5.0 },
            new Movie { Title = "Hoppers", Stars = 4.3 },
            new Movie { Title = "Scream 7", Stars = 2.1 }
        );
        _context.SaveChanges();

        var movies = _repository.GetAll();

        Assert.AreEqual(3, movies.Count);
        Assert.IsTrue(movies.Any(m => m.Title == "Una batalla tras otra"));
        Assert.IsTrue(movies.Any(m => m.Title == "Hoppers"));
        Assert.IsTrue(movies.Any(m => m.Title == "Scream 7"));
    }

    [TestMethod]
    public void Add_PersistsMovieInDatabase()
    {
        var created = _repository.Add(new Movie { Title = "Nueva", Stars = 4 });

        Assert.IsTrue(created.Id > 0);

        var fetched = _repository.GetById(created.Id);
        Assert.IsNotNull(fetched);
        Assert.AreEqual(created.Id, fetched!.Id);
        Assert.AreEqual("Nueva", fetched.Title);
        Assert.AreEqual(4, fetched.Stars);
    }

    [TestMethod]
    public void Update_WhenExists_UpdatesAndReturnsTrue()
    {
        var created = _repository.Add(new Movie { Title = "Original", Stars = 3 });
        var updated = _repository.Update(created.Id, new Movie { Id = 0, Title = "Actualizada", Stars = 1 });

        Assert.IsTrue(updated);

        var fetched = _repository.GetById(created.Id);
        Assert.IsNotNull(fetched);
        Assert.AreEqual(created.Id, fetched!.Id);
        Assert.AreEqual("Actualizada", fetched.Title);
        Assert.AreEqual(1, fetched.Stars);
    }

    [TestMethod]
    public void Update_WhenNotExists_ReturnsFalse()
    {
        var updated = _repository.Update(999, new Movie { Id = 0, Title = "X", Stars = 1 });

        Assert.IsFalse(updated);
    }

    [TestMethod]
    public void Delete_WhenExists_RemovesAndReturnsTrue()
    {
        var created = _repository.Add(new Movie { Title = "Borrar", Stars = 2 });
        var deleted = _repository.Delete(created.Id);

        Assert.IsTrue(deleted);
        Assert.IsNull(_repository.GetById(created.Id));
    }

    [TestMethod]
    public void Delete_WhenNotExists_ReturnsFalse()
    {
        var deleted = _repository.Delete(999);

        Assert.IsFalse(deleted);
    }
}
