using System.ComponentModel.DataAnnotations;

namespace ConsoleAppAllowedValues.Models;

public class State
{
    [Required]
    [AllowedValues("VIC", "NSW", "SA", "WA", "NT", "TAS", ErrorMessage = "Invalid State Code")]
    public string? StateCode { get; set; }
    
    [Required]
    public string? Name { get; set; }
}