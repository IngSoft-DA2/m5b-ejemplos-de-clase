using Cine.BusinessLogic.Abstractions;
using Cine.Contracts;
using Cine.Mappings;
using Microsoft.AspNetCore.Mvc;

namespace Cine.Controllers;
[Route("api/[controller]")]
[ApiController]

public class MoviesController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MoviesController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<MovieDto>> GetAll()
    {
        return Ok(_movieService.GetAll().Select(m => m.ToDto()));
    }
    
    [HttpGet("{id:int}")]
    public ActionResult<MovieDto> GetById([FromRoute] int id)
    {
        var movie = _movieService.GetById(id);
        if (movie is null)
        {
            return NotFound();
        }

        return Ok(movie.ToDto());
    }

    [HttpPost]
    public ActionResult<MovieDto> Create([FromBody] MovieDto movieDto)
    {
        var created = _movieService.Create(movieDto.ToDomain());
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created.ToDto());
    }

    [HttpPut("{id:int}")]
    public IActionResult Update([FromRoute] int id, [FromBody] MovieDto updatedMovie)
    {
        var updated = _movieService.Update(id, updatedMovie.ToDomain());
        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var deleted = _movieService.Delete(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }


}
