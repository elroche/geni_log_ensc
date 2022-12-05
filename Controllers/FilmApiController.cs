using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;
using GestionCinema.Models;


[Route("api/[controller]")]
[ApiController]
public class FilmApiController : ControllerBase
{
    private readonly CinemaContext _context;

    public FilmApiController(CinemaContext context)
    {
        _context = context;
    }

    // GET: api/FilmApi
    public async Task<ActionResult<IEnumerable<Film>>> getFilms()
    {
        return await _context.Films.OrderBy(f => f.Nom).ToListAsync();
    }

    // GET: api/FilmApi/
    [HttpGet("{id}")]
    public async Task<ActionResult<Film>> getFilm(int id)
    {
        var film = await _context.Films.Where(f => f.Id == id)
                .SingleOrDefaultAsync();
        if (film == null)
        {
            return NotFound();
        }
        return film;
    }

    // POST: api/FilmApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Film>> PostFilm(Film film)
    {
        _context.Films.Add(film);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetFilm", new { id = film.Id }, film);
    }

    // Returns true if a film with specified id already exists
    private bool FilmExist(int id)
    {
        return _context.Films.Any(f => f.Id == id);
    }

    // DELETE: api/FilmApi/
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFilm(int id)
    {
        //TODO : Vérifier supression en cascade?
        var film = await _context.Films.FindAsync(id);
        if (film == null)
            return NotFound();

        _context.Films.Remove(film);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
