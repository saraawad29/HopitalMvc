using System.ComponentModel.DataAnnotations;

namespace HopitalMvcSqlite.Models;

public class AdminStaff : Staff
{
    [Required]
    public string Function { get; set; } = "";
}