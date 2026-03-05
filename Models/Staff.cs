using System.ComponentModel.DataAnnotations;

namespace HopitalMvcSqlite.Models;

public abstract class Staff
{
    public int Id { get; set; }

    [Required]
    public string FirstName { get; set; } = "";

    [Required]
    public string LastName { get; set; } = "";

    [Required]
    public DateTime HireDate { get; set; }

    [Required]
    public decimal Salary { get; set; }

    // ✅ Ajout pour corriger tes vues (et utile en vrai)
    [Required, EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    public string Phone { get; set; } = "";
}