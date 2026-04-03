using Cine.Domain;
using Cine.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Cine.Repository;

public sealed class EfMovieRepository(CineDbContext dbContext) : IMovieRepository
{
    public IReadOnlyList<Movie> GetAll()
    {
        return dbContext.Movies
            .AsNoTracking()
            .ToList();
    }

    public Movie? GetById(int id)
    {
        return dbContext.Movies
            .AsNoTracking()
            .FirstOrDefault(m => m.Id == id);
    }

    public Movie Add(Movie movie)
    {
        var entity = movie with { Id = 0 };
        dbContext.Movies.Add(entity);
        dbContext.SaveChanges();

        return entity;
    }

    public bool Update(int id, Movie movie)
    {
        var existing = dbContext.Movies.FirstOrDefault(m => m.Id == id);
        if (existing is null)
        {
            return false;
        }

        existing.Title = movie.Title;
        existing.Stars = movie.Stars;

        dbContext.SaveChanges();
        return true;
    }

    public bool Delete(int id)
    {
        var existing = dbContext.Movies.FirstOrDefault(m => m.Id == id);
        if (existing is null)
        {
            return false;
        }

        dbContext.Movies.Remove(existing);
        dbContext.SaveChanges();
        return true;
    }
}
