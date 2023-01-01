
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;
using GestionCinema.Models;

namespace GestionCinema.Controllers;

public class SalleController : Controller
{
    private readonly CinemaContext _context;


    public SalleController(CinemaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var salles = await _context.Salles.Include(s => s.Cinema).OrderBy(s => s.Id).ToListAsync();
        return View(salles);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var salle = await _context.Salles.Include(s => s.Cinema).Where(s => s.Id == id)
               .SingleOrDefaultAsync();
        if (salle == null)
        {
            return NotFound();
        }

        return View(salle);
    }

    // GET: Salle/Create
    public IActionResult Create()
    {
        var cinemas = _context.Cinemas.OrderBy(c => c.Nom).ToList();
        ViewData["cinemas"] = cinemas;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Cinema,NbPlace,NumeroSalle")] Salle salle)
    {
        if (ModelState.IsValid)
        {
            _context.Add(salle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(salle);
    }


    public async Task<IActionResult> FindSallesCinema(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var salles = await _context.Salles
            .Include(s => s.Cinema)
            .Where(s => s.Cinema.Id == id)
            .ToListAsync();

        if (salles == null)
        {
            return NotFound();
        }

        return View(salles);
    }

    // GET: Salle/Edit/id
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salle = await _context.Salles.Include(s => s.Cinema).Where(s => s.Id == id)
               .SingleOrDefaultAsync();
        if (salle == null)
        {
            return NotFound();
        }
        return View(salle);
    }

    // POST: Salle/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id, Cinema, NbPlace, NumeroSalle")] Salle salle)
    {
        if (id != salle.Id)
        {
            return NotFound();
        }

        try
        {
            _context.Update(salle);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SalleExist(salle.Id))
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

    private bool SalleExist(int id)
    {
        return _context.Salles.Any(s => s.Id == id);
    }

    // GET: Salle/Delete/id
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salle = await _context.Salles
            .FirstOrDefaultAsync(s => s.Id == id);
        if (salle == null)
        {
            return NotFound();
        }

        return View(salle);
    }


}