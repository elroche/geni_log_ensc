
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;

namespace GestionCinema.Controllers;

public class CinemaController : Controller
{
    private readonly CinemaContext _context;


    public CinemaController(CinemaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var cinemas = await _context.Cinemas.OrderBy(c => c.Nom).ToListAsync();
        return View(cinemas);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var cinema = await _context.Cinemas.Where(c => c.Id == id)
               .SingleOrDefaultAsync();
        if (cinema == null)
        {
            return NotFound();
        }

        return View(cinema);
    }
}
