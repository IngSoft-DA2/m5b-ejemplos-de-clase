using Cine.Contracts;
using Cine.Domain;

namespace Cine.Mappings;

public static class MovieMapping
{
    public static MovieDto ToDto(this Movie movie)
    {
        return new MovieDto
        {
            Id = movie.Id,
            Title = movie.Title,
            Stars = movie.Stars
        };
    }

    public static Movie ToDomain(this MovieDto movieDto)
    {
        return new Movie
        {
            Id = movieDto.Id,
            Title = movieDto.Title,
            Stars = movieDto.Stars
        };
    }
}
