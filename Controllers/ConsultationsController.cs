using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HopitalMvcSqlite.Data;
using HopitalMvcSqlite.Models;

namespace HopitalMvcSqlite.Controllers;

public class ConsultationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ConsultationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var consultations = await _context.Consultations
            .Include(c => c.Patient)
            .Include(c => c.Doctor)
            .AsNoTracking()
            .OrderBy(c => c.Date)
            .ToListAsync();

        return View(consultations);
    }

    public IActionResult Create()
    {
        ViewBag.Patients = new SelectList(_context.Patients, "Id", "LastName");
        ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "LastName");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Consultation consultation)
    {
        if (ModelState.IsValid)
        {
            _context.Consultations.Add(consultation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Patients = new SelectList(_context.Patients, "Id", "LastName", consultation.PatientId);
        ViewBag.Doctors = new SelectList(_context.Doctors, "Id", "LastName", consultation.DoctorId);

        return View(consultation);
    }

    public async Task<IActionResult> EditStatus(int id)
    {
        var c = await _context.Consultations
            .Include(x => x.Patient)
            .Include(x => x.Doctor)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (c == null) return NotFound();

        ViewBag.StatusList = new SelectList(new[] { "Planned", "Completed", "Cancelled" }, c.Status);
        return View(c);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditStatus(int id, string status)
    {
        var c = await _context.Consultations.FindAsync(id);
        if (c == null) return NotFound();

        c.Status = status;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Cancel(int id)
    {
        var c = await _context.Consultations.FindAsync(id);
        if (c == null) return NotFound();

        c.Status = "Cancelled";
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> UpcomingForPatient(int patientId)
    {
        var now = DateTime.Now;

        var list = await _context.Consultations
            .AsNoTracking()
            .Where(c => c.PatientId == patientId && c.Date >= now && c.Status != "Cancelled")
            .Include(c => c.Doctor)
            .OrderBy(c => c.Date)
            .ToListAsync();

        return View(list);
    }

    public async Task<IActionResult> TodayForDoctor(int doctorId)
    {
        var start = DateTime.Today;
        var end = start.AddDays(1);

        var list = await _context.Consultations
            .AsNoTracking()
            .Where(c => c.DoctorId == doctorId && c.Date >= start && c.Date < end)
            .Include(c => c.Patient)
            .OrderBy(c => c.Date)
            .ToListAsync();

        return View(list);
    }
}