using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;
using GestionCinema.Models;


[Route("api/[controller]")]
[ApiController]
public class SalleApiController : ControllerBase
{
    private readonly CinemaContext _context;

    public SalleApiController(CinemaContext context)
    {
        _context = context;
    }

    // GET: api/SalleApi
    // Récupère la liste des salles 
    public async Task<ActionResult<IEnumerable<Salle>>> GetSalles()
    {
        return await _context.Salles
            .Include(s => s.Cinema)
            .OrderBy(s => s.Id)
            .ToListAsync();
    }

    // GET: api/SalleApi/id
    // Récupère la salle associé à l'identifiant id
    [HttpGet("GetSalle/{id}")]
    public async Task<ActionResult<Salle>> GetSalle(int id)
    {
        var salle = await _context.Salles
                .Include(s => s.Cinema)
                .Where(s => s.Id == id)
                .SingleOrDefaultAsync();
        if (salle == null)
        {
            return NotFound();
        }
        return salle;
    }

    // GET: api/SalleApi/GetSallesCinema/id
    // Récupère toutes les salles du cinéma associé à l'identifiant id du cinéma
    [HttpGet("GetSallesCinema/{id}")]
    public async Task<ActionResult<IEnumerable<Salle>>> GetSallesCinema(int id)
    {
        var salles = await _context.Salles
             .Include(s => s.Cinema)
             .Where(s => s.CinemaId == id)
             .ToListAsync();
        if (salles == null)
        {
            return NotFound();
        }
        return salles;
    }

    // POST: api/SalleApi
    // Permet d'ajouter une salle
    [HttpPost]
    public async Task<ActionResult<Salle>> PostSalle(SalleDTO salleDTO)
    {
        Salle salle = new Salle(salleDTO);

        var cinema = await _context.Cinemas.Where(c => c.Id == salle.CinemaId).SingleOrDefaultAsync();
        salle.Cinema = cinema!;

        _context.Salles.Add(salle);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSalle", new { id = salle.Id }, salle);
    }

    // PUT: api/SalleApi/
    // Permet de modifier la salle associée à l'identifiant id
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSalle(int id, SalleDTO salleDTO)
    {
        if (id != salleDTO.Id)
            return BadRequest();

        Salle salle = new Salle(salleDTO);

        var cinema = await _context.Cinemas.Where(c => c.Id == salle.CinemaId).SingleOrDefaultAsync();
        salle.Cinema = cinema!;

        _context.Entry(salle).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SalleExist(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Permet de vérifier l'existence de la salle associée à l'identifiant id
    private bool SalleExist(int id)
    {
        return _context.Salles.Any(s => s.Id == id);
    }


    // DELETE: api/SalleApi/
    // Permet de supprimer la salle associée à l'identifiant id
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSalle(int id)
    {
        var salle = await _context.Salles.FindAsync(id);
        if (salle == null)
            return NotFound();

        _context.Salles.Remove(salle);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
