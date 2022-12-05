
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestionCinema.Data;

namespace GestionCinema.Controllers;

public class FilmConctroller : Controller
{
    private readonly CinemaContext _context;


    public FilmConctroller(CinemaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var films = await _context.Films.OrderBy(f => f.Nom).ToListAsync();
        return View(films);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var film = await _context.Films.Where(s => s.Id == id)
               .SingleOrDefaultAsync();
        if (film == null)
        {
            return NotFound();
        }

        return View(film);
    }
}