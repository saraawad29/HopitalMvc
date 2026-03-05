using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HopitalMvcSqlite.Data;
using HopitalMvcSqlite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace HopitalMvcSqlite.Controllers;
using HopitalMvcSqlite.Models.ViewModels;
public class DepartmentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DepartmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var deps = await _context.Departments
            .Include(d => d.MedicalChief)
            .AsNoTracking()
            .ToListAsync();

        return View(deps);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        if (ModelState.IsValid)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        Console.WriteLine($"POST Department Create: Name={department.Name}, Location={department.Location}");
        Console.WriteLine($"ModelState.IsValid = {ModelState.IsValid}");
        return View(department);
        
    }




    // GET: Departments/Edit/5
public async Task<IActionResult> Edit(int id)
{
    var department = await _context.Departments
        .AsNoTracking()
        .FirstOrDefaultAsync(d => d.Id == id);

    if (department == null) return NotFound();

    // Liste des médecins DU département (pour choisir le chef)
    var doctors = await _context.Doctors
        .Where(doc => doc.DepartmentId == id)
        .AsNoTracking()
        .ToListAsync();

    ViewBag.MedicalChiefs = new SelectList(doctors, "Id", "LastName", department.MedicalChiefId);

    return View(department);
}

    // POST: Departments/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Department department)
    {
        if (id != department.Id) return BadRequest();

        // Sécurité : le chef médical doit être un médecin du même département
        if (department.MedicalChiefId.HasValue)
        {
            var isDoctorInDept = await _context.Doctors
                .AnyAsync(d => d.Id == department.MedicalChiefId.Value && d.DepartmentId == department.Id);

            if (!isDoctorInDept)
            {
                ModelState.AddModelError(nameof(department.MedicalChiefId),
                    "Le responsable doit être un médecin appartenant à ce département.");
            }
        }

        if (ModelState.IsValid)
        {
            _context.Update(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        var doctors = await _context.Doctors
            .Where(doc => doc.DepartmentId == id)
            .AsNoTracking()
            .ToListAsync();

        ViewBag.MedicalChiefs = new SelectList(doctors, "Id", "LastName", department.MedicalChiefId);

        return View(department);
    }

    public async Task<IActionResult> Stats()
{
    var stats = await _context.Departments
        .AsNoTracking()
        .Select(d => new DepartmentStatsVm
        {
            DepartmentId = d.Id,
            DepartmentName = d.Name,
            DoctorsCount = _context.Doctors.Count(doc => doc.DepartmentId == d.Id),
            ConsultationsCount = _context.Consultations.Count(c => c.Doctor != null && c.Doctor.DepartmentId == d.Id)
        })
        .ToListAsync();

    return View(stats);
}
}