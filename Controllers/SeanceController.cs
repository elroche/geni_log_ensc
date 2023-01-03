
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;
using GestionCinema.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionCinema.Controllers;

public class SeanceController : Controller
{
    private readonly CinemaContext _context;


    public SeanceController(CinemaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var seances = await _context.Seances
            .Include(s => s.Film)
            .Include(s => s.Salle)
            .Include(s => s.Salle.Cinema)
            .OrderBy(s => s.Id)
            .ToListAsync();
        return View(seances);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
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

        return View(seance);
    }

    public async Task<IActionResult> FindSeancesFilm(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var seances = await _context.Seances
            .Include(s => s.Film)
            .Include(s => s.Salle)
            .Include(s => s.Salle.Cinema)
            .Where(s => s.Film.Id == id)
            .ToListAsync();

        if (seances == null)
        {
            return NotFound();
        }

        return View(seances);
    }

    public IActionResult FindFilmsCinema(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var seance = _context.Seances
                .Include(s => s.Film)
                .Include(s => s.Salle)
                .Include(s => s.Cinema)
                .Where(s => s.CinemaId == id)
                .GroupBy(s => s.Film)
                .Select(s => s.First())
                .ToList();
        ViewData["seances"] = seance;

        if (seance == null)
        {
            return NotFound();
        }

        return View();
    }


    // GET: Seance/Create
    public async Task<IActionResult> Create()
    {
        var cinemas = await _context.Cinemas.OrderBy(c => c.Nom).ToListAsync();
        ViewData["CinemaId"] = new SelectList(cinemas, "Id", "Nom");

        return View();
    }

    public async Task<IActionResult> Create2(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var salles = await _context.Salles.OrderBy(c => c.NumeroSalle).Where(s => s.CinemaId == id).ToListAsync();
        ViewData["SalleId"] = new SelectList(salles, "Id", "NumeroSalle");


        var films = await _context.Films.OrderBy(c => c.Nom).ToListAsync();
        ViewData["FilmId"] = new SelectList(films, "Id", "Nom");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FilmId, SalleId,CinemaId, Date, NbPlaceAchete")] Seance seance)
    {
        // Lookup cinema
        var cinema = _context.Cinemas.Find(seance.CinemaId);
        // Define cinema for new salle
        seance.Cinema = cinema!;

        if (ModelState.IsValid)
        {
            _context.Add(seance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(seance);
    }

    // GET: /Seance/Delete/id
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var seance = await _context.Seances
            .FirstOrDefaultAsync(s => s.Id == id);
        if (seance == null)
        {
            return NotFound();
        }
        return View(seance);
    }


    // POST: /Seance/Delete/id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var seance = await _context.Seances.FindAsync(id);
        _context.Seances.Remove(seance);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
