using System.ComponentModel.DataAnnotations;

namespace HopitalMvcSqlite.Models;

public class Doctor : Staff
{
    [Required]
    public string Specialty { get; set; } = "";

    [Required]
    public string LicenseNumber { get; set; } = "";

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
    public ICollection<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();
}