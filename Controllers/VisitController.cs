using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stomatologia.Data;
using Stomatologia.Models;
using Stomatologia.ViewModels;

namespace Stomatologia.Controllers;

public class VisitController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public VisitController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }

    [HttpGet]
    [Authorize(Roles = ApplicationRoles.User)]
    public async Task<IActionResult> Index()
    {
        var userEmail = User.Identity?.Name;
        
        var visits = await _db.Visits
            .AsNoTracking()
            .Include(v => v.Dentist)
            .Where(v => v.Client.Email == userEmail)
            .ToListAsync();

        return View(visits);
    }

    [HttpGet]
    [Authorize(Roles = ApplicationRoles.User)]
    public async Task<IActionResult> BookVisit()
    {
        ViewBag.AvailableStomatologists = await _db.Dentists.AsNoTracking().ToListAsync();

        return View();
    }

    [HttpPost]
    [Authorize(Roles = ApplicationRoles.User)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BookVisit(VisitViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var userEmail = User.Identity?.Name;
        var user = await _db.Users.FirstAsync(u => u.Email == userEmail);
        var dentist = await _db.Dentists.FirstAsync(d => d.Id == model.DentistId);

        var visit = new Visit
        {
            Dentist = dentist,
            Client = user,
            Date = model.Date
        };

        await _db.Visits.AddAsync(visit);

        await _db.SaveChangesAsync();

        return RedirectToAction("Index", "Visit");
    }
}
