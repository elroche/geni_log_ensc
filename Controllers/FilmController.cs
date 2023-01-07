using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;
using GestionCinema.Models;

namespace GestionCinema.Controllers;

public class FilmController : Controller
{
    private readonly CinemaContext _context;


    public FilmController(CinemaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Films.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var film = await _context.Films.Where(f => f.Id == id)
               .SingleOrDefaultAsync();
        if (film == null)
        {
            return NotFound();
        }

        return View(film);
    }

    // GET: Film/Create
    public IActionResult Create()
    {
        var genres = Film.getNamesGenres();
        ViewData["genres"] = genres;
        return View();
    }

    // POST: Film/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,Realisateur,Resume,Genre,Date,Duree")] Film film)
    {
        if (ModelState.IsValid)
        {
            _context.Add(film);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(film);
    }

    // GET: Film/Edit/id
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var film = await _context.Films.FindAsync(id);
        if (film == null)
        {
            return NotFound();
        }
        return View(film);
    }

    // POST: Film/Edit/id
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Realisateur,Resume,Genre,Date,Duree")] Film film)
    {
        if (id != film.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(film);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FilmExist(film.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(film);
    }

    private bool FilmExist(int id)
    {
        return _context.Films.Any(f => f.Id == id);
    }

    // GET: /Film/Delete/id
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var film = await _context.Films
            .FirstOrDefaultAsync(s => s.Id == id);
        if (film == null)
        {
            return NotFound();
        }
        return View(film);
    }

    // POST: /Film/Delete/id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var film = await _context.Films.FindAsync(id);
        if (film == null)
        {
            return NotFound();
        }
        _context.Films.Remove(film);
        await _context.SaveChangesAsync();

        TempData["messageSuccess"] = "La suppression a bien été effectuée.";

        return RedirectToAction(nameof(Index));
    }
}