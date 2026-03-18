using Cine.Domain;
using Cine.BusinessLogic.Abstractions;
using Cine.Repository.Abstractions;

namespace Cine.BusinessLogic;

public sealed class MovieService(IMovieRepository movieRepository) : IMovieService
{
    public IReadOnlyList<Movie> GetAll() => movieRepository.GetAll();

    public Movie? GetById(int id) => movieRepository.GetById(id);

    public Movie Create(Movie movie)
    {
        Validate(movie);
        return movieRepository.Add(movie with { Id = 0 });
    }

    public bool Update(int id, Movie movie)
    {
        Validate(movie);
        return movieRepository.Update(id, movie);
    }

    public bool Delete(int id) => movieRepository.Delete(id);

    private static void Validate(Movie movie)
    {
        if (string.IsNullOrWhiteSpace(movie.Title))
        {
            throw new ArgumentException("Title is required.", nameof(movie));
        }

        if (movie.Stars is < 0 or > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(movie), "Stars must be between 0 and 5.");
        }
    }
}
