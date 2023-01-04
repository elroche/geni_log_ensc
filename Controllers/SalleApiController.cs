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
    public async Task<ActionResult<IEnumerable<Salle>>> GetSalles()
    {
        return await _context.Salles
            .Include(s => s.Cinema)
            .OrderBy(s => s.Id)
            .ToListAsync();
    }

    // GET: api/SalleApi/id
    [HttpGet("{id}")]
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

    // POST: api/SalleApi
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Salle>> PostSalle(Salle salle)
    {
        _context.Salles.Add(salle);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetSalle", new { id = salle.Id }, salle);
    }

    // PUT: api/SalleApi/
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSalle(int id, Salle salle)
    {
        if (id != salle.Id)
            return BadRequest();

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

    // Returns true if a film with specified id already exists
    private bool SalleExist(int id)
    {
        return _context.Salles.Any(s => s.Id == id);
    }

    /*
    // PUT: api/SalleApi/id
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSalle(int id, Salle salle)
    {
        if (id != salle.Id)
            return BadRequest();

        _context.Entry(salle).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SalleExists(id))
                return NotFound();
            else
                throw;
        }
        return NoContent();
    }

    // Returns true if a salle with specified id already exists
    private bool SalleExists(int id)
    {
        return _context.Salle.Any(s => s.Id == id);
    }
    */

    // DELETE: api/SalleApi/
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
