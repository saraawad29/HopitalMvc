using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HopitalMvcSqlite.Data;
using HopitalMvcSqlite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HopitalMvcSqlite.Controllers;

public class PatientsController : Controller
{
    private readonly ApplicationDbContext _context;

    public PatientsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Patients
    public async Task<IActionResult> Index(string? q, int page = 1, int pageSize = 10)
    {
        var query = _context.Patients
            .AsNoTracking()
            .Include(p => p.Department)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(q))
        {
            q = q.Trim();
            query = query.Where(p => p.LastName.Contains(q) || p.FirstName.Contains(q));
        }

        query = query.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);

        var total = await query.CountAsync();
        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.Q = q ?? "";
        ViewBag.Page = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = (int)Math.Ceiling(total / (double)pageSize);

        return View(items);
    }

    // GET: Patients/Create
    public IActionResult Create()
    {
        ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
        return View();

    }

    // POST: Patients/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Patient patient)
    {
        // ✅ Validation DateOfBirth ici
        if (patient.DateOfBirth >= DateTime.Today)
        {
            ModelState.AddModelError(nameof(patient.DateOfBirth),
                "La date de naissance doit être dans le passé.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", patient.DepartmentId);

        return View(patient);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", patient.DepartmentId);
        return View(patient);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Patient patient)
    {
        if (id != patient.Id) return BadRequest();

        if (patient.DateOfBirth >= DateTime.Today)
            ModelState.AddModelError(nameof(patient.DateOfBirth), "La date de naissance doit être dans le passé.");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Erreur: email ou numéro de dossier déjà utilisé.");
            }
        }

        ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", patient.DepartmentId);
        return View(patient);
    }
//Patients : Delete + gestion exception
    public async Task<IActionResult> Delete(int id)
    {
        var patient = await _context.Patients
            .AsNoTracking()
            .Include(p => p.Department)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (patient == null) return NotFound();
        return View(patient);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null) return NotFound();

        try
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            // exemple : patient a des consultations
            TempData["Error"] = "Suppression impossible : ce patient a des consultations.";
            return RedirectToAction(nameof(Index));
        }
    }


    public async Task<IActionResult> Details(int id)
    {
        var patient = await _context.Patients
            .AsNoTracking()
            .Include(p => p.Department)
            .Include(p => p.Consultations)
                .ThenInclude(c => c.Doctor)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (patient == null) return NotFound();

        return View(patient);
    }
}