using Cine.Domain;

namespace Cine.Repository.Abstractions;

public interface IMovieRepository
{
    IReadOnlyList<Movie> GetAll();
    Movie? GetById(int id);
    Movie Add(Movie movie);
    bool Update(int id, Movie movie);
    bool Delete(int id);
}
