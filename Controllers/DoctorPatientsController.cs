using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HopitalMvcSqlite.Data;
using HopitalMvcSqlite.Models;

namespace HopitalMvcSqlite.Controllers;

public class DoctorPatientsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DoctorPatientsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Liste des affectations (pour vérifier)
    public async Task<IActionResult> Index()
    {
        var list = await _context.DoctorPatients
            .Include(dp => dp.Doctor)
            .Include(dp => dp.Patient)
            .AsNoTracking()
            .OrderByDescending(dp => dp.AssignedAt)
            .ToListAsync();

        return View(list);
    }

    // GET: DoctorPatients/Assign
    public IActionResult Assign()
    {
        ViewBag.Doctors = new SelectList(_context.Doctors.AsNoTracking(), "Id", "LastName");
        ViewBag.Patients = new SelectList(_context.Patients.AsNoTracking(), "Id", "LastName");
        return View();
    }

    // POST: DoctorPatients/Assign
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Assign(int doctorId, int patientId)
    {
        // Empêcher doublon
        var exists = await _context.DoctorPatients
            .AnyAsync(dp => dp.DoctorId == doctorId && dp.PatientId == patientId);

        if (!exists)
        {
            _context.DoctorPatients.Add(new DoctorPatient
            {
                DoctorId = doctorId,
                PatientId = patientId,
                AssignedAt = DateTime.Now
            });
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    // Voir les patients d’un médecin
    public async Task<IActionResult> PatientsOfDoctor(int id)
    {
        var doctor = await _context.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        if (doctor == null) return NotFound();

        var patients = await _context.DoctorPatients
            .Where(dp => dp.DoctorId == id)
            .Include(dp => dp.Patient)
            .AsNoTracking()
            .Select(dp => dp.Patient!)
            .OrderBy(p => p.LastName)
            .ToListAsync();

        ViewBag.DoctorName = $"{doctor.LastName} {doctor.FirstName}";
        return View(patients);
    }
}