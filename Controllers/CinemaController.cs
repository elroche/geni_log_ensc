
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;
using GestionCinema.Models;

namespace GestionCinema.Controllers;

public class CinemaController : Controller
{
    private readonly CinemaContext _context;


    public CinemaController(CinemaContext context)
    {
        _context = context;
    }

    //Récupère tous les cinémas 
    public async Task<IActionResult> Index()
    {
        var cinemas = await _context.Cinemas
            .OrderBy(c => c.Nom)
            .ToListAsync();

        return View(cinemas);
    }

    //Récupère le cinéma avec l'identifiant associé
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var cinema = await _context.Cinemas
            .Include(c => c.Salles)
            .Include(c => c.Seances)
            .Where(c => c.Id == id)
            .SingleOrDefaultAsync();

        if (cinema == null)
        {
            return NotFound();
        }

        return View(cinema);
    }

    public IActionResult SearchCinemas()
    {
        var ville = _context.Cinemas.GroupBy(c => c.Ville).Select(c => c.First()).ToList();
        ViewData["villes"] = ville;

        return View();
    }

    //Récupère tous les cinémas en fonction d'une ville
    public IActionResult ResultSearch(String ville)
    {
        if (ville == null)
        {
            return NotFound();
        }
        var cinemas = from c in _context.Cinemas
                      where c.Ville == ville
                      select c;

        if (cinemas == null)
        {
            return NotFound();
        }
        return View(cinemas.ToList());
    }


    // GET: Cinema/Create
    // Permet d'ajouter un cinéma
    public IActionResult Create()
    {
        return View();
    }

    // POST: Cinema/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nom,Adresse,CodePostal,Ville,Responsable,PrixPlace")] Cinema cinema)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cinema);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cinema);
    }

    // GET: Cinema/Edit/id
    // Permet de modifier le cinéma associé à l'identifiant id
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cinema = await _context.Cinemas.FindAsync(id);
        if (cinema == null)
        {
            return NotFound();
        }
        return View(cinema);
    }

    // POST: Cinema/Edit/id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Adresse,CodePostal,Ville,Responsable,PrixPlace")] Cinema cinema)
    {
        if (id != cinema.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cinema);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CinemaExist(cinema.Id))
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
        return View(cinema);
    }

    // Verifie l'existence du cinéma associé à l'identifiant id
    private bool CinemaExist(int id)
    {
        return _context.Cinemas.Any(c => c.Id == id);
    }

}
