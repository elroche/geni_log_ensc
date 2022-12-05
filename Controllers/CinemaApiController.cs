using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;
using GestionCinema.Models;


[Route("api/[controller]")]
[ApiController]
public class CinemaApiController : ControllerBase
{
    private readonly CinemaContext _context;

    public CinemaApiController(CinemaContext context)
    {
        _context = context;
    }

    // GET: api/CinemaApi
    public async Task<ActionResult<IEnumerable<Cinema>>> getCinemas()
    {
        return await _context.Cinemas.OrderBy(c => c.Nom).ToListAsync();
    }

    // GET: api/CinemaApi/
    [HttpGet("{id}")]
    public async Task<ActionResult<Cinema>> getCinema(int id)
    {
        var cinema = await _context.Cinemas.Where(c => c.Id == id)
                .SingleOrDefaultAsync();
        if (cinema == null)
        {
            return NotFound();
        }
        return cinema;
    }

    // POST: api/CinemaApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Cinema>> PostCinema(Cinema cinema)
    {
        _context.Cinemas.Add(cinema);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCinema", new { id = cinema.Id }, cinema);
    }

    // Returns true if a cinema with specified id already exists
    private bool CinemaExist(int id)
    {
        return _context.Cinemas.Any(c => c.Id == id);
    }

    // DELETE: api/CinemaApi/
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCinema(int id)
    {
        //TODO : Vérifier supression en cascade?
        var cinema = await _context.Cinemas.FindAsync(id);
        if (cinema == null)
            return NotFound();

        _context.Cinemas.Remove(cinema);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
