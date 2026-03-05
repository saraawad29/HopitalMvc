using System.ComponentModel.DataAnnotations;

namespace HopitalMvcSqlite.Models;

public class DoctorPatient
{
    // PK composite
    public int DoctorId { get; set; }
    public Doctor? Doctor { get; set; }

    public int PatientId { get; set; }
    public Patient? Patient { get; set; }

    // Bonus utile pour le TP
    [Required]
    public DateTime AssignedAt { get; set; } = DateTime.Now;
}