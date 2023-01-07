
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;
using GestionCinema.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionCinema.Controllers;

public class SalleController : Controller
{
    private readonly CinemaContext _context;


    public SalleController(CinemaContext context)
    {
        _context = context;
    }

    // Récupère la liste des salles
    public async Task<IActionResult> Index()
    {
        var salles = await _context.Salles.Include(s => s.Cinema).OrderBy(s => s.Id).ToListAsync();
        return View(salles);
    }

    // Récupère la salle associé à l'identifiant id
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

    // Récupère toutes les salles du cinéma associé à l'identifiant id du cinéma
    public async Task<IActionResult> FindSallesCinema(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var salles = await _context.Salles
            .Include(s => s.Cinema)
            .Where(s => s.CinemaId == id)
            .ToListAsync();

        if (salles == null)
        {
            return NotFound();
        }

        return View(salles);
    }

    // GET: Salle/Create
    public async Task<IActionResult> Create()
    {
        var cinemas = await _context.Cinemas.OrderBy(c => c.Nom).ToListAsync();
        ViewData["CinemaId"] = new SelectList(cinemas, "Id", "Nom");
        return View();
    }

    // POST: Salle/Create
    // Permet d'ajouter une salle
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CinemaId,NbPlace,NumeroSalle")] SalleDTO salleDTO)
    {
        Salle salle = new Salle(salleDTO);

        // Lookup cinema
        var cinema = await _context.Cinemas.Where(c => c.Id == salle.CinemaId).SingleOrDefaultAsync();
        // Define cinema for new salle
        salle.Cinema = cinema!;

        salle.NbPlace = salleDTO.NbPlace;
        salle.NumeroSalle = salleDTO.NumeroSalle;

        if (ModelState.IsValid)
        {
            // Create new salle in DB
            _context.Add(salle);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new RouteValueDictionary { { "id", salle.Id } });
        }
        return View(salle);


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
    // Permet de modifier la salle associée à l'identifiant id
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id, CinemaId, NbPlace, NumeroSalle")] SalleDTO salleDTO)
    {
        if (id != salleDTO.Id)
        {
            return NotFound();
        }

        Salle salle = new Salle(salleDTO);

        var cinema = _context.Cinemas.Find(salle.CinemaId);
        salle.Cinema = cinema!;
        salle.NbPlace = salleDTO.NbPlace;
        salle.NumeroSalle = salleDTO.NumeroSalle;

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(salle);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new RouteValueDictionary { { "id", salle.Id } });
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
        }
        return View(salle);
    }

    // Permet de vérifier l'existence de la salle associée à l'identifiant id
    private bool SalleExist(int id)
    {
        return _context.Salles.Any(s => s.Id == id);
    }

    // GET: /Salle/Delete/id
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

    // POST: /Salle/Delete/id
    // Permet de supprimer la salle associée à l'identifiant id
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var salle = await _context.Salles.FindAsync(id);
        if (salle == null)
        {
            return NotFound();
        }
        _context.Salles.Remove(salle);
        await _context.SaveChangesAsync();

        TempData["messageSuccess"] = "La suppression a bien été effectuée.";

        return RedirectToAction(nameof(Index));
    }


}