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
    // Récupère la liste des séances
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
    // Récupère la séance associée à l'identifiant id
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
    // Récupère la liste des films d'un cinéma associée à l'identifiant id du cinéma
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
    // Récupère la liste des séances d'un film associée à l'identifiant id du film
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
    // Permet d'ajouter une séance
    [HttpPost]
    public async Task<ActionResult<Seance>> PostSeance(SeanceDTO seanceDTO)
    {
        Seance seance = new Seance(seanceDTO);

        var cinema = await _context.Cinemas.Where(c => c.Id == seance.CinemaId).SingleOrDefaultAsync();
        seance.Cinema = cinema!;

        var film = await _context.Films.Where(f => f.Id == seance.FilmId).SingleOrDefaultAsync();
        seance.Film = film!;

        var salle = await _context.Salles.Where(s => s.Id == seance.SalleId).SingleOrDefaultAsync();
        seance.Salle = salle!;

        _context.Seances.Add(seance);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSeance", new { id = seance.Id }, seance);
    }

    // PUT: api/SeanceApi/
    // Permet de modifier la séance associée à l'identifiant id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSeance(int id, SeanceDTO seanceDTO)
    {
        if (id != seanceDTO.Id)
            return BadRequest();

        Seance seance = new Seance(seanceDTO);

        var cinema = await _context.Cinemas.Where(c => c.Id == seance.CinemaId).SingleOrDefaultAsync();
        seance.Cinema = cinema!;

        var film = await _context.Films.Where(f => f.Id == seance.FilmId).SingleOrDefaultAsync();
        seance.Film = film!;

        var salle = await _context.Salles.Where(s => s.Id == seance.SalleId).SingleOrDefaultAsync();
        seance.Salle = salle!;

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


    // Permet de vérifier l'existence de la séance associée à l'identifiant id
    private bool SeanceExist(int id)
    {
        return _context.Seances.Any(s => s.Id == id);
    }


    // DELETE: api/SeanceApi/
    // Permet de supprimer la séance associée à l'identifiant id
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
