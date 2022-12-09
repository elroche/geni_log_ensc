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
    public async Task<ActionResult<IEnumerable<Seance>>> getSeances()
    {
        return await _context.Seances.OrderBy(s => s.Id).ToListAsync();
    }

    // GET: api/SeanceApi/
    [HttpGet("{id}")]
    public async Task<ActionResult<Seance>> getSeance(int id)
    {
        var seance = await _context.Seances.Where(s => s.Id == id)
                .SingleOrDefaultAsync();
        if (seance == null)
        {
            return NotFound();
        }
        return seance;
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

}
