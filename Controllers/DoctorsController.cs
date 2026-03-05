using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HopitalMvcSqlite.Data;
using HopitalMvcSqlite.Models;

namespace HopitalMvcSqlite.Controllers;

public class DoctorsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DoctorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var doctors = await _context.Doctors
            .Include(d => d.Department)
            .AsNoTracking()
            .ToListAsync();

        return View(doctors);
    }

    public IActionResult Create()
    {
        ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Doctor doctor)
    {
        if (ModelState.IsValid)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", doctor.DepartmentId);
        return View(doctor);
    }


    public async Task<IActionResult> Planning(int id)
    {
        var now = DateTime.Now;

        var doctor = await _context.Doctors
            .AsNoTracking()
            .Include(d => d.Department)
            .FirstOrDefaultAsync(d => d.Id == id);

        if (doctor == null) return NotFound();

        var consultations = await _context.Consultations
            .AsNoTracking()
            .Where(c => c.DoctorId == id && c.Date >= now && c.Status != "Cancelled")
            .Include(c => c.Patient)
            .OrderBy(c => c.Date)
            .ToListAsync();

        ViewBag.Doctor = doctor;
        return View(consultations);
    }
}