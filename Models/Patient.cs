using System.ComponentModel.DataAnnotations;

namespace HopitalMvcSqlite.Models;

public class Patient
{
    public int Id { get; set; }

    [Required]
    public string FileNumber { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

    [Required]
    public string FirstName { get; set; } = "";

    public DateTime DateOfBirth { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Phone { get; set; } = "";

    // Feature 1 (type complexe)
    public Address Address { get; set; } = new Address();

    public int DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();
    public ICollection<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();
}