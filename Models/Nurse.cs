using System.ComponentModel.DataAnnotations;

namespace HopitalMvcSqlite.Models;

public class Nurse : Staff
{
    [Required]
    public string Service { get; set; } = "";

    [Required]
    public string Grade { get; set; } = "";
}