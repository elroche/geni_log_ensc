
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


    // GET: Seance/Create
    public async Task<IActionResult> CreateInitial()
    {
        var cinemas = await _context.Cinemas.OrderBy(c => c.Nom).Where(c => c.Salles.Count() >= 1).ToListAsync();

        ViewData["CinemaId"] = new SelectList(cinemas, "Id", "Nom");
        return View();
    }

    public async Task<IActionResult> Create(int? CinemaId)
    {
        if (CinemaId == null)
        {
            return NotFound();
        }
        var cinema = _context.Cinemas.Find(CinemaId);
        ViewData["cinema"] = cinema.Nom;

        var salles = await _context.Salles.OrderBy(c => c.NumeroSalle).Where(s => s.CinemaId == CinemaId).ToListAsync();
        ViewData["SalleId"] = new SelectList(salles, "Id", "NumeroSalle");

        var films = await _context.Films.OrderBy(c => c.Nom).ToListAsync();
        ViewData["FilmId"] = new SelectList(films, "Id", "Nom");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,FilmId, SalleId,CinemaId, Date")] SeanceDTO seanceDTO)
    {
        Seance seance = new Seance(seanceDTO);

        // Lookup film
        var film = _context.Films.Find(seance.FilmId);
        // Lookup cinema
        var cinema = _context.Cinemas.Find(seance.CinemaId);
        // Lookup salle
        var salle = _context.Salles.Find(seance.SalleId);

        // Define cinema, film, and salle for new seance
        seance.Cinema = cinema!;
        seance.Film = film!;
        seance.Salle = salle!;

        seance.Id = seanceDTO.Id;
        seance.Date = seanceDTO.Date;
        seance.NbPlaceAchete = seanceDTO.NbPlaceAchete;


        // Create new seance in DB
        _context.Add(seance);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new RouteValueDictionary { { "id", seance.Id } });
    }

    // GET: Seance/Edit/id
    public async Task<IActionResult> Edit(int? id)
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

        var films = await _context.Films.OrderBy(f => f.Nom).ToListAsync();
        ViewData["FilmId"] = new SelectList(films, "Id", "Nom");

        var salles = await _context.Salles.OrderBy(s => s.NumeroSalle).Where(s => s.CinemaId == seance.CinemaId).ToListAsync();
        ViewData["SalleId"] = new SelectList(salles, "Id", "NumeroSalle");

        return View(seance);
    }

    // POST: Seance/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id, FilmId, SalleId, CinemaId, Date, NbPlaceAchete")] SeanceDTO seanceDTO)
    {
        if (id != seanceDTO.Id)
        {
            return NotFound();
        }

        Seance seance = new Seance(seanceDTO);

        var cinema = _context.Cinemas.Find(seance.CinemaId);
        seance.Cinema = cinema!;

        var film = _context.Films.Find(seance.FilmId);
        seance.Film = film!;

        var salle = _context.Salles.Find(seance.SalleId);
        seance.Salle = salle!;

        seance.Id = seanceDTO.Id;
        seance.Date = seanceDTO.Date;
        seance.NbPlaceAchete = seanceDTO.NbPlaceAchete;

        _context.Update(seance);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new RouteValueDictionary { { "id", seance.Id } });
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
