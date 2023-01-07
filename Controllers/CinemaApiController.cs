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

    //Récupère tous les cinémas 
    [HttpGet("")]
    public async Task<ActionResult<IEnumerable<Cinema>>> GetCinemas()
    {
        return await _context.Cinemas.OrderBy(c => c.Nom).ToListAsync();
    }

    //Récupère le cinéma avec l'identifiant associé
    [HttpGet("{id}/GetCinema")]
    public async Task<ActionResult<Cinema>> GetCinema(int id)
    {
        var cinema = await _context.Cinemas.Where(c => c.Id == id)
                .SingleOrDefaultAsync();
        if (cinema == null)
        {
            return NotFound();
        }
        return cinema;
    }

    //Récupère tous les cinémas en fonction d'une ville
    [HttpGet("{ville}/GetCinemas")]
    public async Task<ActionResult<IEnumerable<Cinema>>> GetCinemas(string ville)
    {
        var cinemas = await _context.Cinemas.Where(c => c.Ville == ville)
                .ToListAsync();
        if (cinemas == null)
        {
            return NotFound();
        }
        return cinemas;
    }

    // POST: api/CinemaApi
    // Permet d'ajouter un cinéma
    [HttpPost]
    public async Task<ActionResult<Cinema>> PostCinema(Cinema cinema)
    {
        _context.Cinemas.Add(cinema);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCinema", new { id = cinema.Id }, cinema);
    }

    // PUT: api/CinemaApi/
    // Permet de modifier le cinéma associé à l'identifiant id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCinema(int id, Cinema cinema)
    {
        if (id != cinema.Id)
            return BadRequest();

        _context.Entry(cinema).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CinemaExist(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a cinema with specified id already exists
    // Verifie l'existence du cinéma associé à l'identifiant id
    private bool CinemaExist(int id)
    {
        return _context.Cinemas.Any(c => c.Id == id);
    }

    // DELETE: api/CinemaApi/
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCinema(int id)
    {
        var cinema = await _context.Cinemas.FindAsync(id);
        if (cinema == null)
            return NotFound();

        _context.Cinemas.Remove(cinema);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
