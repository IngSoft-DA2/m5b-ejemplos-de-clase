using Cine.Domain;
using Cine.Repository.Abstractions;

namespace Cine.Repository;

public sealed class InMemoryMovieRepository : IMovieRepository
{
    

    private readonly List<Movie> _movies =
    [
        new Movie { Id = 1, Title = "Una batalla tras otra", Stars = 5.0 },
        new Movie { Id = 2, Title = "Hoppers", Stars = 4.3 },
        new Movie { Id = 3, Title = "Scream 7", Stars = 2.1 }
    ];

    public IReadOnlyList<Movie> GetAll()
    {

        return _movies.ToList();

    }

    public Movie? GetById(int id)
    {
        return _movies.FirstOrDefault(m => m.Id == id);
    }

    public Movie Add(Movie movie)
    {
        var nextId = _movies.Count == 0 ? 1 : _movies.Max(m => m.Id) + 1;
        var created = movie with { Id = nextId };
        _movies.Add(created);
        return created;
    }

    public bool Update(int id, Movie movie)
    {

        var index = _movies.FindIndex(m => m.Id == id);
        if (index < 0)
        {
            return false;
        }

        _movies[index] = movie with { Id = id };
        return true;
    }

    public bool Delete(int id)
    {
        return _movies.RemoveAll(m => m.Id == id) > 0;
    }
}
