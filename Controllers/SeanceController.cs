
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;
using GestionCinema.Models;

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
        var seances = _context.Seances
                .Include(s => s.Film)
                .Include(s => s.Salle)
                .Include(s => s.Cinema)
                .Where(s => s.CinemaId == id)
                .GroupBy(s => s.Film)
                .Select(s => s.First())
                .ToList();
        ViewData["seances"] = seances;

        if (seances == null)
        {
            return NotFound();
        }

        return View();
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

    // GET: Seance/Create
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FilmId, SalleId,CinemaId, Date, NbPlaceAchete")] Seance seance)
    {
        if (ModelState.IsValid)
        {
            _context.Add(seance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
