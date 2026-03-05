using System.ComponentModel.DataAnnotations;

namespace HopitalMvcSqlite.Models;

public class Department
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "La localisation est obligatoire.")]
    public string Location { get; set; } = "";

     // 1 Department -> N Doctors
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();

    // Chef médical (optionnel)
    public int? MedicalChiefId { get; set; }
    public Doctor? MedicalChief { get; set; }

    public Address ContactAddress { get; set; } = new Address();

    // 1 Department -> N Patients (tu l'as déjà)
    public ICollection<Patient> Patients { get; set; } = new List<Patient>();

    public int? ParentDepartmentId { get; set; }
    public Department? ParentDepartment { get; set; }

    public ICollection<Department> SubDepartments { get; set; } = new List<Department>();
    }