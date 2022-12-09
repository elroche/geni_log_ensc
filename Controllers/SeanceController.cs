
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;

namespace GestionCinema.Controllers;

public class SeanceConctroller : Controller
{
    private readonly CinemaContext _context;


    public SeanceConctroller(CinemaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var seances = await _context.Seances.OrderBy(s => s.Id).ToListAsync();
        return View(seances);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var seance = await _context.Seances.Where(s => s.Id == id)
               .SingleOrDefaultAsync();
        if (seance == null)
        {
            return NotFound();
        }

        return View(seance);
    }
}
