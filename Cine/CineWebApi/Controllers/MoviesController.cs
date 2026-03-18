using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers;
[Route("api/[controller]")]
[ApiController]

public class MoviesController : ControllerBase
{
    private static readonly List<Movie> _movies = new() 
    {
        new Movie { Id = 1, Title = "Una batalla tras otra", Stars = 5.0 },
        new Movie { Id = 2, Title = "Hoppers", Stars = 4.3 },
        new Movie { Id = 3, Title = "Scream 7", Stars = 2.1 }
    };
    
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_movies);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);
        return Ok(movie);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Movie movie)
    {
        movie.Id = _movies.Max(m => m.Id) + 1;
        _movies.Add(movie);
        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id, [FromBody] Movie updatedMovie)
    {
        var movie = _movies.FirstOrDefault(m => m.Id == id);
        
        movie.Stars = updatedMovie.Stars;
        movie.Title = updatedMovie.Title;
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute]int id)
    {
        _movies.RemoveAll(m => m.Id == id);
        
        return NoContent();
    }


}

public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public double Stars { get; set; }
}