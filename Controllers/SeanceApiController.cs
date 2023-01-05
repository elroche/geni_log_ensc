using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;
using GestionCinema.Models;


[Route("api/[controller]")]
[ApiController]
public class SeanceApiController : ControllerBase
{
    private readonly CinemaContext _context;

    public SeanceApiController(CinemaContext context)
    {
        _context = context;
    }

    // GET: api/SeanceApi
    public async Task<ActionResult<IEnumerable<Seance>>> GetSeances()
    {
        return await _context.Seances
            .Include(s => s.Film)
            .Include(s => s.Salle)
            .Include(s => s.Cinema)
            .OrderBy(s => s.Id)
            .ToListAsync();
    }

    // GET: api/SeanceApi/GetSeance/id
    [HttpGet("GetSeance/{id}")]
    public async Task<ActionResult<Seance>> GetSeance(int id)
    {
        var seance = await _context.Seances
                .Include(s => s.Film)
                .Include(s => s.Salle)
                .Include(s => s.Cinema)
                .Where(s => s.Id == id)
                .SingleOrDefaultAsync();
        if (seance == null)
        {
            return NotFound();
        }
        return seance;
    }

    // GET: api/SeanceApi/GetFilms/id
    [HttpGet("GetFilms/{id}")]
    public async Task<ActionResult<IEnumerable<Seance>>> GetFilms(int id)
    {
        var seances = await _context.Seances
                .Include(s => s.Film)
                .Include(s => s.Salle)
                .Include(s => s.Cinema)
                .Where(s => s.CinemaId == id)
                .GroupBy(s => s.Film)
                .Select(s => s.First())
                .ToListAsync();
        if (seances == null)
        {
            return NotFound();
        }
        return seances;
    }

    // GET: api/SeanceApi/GetSeancesFilm/id
    [HttpGet("GetSeancesFilm/{id}")]
    public async Task<ActionResult<IEnumerable<Seance>>> GetSeancesFilm(int id)
    {
        var seances = await _context.Seances
                .Include(s => s.Film)
                .Include(s => s.Salle)
                .Include(s => s.Cinema)
                .Where(s => s.Film.Id == id)
                .ToListAsync();
        if (seances == null)
        {
            return NotFound();
        }
        return seances;
    }

    // POST: api/SeanceApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Seance>> PostSeance(Seance seance)
    {
        _context.Seances.Add(seance);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSeance", new { id = seance.Id }, seance);
    }

    // PUT: api/SeanceApi/
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSeance(int id, Seance seance)
    {
        if (id != seance.Id)
            return BadRequest();

        _context.Entry(seance).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SeanceExist(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a film with specified id already exists
    private bool SeanceExist(int id)
    {
        return _context.Seances.Any(s => s.Id == id);
    }

    /*

    // PUT: api/SeanceApi/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSeance(int id, Seance seance1)
    {
        if (id != seance1.Id)
            return BadRequest();

        Seance seance = new Seance(seance1);

        // Lookup student and course
        var salle = _context.Salles.Find(seance.Salle.id);
        var film = _context.Films.Find(seance.Film.id);

        // Define student and course for updated enrollment
        seance.Salle = salle!;
        seance.Film = film!;

        _context.Entry(seance).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Seances.Any(s => s.Id == id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    */

    // DELETE: api/SeanceApi/
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeance(int id)
    {
        var seance = await _context.Seances.FindAsync(id);
        if (seance == null)
            return NotFound();

        _context.Seances.Remove(seance);
        await _context.SaveChangesAsync();

        return NoContent();
    }

}
