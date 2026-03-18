using Cine.Domain;

namespace Cine.BusinessLogic.Abstractions;

public interface IMovieService
{
    IReadOnlyList<Movie> GetAll();
    Movie? GetById(int id);
    Movie Create(Movie movie);
    bool Update(int id, Movie movie);
    bool Delete(int id);
}
