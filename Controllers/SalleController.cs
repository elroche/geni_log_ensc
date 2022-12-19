
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;

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
        var salle = await _context.Salles.Where(s => s.Id == id)
               .SingleOrDefaultAsync();
        if (salle == null)
        {
            return NotFound();
        }

        return View(salle);
    }
}
