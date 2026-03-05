using System.ComponentModel.DataAnnotations;

namespace HopitalMvcSqlite.Models;

public class Address
{
    [Required]
    public string Street { get; set; } = "";

    [Required]
    public string City { get; set; } = "";

    [Required]
    public string PostalCode { get; set; } = "";

    public string? Country { get; set; }
}