using System.ComponentModel.DataAnnotations;

namespace HopitalMvcSqlite.Models;

public class Consultation
{
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public string Status { get; set; } = "Planned";

    // FK Patient
    public int PatientId { get; set; }
    public Patient? Patient { get; set; }

    // FK Doctor
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
}